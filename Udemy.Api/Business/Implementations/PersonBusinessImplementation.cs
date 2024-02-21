using Udemy.Api.Data.Converter.Implementations;
using Udemy.Api.Data.VO;
using Udemy.Api.Hypermedia.Utils;
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

        //HATEOAS
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM Persons p WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(name))
            {
                query += $" AND p.FirstName LIKE '%{name}%' ";
            }

            query += $" ORDER BY p.FirstName {sort} OFFSET {offset} ROWS FETCH NEXT {size} ROWS ONLY";

            string countQuery = @"SELECT COUNT(*) FROM Persons p WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(name))
            {
                countQuery += $" AND p.FirstName LIKE '%{name}%' ";
            }

            var persons = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}
