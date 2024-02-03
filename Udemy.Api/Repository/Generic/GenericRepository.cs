using Microsoft.EntityFrameworkCore;
using Udemy.Api.Context;
using Udemy.Api.Models;

namespace Udemy.Api.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected UdemyContext _context;
        private readonly DbSet<T> dataset;

        public GenericRepository(UdemyContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }

        public List<T> GetAll()
        {
            return dataset.ToList();
        }

        public T GetById(int id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Insert(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return null;
            }
        }

        public void Delete(int id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Exists(int id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }
    }
}
