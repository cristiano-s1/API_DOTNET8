using Udemy.Api.Data.Converter.Implementations;
using Udemy.Api.Data.VO;
using Udemy.Api.Hypermedia.Utils;
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

        //HATEOAS
        public PagedSearchVO<BookVO> FindWithPagedSearch(string title, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM books p WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                query += $" AND p.title LIKE '%{title}%' ";
            }

            query += $" ORDER BY p.title {sort} OFFSET {offset} ROWS FETCH NEXT {size} ROWS ONLY";

            string countQuery = @"SELECT COUNT(*) FROM books p WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                countQuery += $" AND p.title LIKE '%{title}%' ";
            }

            var books = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}
