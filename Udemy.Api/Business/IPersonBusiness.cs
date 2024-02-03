using Udemy.Api.Models;

namespace Udemy.Api.Business
{
    public interface IPersonBusiness
    {
        List<Person> GetAll();
        Person GetById(int id);
        Person Insert(Person entity);
        Person Update(Person entity);
        void Delete(int id);
    }
}
