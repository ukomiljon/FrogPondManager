using FrogsPond.Modules.AccountsContext.Domain.Models;
using FrogsPond.Modules.AccountsContext.Domain.UseCases.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrogsPond.Modules.AccountsContext.Domain.Services
{
    public interface IAccountService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress, string secret);
        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress, string secret);
        Task RevokeToken(string token, string ipAddress);
        Task Register(RegisterRequest model, string origin);
        Task VerifyEmail(string token);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task ValidateResetToken(ValidateResetTokenRequest model);
        Task ResetPassword(ResetPasswordRequest model);
        Task<IEnumerable<AccountResponse>> GetAll();
        Task<AccountResponse> GetById(string id);
        Task<AccountResponse> Create(AccountCreateRequest model);
        Task<AccountResponse> Update(string id, AccountUpdateRequest model);
        Task Delete(string id);

    }
}
