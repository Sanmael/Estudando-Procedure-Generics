using BdOptions.AppDataBase;
using BdOptions.Repositories;
using BdOptions.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdOptions.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly AppDbContext _appDbContext;
       
        private readonly ProcedureConcret _procedureConcret;

        public UnitOfWork(/*AppDbContext appDbContext, */IConfiguration configuration)
        {
            //_appDbContext = appDbContext;
            _procedureConcret = new ProcedureConcret(configuration);
        }

        public IPersonRepository PersonRepository
        {
            get
            {
                return new PersonRepository(_procedureConcret, _procedureConcret);
            }
        }
    }
}
