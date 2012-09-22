
namespace Voting.Models
{
    public class TwilioRequest
    {
        public string SmsSid { get; set; }
        public string AccountSid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }

    }
}
