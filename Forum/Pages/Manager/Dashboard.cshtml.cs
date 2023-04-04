using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages.Manager
{
    [Authorize]
    public class DashboardModel : PageModel
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
