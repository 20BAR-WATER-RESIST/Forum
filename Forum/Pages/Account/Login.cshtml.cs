using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Forum.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly ILoginRepository _loginRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginModel(ILoginRepository loginRepository,IHttpContextAccessor httpContextAccessor)
        {
            _loginRepository = loginRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public LoginForm UserFormData { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var previousUrl = HttpContext.Session.GetString("PreviousUrl");
            HttpContext.Session.Remove("PreviousUrl");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var usr = await _loginRepository.GetVerification(UserFormData.Email, UserFormData.Pass);

            if (usr)
            {

                var userData = await _loginRepository.GetUserAccData(UserFormData.Email, UserFormData.Pass);

                var claims = new List<Claim>() { new Claim(ClaimTypes.Name, userData.UserName),
                                                 new Claim(ClaimTypes.Email, userData.UserEmail),
                                                 new Claim(ClaimTypes.Role, userData.UserTypes.UserTypeName)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = UserFormData.RememberMe
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                if (!string.IsNullOrEmpty(previousUrl) && Uri.IsWellFormedUriString(previousUrl, UriKind.Relative) 
                    && previousUrl.StartsWith("/") && !previousUrl.StartsWith("//") && !previousUrl.StartsWith("/\\"))
                {
                    return LocalRedirect(previousUrl);
                }
                else { return RedirectToPage("/Index"); }

            }
            else { return Page(); }

        }

        public void OnGet()
        {
        }
    }
}
