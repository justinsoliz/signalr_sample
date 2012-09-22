using System;
using System.Web.Mvc;
using SignalR;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;
using Voting.Models;

namespace Voting.Controllers
{
    public class HomeController : TwilioController
    {
        public ActionResult Index()
        {
            var votes = Vote.All();

            return View(votes);
        }

        public ActionResult Sms(TwilioRequest request)
        {
            var vote = new Vote { Choice = request.Body };
            vote.Save();

            var context = GlobalHost.ConnectionManager.GetHubContext<VoteHub>();

            context.Clients.updateVotes(
                string.Format("Vote Choice: {0}", vote.Choice));

            var response = new TwilioResponse();
            response.Sms("Thanks for voting!");

            return new TwiMLResult(response);
        }

        public ActionResult Test(string message)
        {
            var voteHub = GlobalHost.ConnectionManager.GetHubContext<VoteHub>();

            voteHub.Clients.updateVotes("Received");

            return View();
        }
    }
}
