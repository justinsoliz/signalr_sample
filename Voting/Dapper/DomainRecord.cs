using System;
using System.Collections.Generic;
using Voting.Helpers;

namespace Voting.Dapper
{

    public abstract class DomainRecord<TModel> where TModel : class
    {
        protected DomainRecord()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        protected bool Equals(DomainRecord<TModel> other)
        {
            return Id == other.Id && 
                CreatedAt.SortaEquals(other.CreatedAt) &&
                UpdatedAt.SortaEquals(other.UpdatedAt);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DomainRecord<TModel>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ CreatedAt.GetHashCode();
                hashCode = (hashCode*397) ^ UpdatedAt.GetHashCode();
                return hashCode;
            }
        }

        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void Save()
        {
            WithDao(d => d.Save(this as TModel));
        }

        public void Update()
        {
            WithDao(d => d.Update(this as TModel));
        }

        public static TModel Get(long id)
        {
            return ReturnWithDao(d => d.Get(id));
        }

        public static IList<TModel> All()
        {
            return ReturnWithDao(d => d.All());
        }

        private static void WithDao(Action<IDao<TModel>> action)
        {
            var dao = Container.Get<IDao<TModel>>();
            action.Invoke(dao);
        }

        private static TResult ReturnWithDao<TResult>(Func<IDao<TModel>, TResult> func)
        {
            var dao = Container.Get<IDao<TModel>>();
            var result = func.Invoke(dao);
            return result;
        }
    }
}