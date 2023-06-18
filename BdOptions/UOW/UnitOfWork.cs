using BdOptions.AppDataBase;
using BdOptions.Repositories;
using BdOptions.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdOptions.UOW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ProcedureConcret _procedureConcret;

        public UnitOfWork()
        {
            _procedureConcret = new ProcedureConcret(GetConfiguration().GetConnectionString("DefaultConnection"));
        }
        private IConfigurationRoot GetConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();

            return configuration;
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
