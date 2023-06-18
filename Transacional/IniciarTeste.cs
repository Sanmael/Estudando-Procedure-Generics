using BdOptions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacional
{
    
    public class IniciarTeste
    {
        private readonly BdOptions.UOW.UnitOfWork _personRepository;

        public IniciarTeste(BdOptions.UOW.UnitOfWork personRepository)
        {
            _personRepository = personRepository;
        }
        public void ObterPessoar()
        {
            _personRepository.PersonRepository.GetPersonList();
        }
    }
}
