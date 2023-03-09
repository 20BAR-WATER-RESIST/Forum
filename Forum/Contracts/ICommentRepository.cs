﻿using Forum.Models;

namespace Forum.Contracts
{
    public interface ICommentRepository
    {
        Task<List<Comment>> DapperCommentsTest(int topicId, int currentPage);
        Task<int> GetCommentAmmountPerTopic(int id);
        Task<List<(string CommentText, DateTime CommentAddedTime, bool IsActive, string UserName)>> LoadPlotsComments(int id, int currentPage);
    }
}
