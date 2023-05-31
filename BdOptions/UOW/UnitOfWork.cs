using BdOptions.AppDataBase;
using BdOptions.Repositories;
using BdOptions.Repositories.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdOptions.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        private readonly IPersonRepository _personRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //ProcedureConcret<Person> procedureConcret = new ProcedureConcret<Person>();
        
        public IPersonRepository PersonRepository
        {
            get
            {
                return _personRepository ?? new PersonRepository(new SqlConcret<Person>(_appDbContext), new SqlConcret<Person>(_appDbContext));
            }
        }
    }
}
