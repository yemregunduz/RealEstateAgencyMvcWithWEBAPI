using Business.Abstract;
using Core.Extensions;
using Entities.Concrete;
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
    public class RealEstateClassifiedImagesController : ControllerBase
    {
        IRealEstateClassifiedImageService _realEstateClassifiedImageService;
        public RealEstateClassifiedImagesController(IRealEstateClassifiedImageService realEstateClassifiedImageService)
        {
            _realEstateClassifiedImageService = realEstateClassifiedImageService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("realEsateClassifiedImage"))] IFormFile file, [FromForm] RealEstateClassifiedImage realEstateClassifiedImage)
        {
            var result = _realEstateClassifiedImageService.Add(file, realEstateClassifiedImage);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("id"))] int Id)
        {
            var realEstateClassifiedImage = _realEstateClassifiedImageService.Get(Id).Data;
            var result = _realEstateClassifiedImageService.Delete(realEstateClassifiedImage);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("realEsateClassifiedImage"))] IFormFile file,[FromForm(Name =("imageId"))] int Id)
        {
            var realEsateClassifiedImage = _realEstateClassifiedImageService.Get(Id).Data;
            var result = _realEstateClassifiedImageService.Update(file, realEsateClassifiedImage);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getallrealestateclassifiedimagesbyrealestateclassifiedid")]
        public IActionResult GetAllUserImagesByUserId(int realEstateClassifiedId)
        {
            var result = _realEstateClassifiedImageService.GetAllRealEstateImagesByRealEstateId(realEstateClassifiedId);
            if (result.Success == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
