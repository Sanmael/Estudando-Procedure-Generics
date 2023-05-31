using ApiProjeto.DTos;
using ApiProjeto.Service;
using AutoMapper;
using BdOptions.UOW;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProjeto.Controllers
{
    [Route("{Controller}/{Action}")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public void AddPerson(PersonModel personModel)
        {
            try
            {
                _personService.AddNewPerson(personModel);
            }
            catch (Exception ex)
            {

            }
        }
        [HttpPost]
        public void EditPerson(PersonModel personModel)
        {
            try
            {                
                _personService.EditPerson(personModel);
            }
            catch (Exception ex)
            {

            }
        }
        [HttpGet("{id}")]
        public void DeletePerson(long id)
        {
            try
            {
                PersonModel personModel = _personService.GetPersonById(id);

                _personService.DeletePerson(personModel);
            }
            catch (Exception ex)
            {

            }
        }
        [HttpGet("{id}")]
        public ActionResult GetPersonById(long id)
        {
            try
            {
                PersonModel personModel = _personService.GetPersonById(id);

                return Ok(personModel);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetPersonList()
        {
            try
            {
                List<PersonModel> personModel = _personService.GetPersonList();

                return Ok(personModel);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
