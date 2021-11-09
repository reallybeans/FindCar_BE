using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.FeedbackService
{
    public interface IFeedbackService
    {
        Task<FeedbackCreateDataDTO> CreateFeedbackAsync(FeedbackCreateDTO feedbackCreateDTO);
        Task<FeedbackDataDTO> GetFeedbackByIdAsync(int id);
        Task<FeedbackListDataDTO> GetAllFeedbackAsync();
        Task<FeedbackUpdateDataDTO> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackUpdateDTO);
        Task<bool> DeleteFeedbackAsync(int id);
        Task<FeedbackListDataDTO> GetFeedbackByGroupAsync(int id);
        Task<FeedbackListDataDTO> GetFeedbackByCustomerAsync(int id);
    }
}
