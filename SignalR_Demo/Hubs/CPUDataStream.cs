using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR;
using System.Threading;
using System.Diagnostics;
using SignalR_Demo.Models;

namespace SignalR_Demo.Hubs
{
    public class CPUDataStream : PersistentConnection
    {
        protected override System.Threading.Tasks.Task OnConnectedAsync(HttpContextBase context, string clientId)
        {
            // this is used for the live stock quotes.. a constant stream
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var connection = SignalR.Connection.GetConnection<CPUDataStream>();
                //var counter = new PerformanceCounter();
                //counter.CategoryName = "Processor";
                //counter.CounterName = "% Processor Time";
                //counter.InstanceName = "_Total";

                //var memCounter = new PerformanceCounter();
                //memCounter.CategoryName = "Memory";
                //memCounter.CounterName = "Available MBytes";


                while (true)
                {
                    //var item = new CPUInfoItem()
                    //{
                    //    DateString = DateTime.Now.ToString("hh:mm:ss"),
                    //    CPUUsage = Math.Round(counter.NextValue(), 2),
                    //    MemUsage = memCounter.NextValue()
                    //};

                    var item = new CPUInfoItem()
                    {
                        DateString = DateTime.Now.ToString("hh:mm:ss"),
                        CPUUsage = new Random().Next(0, 60),
                        MemUsage = new Random().Next(0, 20),
                    };

                    connection.Broadcast(item);

                    Thread.Sleep(500);
                }
            });
            
            return base.OnConnectedAsync(context, clientId);
        }
    }
}