using Udemy.Api.Data.VO;
using Udemy.Api.Hypermedia.Utils;

namespace Udemy.Api.Business
{
    public interface IBookBusiness
    {
        List<BookVO> GetAll();
        BookVO GetById(int id);
        BookVO Insert(BookVO entity);
        BookVO Update(BookVO entity);
        void Delete(int id);

        //HATEOAS
        PagedSearchVO<BookVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
