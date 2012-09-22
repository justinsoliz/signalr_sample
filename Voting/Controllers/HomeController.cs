using System.Collections.Generic;
using System.Web.Mvc;
using Voting.Models;

namespace Voting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var votes = Vote.All();

            return View(votes);
        }
    }
}
