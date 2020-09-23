using FrogsPond.Modules.AccountsContext.Domain.Entities;
using FrogsPond.Modules.AccountsContext.Domain.Models;
using FrogsPond.Modules.AccountsContext.Domain.Services;
using FrogsPond.Modules.AccountsContext.Domain.UseCases.DTO;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FrogsPond.Modules.AccountsContext.Domain.Services
{

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountService(
            IAccountRepository accountRepository,
            IEmailService emailService,
             IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress, string secret)
        {
            var account = await _accountRepository.FindByEmail(model.Email);

            if (account == null || !account.IsVerified || !BC.Verify(model.Password, account.PasswordHash))
                throw new Exception("Email or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(account, _emailService.GetSecret());
            var refreshToken = generateRefreshToken(ipAddress);

            // save refresh token            
            account.AddResetToken(refreshToken);
            await _accountRepository.Update(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress, string secret)
        {
            var (refreshToken, account) = await getRefreshToken(token);

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            account.AddResetToken(newRefreshToken);
            await _accountRepository.Update(account);

            // generate new jwt
            var jwtToken = generateJwtToken(account, secret);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var (refreshToken, account) = await getRefreshToken(token);

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _accountRepository.Update(account);
        }

        public async Task Register(RegisterRequest model, string origin)
        {
            // validate
            if (await _accountRepository.FindByEmail(model.Email) != null)
            {
                // send already registered error in email to prevent account enumeration
                sendAlreadyRegisteredEmail(model.Email, origin);
                return;
            }

            // map model to new account object
            var account = _mapper.Map<Account>(model);

            // first registered account is an admin
            var isFirstAccount = await  _accountRepository.GetCount() == 0;
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.VerificationToken = randomTokenString();

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            await _accountRepository.Add(account);

            // send email
            sendVerificationEmail(account, origin);
        }

        public async Task VerifyEmail(string token)
        {
            var account = await _accountRepository.FindValidatedResetToken(token);

            if (account == null) throw new Exception("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await _accountRepository.Update(account);
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _accountRepository.FindByEmail(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            // create reset token that expires after 1 day
            account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(24);

            await _accountRepository.Update(account);

            // send email
            sendPasswordResetEmail(account, origin);
        }

        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            var account = await _accountRepository.FindValidatedResetToken(model.Token);

            if (account == null)
                throw new Exception("Invalid token");
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await _accountRepository.FindValidatedResetToken(model.Token);

            if (account == null)
                throw new Exception("Invalid token");

            // update password and remove reset token
            account.PasswordHash = BC.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _accountRepository.Update(account);
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = await _accountRepository.GetAll();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(string id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse> Create(AccountCreateRequest model)
        {
            // validate
            if (await _accountRepository.FindByEmail(model.Email) != null)
                throw new Exception($"Email '{model.Email}' is already registered");

            // map model to new account object
            var account = _mapper.Map<Account>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
            await _accountRepository.Add(account);

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse> Update(string id, AccountUpdateRequest model)
        {
            var account = await getAccount(id);

            // validate
            if (account.Email != model.Email && _accountRepository.FindByEmail(model.Email) != null)
                throw new Exception($"Email '{model.Email}' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                account.PasswordHash = BC.HashPassword(model.Password);

            // copy model to account and save
            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            await _accountRepository.Update(account);

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(string id)
        {
            var account = await getAccount(id);
            await _accountRepository.Delete(account);
        }

        private async Task<Account> getAccount(string id)
        {
            var account = await _accountRepository.FindById(id);
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        private async Task<(RefreshToken, Account)> getRefreshToken(string token)
        {
            var account = await _accountRepository.FindOneFromRefreshTokens(token);
            if (account == null) throw new Exception("Invalid token");
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive) throw new Exception("Invalid token");
            return (refreshToken, account);
        }

        private string generateJwtToken(Account account, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_emailService.GetSecret());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private string randomTokenString()
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void sendVerificationEmail(Account account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/accounts/verify-email?token={account.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                             <p><code>{account.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }

        private void sendPasswordResetEmail(Account account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/accounts/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }
    }

}
