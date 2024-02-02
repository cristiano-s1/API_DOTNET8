using Udemy.Api.Models;

namespace Udemy.Api.Business
{
    public interface IPersonBusiness
    {
        List<Person> GetAll();
        Person GetById(int id);
        Person Create(Person person);
        Person Update(Person person);
        void Delete(int id);
    }
}
