using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_Demo.Models
{
    public class Player
    {
        public string id { get; set; }
        public ICollection<string> Moves { get; set; }
    }
}