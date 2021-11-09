using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Service.FeedbackService;

namespace Tim_Xe.API.Controllers.V1
{
    [Route("api/v1/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly FeedbackServiceImp _feedbackServiceImp;
        public FeedbacksController()
        {
            _feedbackServiceImp = new FeedbackServiceImp();
        }
        [HttpGet]
        public async Task<FeedbackListDataDTO> GetAll()
        {
            return await _feedbackServiceImp.GetAllFeedbackAsync();
        }
        [HttpGet("{id}")]
        public async Task<FeedbackDataDTO> GetFeedbackById(int id)
        {
            return await _feedbackServiceImp.GetFeedbackByIdAsync(id);
        }
        [HttpGet("feedback-to-group/{id}")]
        public async Task<FeedbackListDataDTO> GetFeedbackByGroupId(int id)
        {
            return await _feedbackServiceImp.GetFeedbackByGroupAsync(id);
        }
        [HttpGet("feedback-by-customer/{id}")]
        public async Task<FeedbackListDataDTO> GetFeedbackByCustomer(int id)
        {
            return await _feedbackServiceImp.GetFeedbackByCustomerAsync(id);
        }
        [HttpPost]
        public async Task<FeedbackCreateDataDTO> CreateAsync(FeedbackCreateDTO feedback)
        {
            return await _feedbackServiceImp.CreateFeedbackAsync(feedback);
        }
        [HttpPut]
        public async Task<FeedbackUpdateDataDTO> UpdateAsync(FeedbackUpdateDTO feedback)
        {
            return await _feedbackServiceImp.UpdateFeedbackAsync(feedback);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            if (id > 0)
            {
                var result = await _feedbackServiceImp.DeleteFeedbackAsync(id);
                if (result)
                {
                    return Ok("Delete Success!");
                }
                else return BadRequest("Delete Failed!");
            };
            return NotFound();
        }
    }
}
