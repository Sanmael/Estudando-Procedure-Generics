using BdOptions;
using BdOptions.Repositories;
using BdOptions.UOW;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class TestandoClasses
    {
        ProcedureConcret _sqlConcret;
        public TestandoClasses(string connectionString)
        {
            _sqlConcret = new ProcedureConcret(connectionString);
        }

        public List<Person> ObterPessoas()
        {
            PersonRepository personRepository = new PersonRepository(_sqlConcret, _sqlConcret);

            return personRepository.GetPersonList();
        }
    }
}
