using System.Collections.Generic;

namespace Voting.Dapper
{
    public interface IDao<TModel>
    {
        void Save(TModel model);
        void Update(TModel model);
        TModel Get(long id);
        IList<TModel> All();
    }
}
