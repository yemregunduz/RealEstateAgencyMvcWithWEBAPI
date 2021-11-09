using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAgencyWEb.Controllers
{
    public class RealEstateClassifiedsController : Controller
    {
        IRealEstateClassifiedService _realEstateClassifiedService;
        public RealEstateClassifiedsController(IRealEstateClassifiedService realEstateClassifiedService)
        {
            _realEstateClassifiedService = realEstateClassifiedService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
