using FrontEnd2.Models;
using FrontEnd2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontEnd2.Controllers
{
    public class PersonController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var url = "https://localhost:44339/Person/GetPersonList";

                string result = await HttpService.GetAsync(url);

                List<PersonViewModel> personViewModelList = JsonSerializer.Deserialize<List<PersonViewModel>>(result);

                return View(personViewModelList);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }

        }
        public IActionResult CreateNewPerson()
        {
            return View();
        }
        public async Task<IActionResult> AddNewPerson(PersonViewModel personViewModel)
        {
            try
            {
                var url = "https://localhost:44339/Person/AddPerson";

                string result = await HttpService.PostAsync(url,personViewModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        public async Task<IActionResult> EditPersonView(long id)
        {
            var url = "https://localhost:44339/Person/GetPersonById";

            string result = await HttpService.GetAsync(url, id);

            PersonViewModel personViewModel = JsonSerializer.Deserialize<PersonViewModel>(result);

            return View(personViewModel);
        }

        public async Task<IActionResult> EditPerson(PersonViewModel personViewModel)
        {
            var url = "https://localhost:44339/Person/EditPerson";

            string result = await HttpService.PostAsync(url, personViewModel);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeletePersonView(long id)
        {
            var url = "https://localhost:44339/Person/GetPersonById";

            string result = await HttpService.GetAsync(url, id);

            PersonViewModel personViewModel = JsonSerializer.Deserialize<PersonViewModel>(result);

            return View(personViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var url = "https://localhost:44339/Person/DeletePerson";

            string result = await HttpService.GetAsync(url, id);

            return RedirectToAction("Index");
        }

    }
}
