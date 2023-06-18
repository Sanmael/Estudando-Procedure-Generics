using FrontEnd2.Models;
using FrontEnd2.Services;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CarregarTeste()
        {
            List<PersonViewModel> personViewModel = new List<PersonViewModel>();

            try
            {
                if (Request.Cookies.TryGetValue("PersonWallet", out string results))
                {
                    ApiResponse apiResponse = JsonSerializer.Deserialize<ApiResponse>(results);

                    personViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonViewModel>>(apiResponse.Data.ToString());

                    return Json(new { data = personViewModel });
                }

                var url = "https://localhost:44339/Person/GetPersonList";

                string result = await HttpService.GetAsync(url);

                ApiResponse apiResponses = JsonSerializer.Deserialize<ApiResponse>(result);


                if (apiResponses.Sucess)
                {
                    personViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonViewModel>>(apiResponses.Data.ToString());

                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(2)
                    };

                    Response.Cookies.Append("PersonWallet", result, cookieOptions);

                    return Json(new { data = personViewModel });
                }

                return Json(new { data = personViewModel });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        public IActionResult ResetCache()
        {
            if (Request.Cookies.TryGetValue("PersonWallet", out string results))
            {
                Response.Cookies.Delete("PersonWallet");

                return Json(new { success = true });
            }

            return Json(new { success = false });

        }
        public IActionResult CreateNewPerson()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewPerson(PersonViewModel personViewModel)
        {
            try
            {
                var url = "https://localhost:44339/Person/AddPerson";

                string result = await HttpService.PostAsync(url, personViewModel);

                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        public async Task<IActionResult> EditPersonView(long id)
        {
            var url = "https://localhost:44339/Person/GetPersonById";

            try
            {
                string result = await HttpService.GetAsync(url, id);

                ApiResponse apiResponses = JsonSerializer.Deserialize<ApiResponse>(result);

                if (apiResponses.Sucess)
                {
                    PersonViewModel personViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonViewModel>(apiResponses.Data.ToString());
                    return View(personViewModel);
                }
                else
                    throw new Exception(apiResponses.Data.ToString());
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerson(PersonViewModel personViewModel)
        {
            try
            {
                var url = "https://localhost:44339/Person/EditPerson";

                string result = await HttpService.PostAsync(url, personViewModel);

                ApiResponse apiResponses = JsonSerializer.Deserialize<ApiResponse>(result);

                return Json(new { success = apiResponses.Sucess });
            }
            catch (Exception ex)
            {
                return Json(new { success = ex.Message });
            }

        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonDetails(long id)
        {
            try
            {
                var url = "https://localhost:44339/Person/GetPersonById";

                string result = await HttpService.GetAsync(url, id);

                ApiResponse apiResponses = JsonSerializer.Deserialize<ApiResponse>(result);

                PersonViewModel personViewModel = null;

                if (apiResponses.Sucess)
                {
                    personViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<PersonViewModel>(apiResponses.Data.ToString());                    
                }                    

                return Json(new { success = true, data = personViewModel });
            }
            catch (Exception ex)
            {
                return Json(new { success = ex.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePerson(long id)
        {
            try
            {
                var url = "https://localhost:44339/Person/DeletePerson";

                string result = await HttpService.GetAsync(url, id);

                ApiResponse apiResponses = JsonSerializer.Deserialize<ApiResponse>(result);

                if (apiResponses.Sucess)
                    return Json(new { success = apiResponses.Sucess });

                else
                    throw new Exception(apiResponses.Data.ToString());

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }

        }

    }
}
