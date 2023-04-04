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

        public async Task<JsonResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "Wprawdzono nieprawid³owy format danych do formularza." });
            } 

            var checkUsr = await _registerRepository.CheckUsernameAndUseremailAvailability(RegistrationData.RegisterUsername, RegistrationData.RegisterEmail);

            if (string.IsNullOrEmpty(checkUsr))
            {
                
                var registerResults = await _registerRepository.CompleteRegister(RegistrationData.RegisterEmail, RegistrationData.RegisterUsername, RegistrationData.RegisterPassword);

                if (registerResults)
                {
                    return new JsonResult(new { success = true });
                }
                else { return new JsonResult(new { success = false, message="Wyst¹pi³ b³¹d podczas przetwarzania formularza rejestracji. Spróbuj ponownie za chwilê." }); }
            }
            else
            {
                return new JsonResult(new { success = false, message = checkUsr });
            }

        }

        public void OnGet()
        {
        }
    }
}
