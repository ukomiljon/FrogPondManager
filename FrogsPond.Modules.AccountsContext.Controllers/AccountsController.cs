using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FrogsPond.Modules.AccountsContext.Domain.Entities;
using FrogsPond.Modules.AccountsContext.Domain.Models;
using FrogsPond.Modules.AccountsContext.Domain.Services;
using FrogsPond.Modules.AccountsContext.Domain.UseCases;
using FrogsPond.Modules.AccountsContext.Domain.UseCases.DTO;
 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrogsPond.Modules.AccountsContext.Controllers
{
    [ApiController] 
    [Route("[controller]")]

    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(
            IAccountService accountService,
            IMapper mapper
            )
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async  Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model, string secret)
        {
            var response = await _accountService.Authenticate(model, ipAddress(), secret);
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshToken(string secret)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshToken(refreshToken, ipAddress(), secret);
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            // users can revoke their own tokens and admins can revoke any tokens
            if (!Account.OwnsToken(token) && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            await _accountService.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            await _accountService.Register(model, Request.Headers["origin"]);
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailRequest model)
        {
            await _accountService.VerifyEmail(model.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
           await _accountService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> ValidateResetToken(ValidateResetTokenRequest model)
        {
            await _accountService.ValidateResetToken(model);
            return Ok(new { message = "Token is valid" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            await _accountService.ResetPassword(model);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAll()
        {
            var accounts = await _accountService.GetAll();
            return Ok(accounts);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AccountResponse>> GetById(string id)
        {
            // users can get their own account and admins can get any account
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var account = await _accountService.GetById(id);
            return Ok(account);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<AccountResponse>> Create(AccountCreateRequest model)
        {
            var account = await _accountService.Create(model);
            return Ok(account);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AccountResponse>> Update(string id, AccountUpdateRequest model)
        {
            // users can update their own account and admins can update any account
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            // only admins can update role
            if (Account.Role != Role.Admin)
                model.Role = null;

            var account = await _accountService.Update(id, model);
            return Ok(account);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(string id)
        {
            // users can delete their own account and admins can delete any account
            if (id != Account.Id && Account.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            await _accountService.Delete(id);
            return Ok(new { message = "Account deleted successfully" });
        }

        // helper methods

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }



}
