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

            try
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<VoteHub>();

                context.Clients.update(
                    string.Format("Vote Choice: {0}", vote.Choice));
            }
            catch (Exception ex)
            {
                return TwiML(new TwilioResponse().Sms(ex.Message));
            }


            var response = new TwilioResponse();
            response.Sms("Thanks for voting!");

            return new TwiMLResult(response);
        }

        public ActionResult Test(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<VoteHub>();
            context.Clients.update(message);

            return View();
        }
    }
}
