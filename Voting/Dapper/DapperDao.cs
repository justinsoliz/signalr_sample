using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using DapperExtensions;

namespace Voting.Dapper
{
    public class DapperDao<TModel> : IDao<TModel> where TModel : DomainRecord<TModel>
    {
        protected readonly string ConnString =
            ConfigurationManager.ConnectionStrings[GlobalEnv.ConnectionString].ConnectionString;

        public void Save(TModel model)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();

                model.Id = conn.Insert(model);

                conn.Close();
            }
        }

        public void Update(TModel model)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();

                model.UpdatedAt = DateTime.UtcNow;

                conn.Update(model);

                conn.Close();
            }
        }

        public virtual TModel Get(long id)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();

                var result = conn.Get<TModel>(id);

                conn.Close();

                return result;
            }
        }

        public IList<TModel> All()
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();

                var results = conn.GetList<TModel>().ToList();

                conn.Close();

                return results;
            }
        }
    }
}
