using Udemy.Api.Models;
using Udemy.Api.Repository.Generic;

namespace Udemy.Api.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public List<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public Book GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Book Insert(Book entity)
        {
            return _repository.Insert(entity);
        }

        public Book Update(Book entity)
        {
            return _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
