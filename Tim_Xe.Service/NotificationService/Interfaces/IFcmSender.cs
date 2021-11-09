using System.Threading;
using System.Threading.Tasks;
using Tim_Xe.Service.NotificationService.Google;

namespace Tim_Xe.Service.NotificationService.Interfaces
{
    public interface IFcmSender
    {
        Task<FcmResponse> SendAsync(string deviceId, object payload, CancellationToken cancellationToken = default);
        Task<FcmResponse> SendAsync(object payload, CancellationToken cancellationToken = default);
    }
}
