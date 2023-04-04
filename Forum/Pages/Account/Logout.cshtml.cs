using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages.Account
{
    public class LogoutModel : PageModel
    {

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public async Task<JsonResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync();
            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await Logout();
        }
    }
}
