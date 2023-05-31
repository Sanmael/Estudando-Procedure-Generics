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
        private readonly IRepository _personRepository;
        private readonly IGetRepository _getRepository;

        public PersonRepository(IRepository personRepository, IGetRepository getRepository)
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
            _personRepository.Delete<Person>(person.PersonId);
        }
        public Person GetPersonById(long id)
        {
            return _getRepository.GetEntityById<Person>(id);
        }
        public Person GetPersonByTaxNumber(string taxNumber)
        {
            return _getRepository.Get<Person>(x => x.TaxNumber.Equals(taxNumber));
        }
        public List<Person> GetPersonList()
        {
            return _getRepository.GetAll<Person>().ToList();
        }
        public List<Person> GetPersonListByName(string name)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("@PersonName", name);
            return _getRepository.GetAll<Person>(keyValuePairs, "GetAllPersonByName").ToList();
        }
        public void EditPerson(Person person)
        {
            _personRepository.Edit(person);
        }
    }
}
