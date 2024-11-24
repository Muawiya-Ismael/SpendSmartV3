using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SpendSmartV3.Objects.Views.Authentication;

namespace SpendSmartV3.Services.Account
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IdentityUser user;

        public AccountServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
    }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task SignIn()
        {
            await _signInManager.SignInAsync(user, false);
        }

        public async Task<IdentityUser> getUser()
        {
            var user = _httpContextAccessor.HttpContext?.User; 
            if (user == null)
            {
                return null;
            }

            return await _userManager.GetUserAsync(user);
        }

    }
}
