using Newtonsoft.Json;
using RealEstateAgencyMVC.Models;
using RealEstateWebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RealEstateAgencyMVC.Controllers
{
    public class AuthController : Controller
    {
        
        // GET: Auth
        private static string apiUrl = "https://localhost:44312/";
        [HttpPost]

        public async Task<ActionResult> Login(UserForLoginDtoModel userForLoginDtoModel)
        {
            var tokenModel = new SingleResponseModel<TokenModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrl);

                var content = new StringContent(JsonConvert.SerializeObject(userForLoginDtoModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("api/auth/login", content);
                var responseObject = JsonConvert.DeserializeObject<SingleResponseModel<TokenModel>>(response.Content.ReadAsStringAsync().Result);
                if (responseObject.Success == true)
                {
                    tokenModel = responseObject;
                    Session["token"] = tokenModel.Data.Token;
                    Session["userType"] = tokenModel.Data.UserType;
                    Session["userId"] = tokenModel.Data.UserId;
                    if (tokenModel.Data.UserType.Contains("admin"))
                    {
                        return RedirectToAction("GetAllUsers", "Users");
                    }
                    return RedirectToAction("Index", "realestateclassifieds");
                }
                else
                {
                    TempData["Message"] = responseObject.Message;
                    return RedirectToAction("login", "auth");
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> Register(UserForRegisterDtoModel userForRegisterDtoModel)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrl);
                if (userForRegisterDtoModel.Password==null)
                {
                    userForRegisterDtoModel.Password = "Darkteam54?";
                }
                var content = new StringContent(JsonConvert.SerializeObject(userForRegisterDtoModel), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("api/auth/register", content);
                var responseObject = JsonConvert.DeserializeObject<SingleResponseModel<UserForRegisterDtoModel>>(response.Content.ReadAsStringAsync().Result);
                if (responseObject.Success)
                {
                    TempData["Message"] = responseObject.Message;
                    return RedirectToAction("register", "auth");
                }
                else
                {
                    TempData["Message"] = responseObject.Message;
                    return RedirectToAction("register", "auth");
                }
            }

        }
        public ActionResult Unauthorized()
        {
            return View("Unauthorized");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            if (Session.Count==0)
            {
                return RedirectToAction("login", "auth");
            }
            return null;
        }
        public ViewResult Login()
        {
            return View();
        }
        public ViewResult Register()
        {
            return View();
        }
        [HttpGet]
        public ViewResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(UserForChangePasswordDto userForChangePasswordDto)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrl);
                var content = new StringContent(JsonConvert.SerializeObject(userForChangePasswordDto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync("api/auth/changepassword", content);
                var responseObject = JsonConvert.DeserializeObject<SingleResponseModel<UserForRegisterDtoModel>>(response.Content.ReadAsStringAsync().Result);
                if (responseObject.Success)
                {
                    TempData["Message"] = responseObject.Message;
                    return RedirectToAction("changepassword", "auth");
                }
                else
                {
                    TempData["Message"] = responseObject.Message;
                    return RedirectToAction("changepassword", "auth");
                }
            }

        }

    }
}