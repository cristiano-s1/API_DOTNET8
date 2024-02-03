using Udemy.Api.Data.Converter.Implementations;
using Udemy.Api.Data.VO;
using Udemy.Api.Models;
using Udemy.Api.Repository.Generic;

namespace Udemy.Api.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> GetAll()
        {
            return _converter.Parse(_repository.GetAll());
        }

        public BookVO GetById(int id)
        {
            return _converter.Parse(_repository.GetById(id));
        }

        public BookVO Insert(BookVO entity)
        {
            Book book = _converter.Parse(entity);
            book = _repository.Insert(book); 

            return _converter.Parse(book);
        }

        public BookVO Update(BookVO entity)
        {
            Book book = _converter.Parse(entity);
            book = _repository.Update(book);

            return _converter.Parse(book);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
