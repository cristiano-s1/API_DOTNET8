using Udemy.Api.Models;

namespace Udemy.Api.Repository.Generic
{
    public interface IRepository <T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(int id);
        T Insert(T Item);
        T Update(T Item);
        void Delete(int id);
        bool Exists(int id);
    }
}
