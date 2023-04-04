using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

public class LoginModel : PageModel
{
    private readonly ILoginRepository _loginRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginModel(ILoginRepository loginRepository, IHttpContextAccessor httpContextAccessor)
    {
        _loginRepository = loginRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    [BindProperty]
    public LoginForm UserFormData { get; set; }

    public async Task<JsonResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return new JsonResult(new { success = false });
        }

        var usr = await _loginRepository.GetVerification(UserFormData.Email, UserFormData.Pass);

        if (string.IsNullOrEmpty(usr))
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

            return new JsonResult(new { success = true });
        }
        else
        {
            return new JsonResult(new { success = false, message = usr });
        }
    }

    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Index");
        }

        return Page();
    }

}
