using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class IniciarTeste
    {
        private readonly BdOptions.UOW.IUnitOfWork _unitOfWork;
        public IniciarTeste(BdOptions.UOW.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void ObterPessoar()
        {
            _unitOfWork.PersonRepository.GetPersonList();
        }
    }
}
