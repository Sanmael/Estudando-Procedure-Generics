using BdOptions.Repositories;
using BdOptions.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdOptions.UOW
{
    public interface IUnitOfWork
    {
        public IPersonRepository PersonRepository { get; }
    }
}
