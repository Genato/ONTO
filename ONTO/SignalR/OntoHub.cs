using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONTO.SignalR
{
    public class OntoHub : Hub
    {
        public void ShowNotificationBar()
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<OntoHub>();
            hubContext.Clients.All.showNotificationBar();
        }
    }
}