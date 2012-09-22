using SignalR.Hubs;

namespace Voting.Models
{
    [HubName("VoteHub")]
    public class VoteHub : Hub
    {
        public void Update(string message)
        {
            Clients.updateVotes(message);
        }
    }
}