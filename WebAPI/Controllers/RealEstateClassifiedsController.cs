using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateClassifiedsController : ControllerBase
    {
        IRealEstateClassifiedService _realEstateClassifiedService;
        public RealEstateClassifiedsController(IRealEstateClassifiedService realEstateClassifiedService)
        {
            _realEstateClassifiedService = realEstateClassifiedService;
        }
        [HttpPost("add")]
        public IActionResult Add(RealEstateClassified realEstateClassified)
        {
            var result = _realEstateClassifiedService.Add(realEstateClassified);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(RealEstateClassified realEstateClassified)
        {
            var result = _realEstateClassifiedService.Delete(realEstateClassified);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(RealEstateClassified realEstateClassified)
        {
            var result = _realEstateClassifiedService.Update(realEstateClassified);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("changerealestateclassifiedstatus")]
        public IActionResult ChangeRealEstateClassifiedStatus(RealEstateClassified realEstateClassified)
        {
            var result = _realEstateClassifiedService.ChangeRealEstateClassifiedStatus(realEstateClassified);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getallrealestateclassifieddetailsdtobyuseridandstatus")]
        public IActionResult GetAllRealEstateClassifiedDetailsDtoByUserIdAndStatus(int userId,bool status)
        {
            var result = _realEstateClassifiedService.GetAllRealEstateClassifiedDetailsDtoByUserIdAndStatus(userId, status);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getallrealestateclassifieddetailsdtobyuserid")]
        public IActionResult GetAllRealEstateClassifiedDetailsDtoByUserId(int userId)
        {
            var result = _realEstateClassifiedService.GetAllRealEstateClassifiedDetailsDtoByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getrealestateclassifiedbyid")]
        public IActionResult GetRealEstateClassifiedById(int id)
        {
            var result = _realEstateClassifiedService.GetRealEstateClassifiedById(id);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("getallrealestateclassifieddetailsdtobyfilterparams")]
        public IActionResult GetAllRealEstateClassifiedDetailsDtoByFilterParams(SearchFilterForRealEstateClassified searchFilterForRealEstateClassified)
        {
            var result = _realEstateClassifiedService.GetAllRealEstateClassifiedDetailsDtoBySearchParametrs(searchFilterForRealEstateClassified);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
