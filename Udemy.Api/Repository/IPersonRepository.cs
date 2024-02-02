using Udemy.Api.Models;

namespace Udemy.Api.Repository
{
    public interface IPersonRepository
    {
        List<Person> GetAll();
        Person GetById(int id);
        Person Insert(Person person);
        Person Update(Person person);
        void Delete(int id);
        bool Exists(int id);
    }
}
