
using ApiProjeto.DTos;
using AutoMapper;
using BdOptions.AppDataBase;
using BdOptions.UOW;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProjeto.Service
{
    public class PersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public List<PersonModel> GetPersonList()
        {
            return _unitOfWork.PersonRepository.GetPersonList().Select(x => _mapper.Map<PersonModel>(x)).ToList();
        }

        public PersonModel GetPersonById(long id)
        {
            try
            {
                Person person = _unitOfWork.PersonRepository.GetPersonById(id);

                if (person == null)
                    throw new Exception("Person não Encontrada");

                return _mapper.Map<PersonModel>(person);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public bool AddNewPerson(PersonModel personModel)
        {
            try
            {
                Person person = _mapper.Map<Person>(personModel);

                if (person == null)
                    throw new Exception("Person não Encontrada");

                _unitOfWork.PersonRepository.InsetPerson(person);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public bool EditPerson(PersonModel personModel)
        {
            try
            {
                Person person = _mapper.Map<Person>(personModel);

                if (person == null)
                    throw new Exception("Person não Encontrada");

                _unitOfWork.PersonRepository.EditPerson(person);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
        public bool DeletePerson(PersonModel personModel)
        {
            try
            {
                Person person = _mapper.Map<Person>(personModel);

                if (person == null)
                    throw new Exception("Person não Encontrada");

                _unitOfWork.PersonRepository.DeletePerson(person);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
