using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTest.hubs
{
    public class TestHub : Hub
    {        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task NotifyAll() 
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 500000; i++)
            {
                await Clients.All.SendAsync("ReceiveMessage", "trading engine", $"oferta ha cambiado {i}");
            }
            sw.Stop();
            var total = Convert.ToDecimal(sw.ElapsedMilliseconds) / 1000m;
            await Clients.All.SendAsync("ReceiveMessage", "trading engine", "rafaga terminada en: " + total + " segs ");
        }
    }
}
