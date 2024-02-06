using Udemy.Api.Data.Converter.Implementations;
using Udemy.Api.Data.VO;
using Udemy.Api.Models;
using Udemy.Api.Repository;

namespace Udemy.Api.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> GetAll()
        {
            return _converter.Parse(_repository.GetAll());
        }

        public PersonVO GetById(int id)
        {
            return _converter.Parse(_repository.GetById(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PersonVO Insert(PersonVO entity)
        {
            Person person = _converter.Parse(entity);
            person = _repository.Insert(person);

            return _converter.Parse(person);
        }

        public PersonVO Update(PersonVO entity)
        {
            Person person = _converter.Parse(entity);
            person = _repository.Update(person);

            return _converter.Parse(person);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }
    }
}
