using Microsoft.AspNetCore.SignalR;

namespace HospitalQueueMS.Hubs
{
    public class WaitingRoomHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            if (httpContext.User.IsInRole("Reception"))
                await Groups.AddToGroupAsync(Context.ConnectionId, "Reception");

            if (httpContext.User.IsInRole("Admin"))
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admin");

            if (httpContext.User.IsInRole("Doctor"))
            {
                var clinicId = httpContext.Request.Query["clinicId"];
                if (!string.IsNullOrEmpty(clinicId))
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"Clinic_{clinicId}");
                }
            }

            await base.OnConnectedAsync();
        }


    }
}
