using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace HospitalQueueMS.Hubs
{
    public class QueueHub : Hub
    {
        
        public async Task UpdateQueue(string message)
        {
            await Clients.All.SendAsync("ReceiveQueueUpdate", message);
        }
    }
}
