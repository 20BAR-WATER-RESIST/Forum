using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages
{
    [Authorize]
    public class ManagerModel : PageModel
    {
        public IActionResult OnGet()
        {
            bool isAdmin = User.IsInRole("Admin");
            bool isOwner = User.IsInRole("Owner");

            if (isAdmin || isOwner)
            {
                return Page();
            }
            else
            {
                return new ForbidResult();
            }
        }
    }
}
