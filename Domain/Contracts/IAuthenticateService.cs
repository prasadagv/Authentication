using Authentication.Domain.Models;

namespace Authentication.Domain.Contracts
{
    public interface IAuthenticateService
    {
        public Task<string> Authenticate(LoginRequestModel loginRequestModel);
    }
}
