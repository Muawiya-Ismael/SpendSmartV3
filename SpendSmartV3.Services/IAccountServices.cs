using Microsoft.AspNetCore.Identity;
using SpendSmartV3.Objects.Views.Authentication;

namespace SpendSmartV3.Services
{
    public interface IAccountServices
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignInResult> Login(LoginViewModel model);
        Task Logout();
        Task SignIn();
        Task<IdentityUser> getUser();
    }
}
