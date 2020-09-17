using FrogsPond.Modules.AccountsContext.Domain.Entities;
using FrogsPond.Modules.AccountsContext.Domain.Models;
using FrogsPond.Modules.AccountsContext.Domain.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrogsPond.Modules.AccountsContext.Domain.Services
{
    public interface IAccountService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress, string secret);
        AuthenticateResponse RefreshToken(string token, string ipAddress, string secret);
        void RevokeToken(string token, string ipAddress);
        void Register(RegisterRequest model, string origin);
        void VerifyEmail(string token);
        void ForgotPassword(ForgotPasswordRequest model, string origin);
        void ValidateResetToken(ValidateResetTokenRequest model);
        void ResetPassword(ResetPasswordRequest model);
        IEnumerable<AccountResponse> GetAll();
        AccountResponse GetById(int id);
        AccountResponse Create(CreateRequest model);
        AccountResponse Update(int id, UpdateRequest model);
        void Delete(int id); 
       
    }
}
