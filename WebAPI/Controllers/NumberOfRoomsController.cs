using Business.Abstract;
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
    public class NumberOfRoomsController : ControllerBase
    {
        INumberOfRoomService _numberOfRoomService;
        public NumberOfRoomsController(INumberOfRoomService numberOfRoomService)
        {
            _numberOfRoomService = numberOfRoomService;
        }
        [HttpPost("add")]
        public IActionResult Add(NumberOfRoom numberOfRoom)
        {
            var result = _numberOfRoomService.Add(numberOfRoom);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(NumberOfRoom numberOfRoom)
        {
            var result = _numberOfRoomService.Delete(numberOfRoom);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(NumberOfRoom numberOfRoom)
        {
            var result = _numberOfRoomService.Update(numberOfRoom);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _numberOfRoomService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
