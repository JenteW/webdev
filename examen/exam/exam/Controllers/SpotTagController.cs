﻿using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpotTagController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public SpotTagController(InterfaceCampingContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SpotTag>> GetSpotTag()
        {
            return Ok(_data.GetSpotTags());
        }

        [HttpPost]
        public void AddSpotTag([FromBody] SpotTag spotTag)
        {
            _data.AddSpotTag(spotTag);
        }
    }
}
