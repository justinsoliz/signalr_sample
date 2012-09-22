using Voting.Dapper;

namespace Voting.Models
{
    public class Vote : DomainRecord<Vote>
    {
        public string Choice { get; set; }
    }
}