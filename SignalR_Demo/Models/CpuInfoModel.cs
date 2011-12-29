using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_Demo.Models
{
    public class CPUInfoModel
         : List<CPUInfoItem>
    {
    }

    public class CPUInfoItem
    {
        public string DateString { get; set; }
        public double CPUUsage { get; set; }
        public double MemUsage { get; set; }
    }
}