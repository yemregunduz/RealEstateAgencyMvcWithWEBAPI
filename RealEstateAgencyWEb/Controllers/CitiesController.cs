using Business.Abstract;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAgencyWEb.Controllers
{
    public class CitiesController : Controller
    {
        ICityService _cityService;
        IHttpContextAccessor _httpContextAccessor;
        public CitiesController(ICityService cityService, IHttpContextAccessor httpContextAccessor)
        {
            _cityService = cityService;
            _httpContextAccessor = httpContextAccessor;
        }
        public ViewResult Index()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("token");
            if (token!=null)
            {
                var userClaims =_httpContextAccessor.HttpContext.User.ClaimRoles();
                foreach (var userClaim in userClaims)
                {
                    if (userClaim=="admin")
                    {
                        
                    }
                }
                var result = _cityService.GetAll();
                return View(result.Data);
            }
            else
            {
                return View("~/Views/Auth/Login.cshtml");
            }
             
        }
    }
}
