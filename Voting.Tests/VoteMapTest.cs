using System;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using Ninject.Extensions.Conventions;
using NUnit.Framework;
using Voting.Dapper;
using Voting.Models;

namespace Voting.Tests
{
    [TestFixture]
    public class VoteMapTest
    {
        [SetUp]
        public void Setup()
        {
            
           var kernel = Container.Init();

            kernel.Bind(x => x
                 .FromAssembliesMatching("*")
                 .SelectAllClasses()
                 .BindDefaultInterface());


            kernel.Bind(typeof(IDao<>)).To(typeof(DapperDao<>));
        }

        [Test]
        public void It_should_save_and_read_a_vote()
        {
            // Given a vote
            var expectedVote = new Vote { Choice = "1" };

            // When I save the vote
            expectedVote.Save();

            // And I read it from the db
            Vote actualVote = Vote.Get(expectedVote.Id);
            
            // Then it should equal the expected vote
            actualVote.ShouldEqual(expectedVote);

            // And when I delete it 
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[GlobalEnv.ConnectionString].ConnectionString))
            {
                string query = string.Format("Delete from Vote where Id = {0}", actualVote.Id);

                Console.WriteLine("Executing: {0}", query);

                conn.Open();

                conn.Execute(query);

                conn.Close();
            }
        }
    }
}
