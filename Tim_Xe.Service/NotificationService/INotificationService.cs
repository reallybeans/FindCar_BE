using System.Threading.Tasks;
using Tim_Xe.Data.Models;

namespace Tim_Xe.Service.NotificationService
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
    }
}
