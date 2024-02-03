using Udemy.Api.Models;

namespace Udemy.Api.Business
{
    public interface IBookBusiness
    {
        List<Book> GetAll();
        Book GetById(int id);
        Book Insert(Book entity);
        Book Update(Book entity);
        void Delete(int id);
    }
}
