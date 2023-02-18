
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lesson1.Models;

namespace lesson1.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{

    [HttpGet]
    public IEnumerable<Job> Get()
    {
      return JobService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Job> Get(int id)
    { 
        var t = JobService.Get(id);
            if (t == null)
                return NotFound();
             return t;
      
    }
    [HttpPost]
    public ActionResult Post(Job Job){
          JobService.Add(Job);
          return CreatedAtAction(nameof(Post), new { id = Job.Id }, Job);
    }
       [HttpPut("{id}")]
        public ActionResult Put(int id, Job Job)
        {
            if (! JobService.Update(id, Job))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! JobService.Delete(id))
                return NotFound();
            return NoContent();            
        }
}
