﻿using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public CityController(InterfaceCampingContext data)
        {
            _data = data;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Admin>> GetCity()
        {
            return Ok(_data.GetCities());
        }


        [HttpPost]
        public void AddCity([FromBody] City city)
        {
            _data.AddCity(city);
        }
    }
}