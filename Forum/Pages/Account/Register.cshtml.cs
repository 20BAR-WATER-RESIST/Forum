using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IRegisterRepository _registerRepository;

        public RegisterModel(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        [BindProperty]
        public RegisterForm RegistrationData { get; set; }
        public bool RegistrationCompleted { get; private set; } = false;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            } 

            var checkUsr = await _registerRepository.CheckUsernameAvailability(RegistrationData.RegisterUsername);

            if (checkUsr)
            {
                
                var registerStatus = await _registerRepository.CompleteRegister(RegistrationData.RegisterEmail, RegistrationData.RegisterUsername, RegistrationData.RegisterPassword);

                RegistrationCompleted = registerStatus;

                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }

        }

        public void OnGet()
        {
        }
    }
}
