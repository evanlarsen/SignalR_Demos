using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalR_Demo.Infrastructure.Attributes;

namespace SignalR_Demo.Controllers
{
    [ELMAHHandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChatRoom()
        {
            return View();
        }

        public ActionResult StockQuotes()
        {
            return View();
        }

        public ActionResult TicTacToe()
        {
            return View();
        }
    }
}
