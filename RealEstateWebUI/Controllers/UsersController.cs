using Newtonsoft.Json;
using RealEstateAgencyMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstateAgencyMVC.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        private static string apiUrl = "https://localhost:44312/";
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            ListResponseModel<UserViewModel> users = new ListResponseModel<UserViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/users/getall");
                if (response.IsSuccessStatusCode)
                {
                    var userResponse = response.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<ListResponseModel<UserViewModel>>(userResponse);
                }

                return View(users.Data);
            }
        }
        [HttpPost]
       
        public async Task<ActionResult> AddUser(UserForRegisterDtoModel userForRegisterDtoModel)
        {
            using (var httpClient = new HttpClient())
            {
                userForRegisterDtoModel.Status = true;
                httpClient.BaseAddress = new Uri(apiUrl);
                if (userForRegisterDtoModel.Password == null)
                {
                    userForRegisterDtoModel.Password = "RealEstate54?";
                }

                var content = new StringContent(JsonConvert.SerializeObject(userForRegisterDtoModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("api/auth/register", content);
                var responseObject = JsonConvert.DeserializeObject<SingleResponseModel<UserForRegisterDtoModel>>(response.Content.ReadAsStringAsync().Result);
                if (responseObject.Success)
                {
                    TempData["Message"] = responseObject.Message;
                    return RedirectToAction("GetAllUsers", "Users");
                }
                else
                {
                    TempData["Message"] = responseObject.Message;
                    return RedirectToAction("GetAllUsers", "Users");
                }
            }
        }
        [HttpGet]
        public ViewResult AddUser()
        {
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                SingleResponseModel<UserViewModel> userViewModel = new SingleResponseModel<UserViewModel>();
                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync("api/users/getuserbyuserid?id="+id);
                if (response.IsSuccessStatusCode)
                {
                    userViewModel = JsonConvert.DeserializeObject<SingleResponseModel<UserViewModel>>(response.Content.ReadAsStringAsync().Result);
                }

                var content = new StringContent(JsonConvert.SerializeObject(userViewModel.Data), Encoding.UTF8, "application/json");

                HttpResponseMessage deleteResponse = await httpClient.PostAsync("api/users/delete", content);
                var responseObject = JsonConvert.DeserializeObject<ResponseModel>(deleteResponse.Content.ReadAsStringAsync().Result);
                if (responseObject.Success == true)
                {
                    return RedirectToAction("GetAllUsers", "Users");
                }
                return RedirectToAction("GetAllUsers", "Users");
            }
        }
        
        
    
    }
}