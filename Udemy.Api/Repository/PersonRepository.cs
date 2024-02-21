using Udemy.Api.Context;
using Udemy.Api.Models;
using Udemy.Api.Repository.Generic;

namespace Udemy.Api.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(UdemyContext context) : base(context) { }

        public Person Disable(int id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) 
                return null;

            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (user != null)
            {
                user.Enabled = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return user;
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(x => x.FirstName.Contains(firstName) && x.LastName.Contains(lastName)).ToList();

            }
            
            if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(x => x.LastName.Contains(lastName)).ToList();

            }
            
            if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(x => x.FirstName.Contains(firstName)).ToList();
            }

            return null;
        }
    }
}
