using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.Application.Commands;
using Grayson.ExampleCQRS.WebApi.Models;
using Grayson.SeedWork.DDD.Application;
using Microsoft.AspNetCore.Mvc;

namespace Grayson.ExampleCQRS.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class KmStandenController : Controller
    {
        private readonly ICommandBus _commandBus;

        public KmStandenController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]KmStandViewModel model)
        {
            _commandBus.Send(new AddNewKmStand(model.Stand, model.Datum, Guid.Empty));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
