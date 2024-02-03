using Udemy.Api.Data.VO;
using Udemy.Api.Models;

namespace Udemy.Api.Business
{
    public interface IPersonBusiness
    {
        List<PersonVO> GetAll();
        PersonVO GetById(int id);
        PersonVO Insert(PersonVO entity);
        PersonVO Update(PersonVO entity);
        void Delete(int id);
    }
}
