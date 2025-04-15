// ISaleOrderStatusService.cs
using EMIM.Models;

namespace EMIM.Services
{
    public interface ISaleOrderStatusService
{
    Task CreateInitialStatusAsync(int orderId, string statusName);
    Task<string> GetCurrentStatusAsync(int orderId);
    Task UpdateOrderStatusAsync(int orderId, string newStatus);
}
}