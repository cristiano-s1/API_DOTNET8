using Udemy.Api.Data.Converter.Implementations;
using Udemy.Api.Data.VO;
using Udemy.Api.Models;
using Udemy.Api.Repository.Generic;

namespace Udemy.Api.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository)
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
    }
}
