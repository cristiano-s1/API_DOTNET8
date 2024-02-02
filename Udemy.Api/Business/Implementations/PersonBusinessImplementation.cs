using Udemy.Api.Models;
using Udemy.Api.Repository;

namespace Udemy.Api.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        public List<Person> GetAll()
        {
            return _repository.GetAll();
        }

        public Person GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Insert(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
