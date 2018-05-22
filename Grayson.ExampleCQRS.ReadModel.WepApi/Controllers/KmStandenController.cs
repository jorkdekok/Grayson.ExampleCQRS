using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.Domain.ReadModel.Model;
using Grayson.ExampleCQRS.Domain.ReadModel.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Grayson.ExampleCQRS.ReadModel.WepApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class KmStandenController : Controller
    {
        private readonly IKmStandViewRepository _kmStandViewRepository;

        public KmStandenController(IKmStandViewRepository kmStandViewRepository)
        {
            _kmStandViewRepository = kmStandViewRepository;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<KmStandView> Get(int page = 1, int pageSize = 10)
        {
            var list = _kmStandViewRepository.GetAll(page, pageSize);
            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
