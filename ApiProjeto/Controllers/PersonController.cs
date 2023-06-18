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
        public ActionResult AddPerson([FromBody] PersonModel personModel)
        {
            try
            {
                bool create = _personService.AddNewPerson(personModel);

                return Ok(create);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPost]
        public ActionResult EditPerson([FromBody] PersonModel personModel)
        {
            ApiResponse apiResponse = new ApiResponse();

            try
            {
                bool edit = _personService.EditPerson(personModel);

                apiResponse = new ApiResponse(edit, personModel);

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        [HttpGet("{id}")]
        public ActionResult DeletePerson(long id)
        {
            ApiResponse apiResponse = null;

            try
            {

                PersonModel personModel = _personService.GetPersonById(id);

                if (personModel.PersonId == 0)
                {
                    throw new Exception("Usuario não encontrado!");
                }

                _personService.DeletePerson(personModel);

                apiResponse = new ApiResponse(true, personModel);

                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse(false, ex.Message);
                return Ok(apiResponse);
            }
        }
        [HttpGet("{id}")]
        public ActionResult GetPersonById(long id)
        {
            ApiResponse apiResponse = null;

            try
            {

                PersonModel personModel = _personService.GetPersonById(id);

                if (personModel.PersonId == 0)
                {
                    throw new Exception("Usuario não encontrado!");
                }

                apiResponse = new ApiResponse(true, personModel);

                return Ok(apiResponse);
            }

            catch (Exception ex)
            {
                apiResponse = new ApiResponse(false, ex.Message);
                return Ok(apiResponse);
            }
        }
        [HttpGet]
        public ActionResult GetPersonList()
        {
            try
            {
                ApiResponse apiResponse = null;

                List<PersonModel> personModel = _personService.GetPersonList();

                if (personModel.Count < 0)
                    apiResponse = new ApiResponse(false, personModel);

                apiResponse = new ApiResponse(true, personModel);

                return Ok(apiResponse);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
