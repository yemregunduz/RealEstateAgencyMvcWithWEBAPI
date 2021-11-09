using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAgencyWEb.Controllers
{
    public class AuthController : Controller
    {
        IAuthService _authService;
        IHttpContextAccessor _httpContextAccessor;
        INotyfService _notyfService;
        public AuthController(IAuthService authService, IHttpContextAccessor httpContextAccessor, INotyfService notyfService)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _notyfService = notyfService;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                ViewBag.Message = userToLogin.Message;
                _notyfService.Success(userToLogin.Message, 5);
                return RedirectToAction("login", "auth");
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success == true)
            {
                _httpContextAccessor.HttpContext.Session.SetString("token", result.Data.Token);
                _httpContextAccessor.HttpContext.Session.SetInt32("userId", result.Data.UserId ?? default(int));
                _httpContextAccessor.HttpContext.Session.SetString("userType", result.Data.UserType);
                
                return RedirectToAction("index","cities");
            }
            return RedirectToAction("login","auth");
        }
    }
}
