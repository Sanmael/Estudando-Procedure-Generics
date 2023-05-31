using BdOptions.AppDataBase;
using BdOptions.Repositories.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdOptions.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IGetRepository<Person> _getRepository;

        public PersonRepository(IRepository<Person> personRepository, IGetRepository<Person> getRepository)
        {
            _personRepository = personRepository;
            _getRepository = getRepository;
        }

        public void InsetPerson(Person person)
        {
            _personRepository.Insert(person);
        }
        public void DeletePerson(Person person)
        {
            _personRepository.Delete(person);
        }
        public Person GetPersonById(long id)
        {
            return _getRepository.GetEntity(id);
        }
        public Person GetPersonByTaxNumber(string taxNumber)
        {
            return _getRepository.Get(x => x.TaxNumber.Equals(taxNumber));
        }
        public List<Person> GetPersonList()
        {
            return _getRepository.GetAll().ToList();
        }
        public void EditPerson(Person person)
        {
            _personRepository.Edit(person);
        }
    }
}
