using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tim_Xe.Data.Models;
using Tim_Xe.Data.Repository;
using Tim_Xe.Data.Repository.Entities;

namespace Tim_Xe.Service.FeedbackService
{
    public class FeedbackServiceImp : IFeedbackService
    {
        private readonly TimXeDBContext context;
        private readonly FeedbackMapping feedbackMapping;
        public FeedbackServiceImp()
        {
            context = new TimXeDBContext();
            feedbackMapping = new FeedbackMapping();
        }
        public async Task<FeedbackCreateDataDTO> CreateFeedbackAsync(FeedbackCreateDTO feedbackCreateDTO)
        {
            try
            {
                var existingCustomer = context.Customers.FirstOrDefault(c => c.Id == feedbackCreateDTO.CustomerId);
                var existingBooking = context.Bookings.FirstOrDefault(c => c.Id == feedbackCreateDTO.BookingId);
                var existingDriver = context.Drivers.FirstOrDefault(d => d.Id == feedbackCreateDTO.DriverId);
                if (existingDriver.ReviewScore == null) existingDriver.ReviewScore = 0;
                if (existingCustomer == null)
                {
                    return new FeedbackCreateDataDTO("customer is not available", null, "fail");
                }
                else if (existingBooking == null)
                {
                    return new FeedbackCreateDataDTO("booking is not available", null, "fail");
                }
                else if (existingDriver == null)
                {
                    return new FeedbackCreateDataDTO("driver is not available", null, "fail");
                }
                else
                {
                    switch (feedbackCreateDTO.Ratting)
                    {
                        case 5:
                            existingDriver.ReviewScore += 3;
                            break;
                        case 4:
                            existingDriver.ReviewScore += 2;
                            break;
                        case 3:
                            existingDriver.ReviewScore += 1;
                            break;
                        case 2:
                            existingDriver.ReviewScore -= 1;
                            break;
                        case 1:
                            existingDriver.ReviewScore -= 2;
                            break;
                        default: break;
                    }
                    context.Drivers.Update(existingDriver);
                    await context.SaveChangesAsync();
                    context.Feedbacks.Add(new Feedback()
                    {
                        CustomerId = feedbackCreateDTO.CustomerId,
                        GroupId = context.Drivers.Where(d => d.Id == feedbackCreateDTO.DriverId).Select(d => d.GroupId).FirstOrDefault(),
                        Ratting = feedbackCreateDTO.Ratting,
                        PostDate = DateTime.Now,
                        BookingId = feedbackCreateDTO.BookingId,
                        Description = feedbackCreateDTO.Description,
                        DriverId = feedbackCreateDTO.DriverId,
                        IsDelete = false,
                    });
                    await context.SaveChangesAsync();
                    return new FeedbackCreateDataDTO("cerate success", feedbackCreateDTO, "success");
                }

            }
            catch (Exception e)
            {
                return new FeedbackCreateDataDTO("create fail", null, "fail");
            }
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var existingFeedback = await context.Feedbacks.FirstOrDefaultAsync(c => c.Id == id);

            if (existingFeedback != null)
            {
                existingFeedback.IsDelete = true;
                await context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<FeedbackListDataDTO> GetAllFeedbackAsync()
        {
            var result = await context.Feedbacks.ProjectTo<FeedbackDTO>(feedbackMapping.configFeedback).ToListAsync();
            if (result.Count() == 0)
            {
                return new FeedbackListDataDTO("list is empty", null, "empty");
            }
            else return new FeedbackListDataDTO("success", result, "success");
        }

        public async Task<FeedbackListDataDTO> GetFeedbackByCustomerAsync(int id)
        {
            var result = await context.Feedbacks.Where(f => f.CustomerId == id).ProjectTo<FeedbackDTO>(feedbackMapping.configFeedback).ToListAsync();
            if (result.Count() == 0)
            {
                return new FeedbackListDataDTO("list is empty", null, "success");
            }
            else return new FeedbackListDataDTO("success", result, "success");
        }

        public async Task<FeedbackListDataDTO> GetFeedbackByGroupAsync(int id)
        {
            var result = await context.Feedbacks.Where(f => f.GroupId == id).ProjectTo<FeedbackDTO>(feedbackMapping.configFeedback).ToListAsync();
            if (result.Count() == 0)
            {
                return new FeedbackListDataDTO("list is empty", null, "success");
            }
            else return new FeedbackListDataDTO("success", result, "success");
        }

        public async Task<FeedbackDataDTO> GetFeedbackByIdAsync(int id)
        {
            var result = await context.Feedbacks.ProjectTo<FeedbackDTO>(feedbackMapping.configFeedback).FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                return new FeedbackDataDTO("fail", null, "not Available");
            }
            else return new FeedbackDataDTO("success", result, "available");
        }

        public async Task<FeedbackUpdateDataDTO> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackUpdateDTO)
        {
            try
            {
                var existingGroup = context.Groups.FirstOrDefault(g => g.Id == feedbackUpdateDTO.GroupId);
                var existingCustomer = context.Customers.FirstOrDefault(c => c.Id == feedbackUpdateDTO.CustomerId);
                var existingBooking = context.Bookings.FirstOrDefault(c => c.Id == feedbackUpdateDTO.BookingId);
                var existingDriver = context.Drivers.FirstOrDefault(d => d.Id == feedbackUpdateDTO.DriverId);
                var existingFeedback = await context.Feedbacks.FirstOrDefaultAsync(c => c.Id == feedbackUpdateDTO.Id);
                if (existingDriver.ReviewScore == null) existingDriver.ReviewScore = 0;
                if (existingFeedback == null)
                {
                    return new FeedbackUpdateDataDTO("feedback is not available", null, "update fail");
                }
                if (existingGroup == null)
                {
                    return new FeedbackUpdateDataDTO("group is not available", null, "fail");
                }
                else if (existingCustomer == null)
                {
                    return new FeedbackUpdateDataDTO("cusstomer is not available", null, "fail");
                }
                else if (existingBooking == null)
                {
                    return new FeedbackUpdateDataDTO("booking is not available", null, "fail");
                }
                else if (existingDriver == null)
                {
                    return new FeedbackUpdateDataDTO("driver is not available", null, "fail");
                }
                else
                {
                    switch (existingFeedback.Ratting)
                    {
                        case 5:
                            existingDriver.ReviewScore -= 3;
                            break;
                        case 4:
                            existingDriver.ReviewScore -= 2;
                            break;
                        case 3:
                            existingDriver.ReviewScore -= 1;
                            break;
                        case 2:
                            existingDriver.ReviewScore += 1;
                            break;
                        case 1:
                            existingDriver.ReviewScore += 2;
                            break;
                        default: break;
                    }
                    switch (feedbackUpdateDTO.Ratting)
                    {
                        case 5:
                            existingDriver.ReviewScore += 3;
                            break;
                        case 4:
                            existingDriver.ReviewScore += 2;
                            break;
                        case 3:
                            existingDriver.ReviewScore += 1;
                            break;
                        case 2:
                            existingDriver.ReviewScore -= 1;
                            break;
                        case 1:
                            existingDriver.ReviewScore -= 2;
                            break;
                        default: break;
                    }
                    context.Drivers.Update(existingDriver);
                    await context.SaveChangesAsync();
                    existingFeedback.CustomerId = feedbackUpdateDTO.CustomerId;
                    existingFeedback.GroupId = feedbackUpdateDTO.GroupId;
                    existingFeedback.Ratting = feedbackUpdateDTO.Ratting;
                    existingFeedback.BookingId = feedbackUpdateDTO.BookingId;
                    existingFeedback.IsDelete = feedbackUpdateDTO.IsDelete;
                    existingFeedback.Description = feedbackUpdateDTO.Description;
                    existingFeedback.DriverId = feedbackUpdateDTO.DriverId;
                    context.Feedbacks.Update(existingFeedback);
                    await context.SaveChangesAsync();
                    return new FeedbackUpdateDataDTO("update success", feedbackUpdateDTO, "success");
                }
            }
            catch (Exception e)
            {
                return new FeedbackUpdateDataDTO("fail", null, "update fail");
            }
        }
    }
}
