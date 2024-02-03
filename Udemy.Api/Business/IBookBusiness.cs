using Udemy.Api.Data.VO;
using Udemy.Api.Models;

namespace Udemy.Api.Business
{
    public interface IBookBusiness
    {
        List<BookVO> GetAll();
        BookVO GetById(int id);
        BookVO Insert(BookVO entity);
        BookVO Update(BookVO entity);
        void Delete(int id);
    }
}
