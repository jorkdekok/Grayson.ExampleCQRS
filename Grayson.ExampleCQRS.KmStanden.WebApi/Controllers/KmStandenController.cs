using System;
using System.Collections.Generic;
using Grayson.ExampleCQRS.KmStanden.Application.Commands;
using Grayson.ExampleCQRS.KmStanden.WebApi.Models;
using Grayson.SeedWork.DDD.Application;

using Microsoft.AspNetCore.Mvc;

namespace Grayson.ExampleCQRS.KmStanden.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class KmStandenController : Controller
    {
        private readonly ICommandBus _commandBus;

        public KmStandenController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public KmStandViewModel Get(Guid id)
        {
            return new KmStandViewModel();
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
    }
}