using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdOptions.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public void InsetPerson(Person person);
        public void EditPerson(Person person);

        public void DeletePerson(Person person);

        public Person GetPersonById(long id);

        public Person GetPersonByTaxNumber(string taxNumber);

        public List<Person> GetPersonList();

    }
}
