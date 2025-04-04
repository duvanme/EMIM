using EMIM.ViewModels;
using EMIM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMIM.Services
{
    public interface IQuestionService
{
    Task<List<QuestionViewModel>> GetQuestionsByProductIdAsync(int productId, QuestionStatus? status = null);
    Task<List<QuestionViewModel>> GetQuestionsByStoreIdAsync(string storeId);
    Task<bool> CanStoreAnswerQuestionAsync(int questionId, string storeId);
    Task<bool> SaveQuestionAsync(Question question);
    Task<bool> AnswerQuestionAsync(int id, string answer);
    Task<int> GetProductIdByQuestionIdAsync(int questionId);
    Task<List<QuestionViewModel>> GetAnsweredQuestionsByProductIdAsync(int productId);
}
}
