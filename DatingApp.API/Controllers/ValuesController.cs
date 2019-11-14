using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    //http://localhost:5000/api/values
    // will go to method in httpget en will get you the values
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // adding our database
        private readonly DataContext _context;
        public ValuesController(DataContext context) //rechtermuisklik - initialize paramter
        {
           _context = context;

        }

        // GET api/values
        [HttpGet]
        // instead of just return Iactionresult; add async Task<Actionresult>
        public async Task<IActionResult> GetValues()
        {
          var values = await _context.Values.ToListAsync();
          // change Tolist to Async method ; TolistAsync

// return http OK respons
          return Ok(values);
        }

        // GET api/values/5
        //http://localhost:5000/api/values/5 for example
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id==id);

            return Ok(value);
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