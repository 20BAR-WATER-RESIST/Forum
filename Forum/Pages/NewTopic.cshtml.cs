using Forum.Contracts;
using Forum.Models;
using Forum.Models.ReportSystem;
using Forum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Forum.Pages
{
    [Authorize]
    public class NewTopicModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICRUD_Repository _crud_Repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewTopicModel(ICategoryRepository categoryRepository, ICRUD_Repository crud_Repository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _crud_Repository = crud_Repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Category> listOfCategories { get; private set; }
        [BindProperty]
        public Category CategoryFormData { get; set; }
        [BindProperty]
        public Topic TopicFormData { get; set; }

        public async Task OnGet()
        {
            listOfCategories = await _categoryRepository.LoadListOfCategories();
        }

        public async Task<IActionResult> OnPostSendTopic()
        {
            string UserName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            if (UserName != null)
            {
                await _crud_Repository.InsertTopic(CategoryFormData.CategoryID,TopicFormData.TopicName.ToString(), TopicFormData.TopicDescription.ToString(), UserName);

                return RedirectToPage("/Index");
            }
            else { return Page(); }
        }
    }
}
