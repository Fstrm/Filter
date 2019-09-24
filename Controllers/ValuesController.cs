using System;
using System.Collections.Generic;
using Filter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Filter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDataAccessLayer _dataAccessLayer;
        private readonly IMemoryCache _cache;

        public ValuesController(IDataAccessLayer dataAccessLayer, IMemoryCache cache)
        {
            _dataAccessLayer = dataAccessLayer;
            _cache = cache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string address = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            address = "192.168.88.74";
            address = "193.168.70.75";

            if (!(address == "::1" || _dataAccessLayer.CheckAddress(address)))
            {
                _cache.Set(DateTime.Now.ToString(), address, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
                return new UnauthorizedResult();
            }

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
