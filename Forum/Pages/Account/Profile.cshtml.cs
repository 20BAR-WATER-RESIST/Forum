using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Forum.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ICommentRepository _commentRepository;

        public ProfileModel(IUserRepository userRepository, ITopicRepository topicRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _topicRepository = topicRepository;
            _commentRepository = commentRepository;
        }

        public async Task<string> TrimString(string title, int lenght)
        {
            string results = title.ToString();
            string backresults = results.Substring(0, Math.Min(results.Length, lenght));

            if (results.Length > lenght)
            {
                backresults += "...";
            }

            return backresults;
        }

        public string ProfileName { get; private set; }
        public User userProfileHeader { get; private set; }
        public List<Topic> userProfileTopics { get; private set; }
        public List<Comment> userProfileComments { get; private set; }

        public async Task OnGet(string profileName)
        {
            ProfileName = profileName;

            userProfileHeader = await _userRepository.LoadUserProfileHeader(profileName);
            userProfileTopics = await _topicRepository.LoadUserProfileTopics(profileName);
            userProfileComments = await _commentRepository.LoadUserProfileComments(profileName);
        }
    }
}
