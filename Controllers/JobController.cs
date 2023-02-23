
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lesson1.Models;
using lesson1.Interfaces;

namespace lesson1.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{

private jobInterface job;
public JobController(jobInterface job){
    this.job=job;
}
    [HttpGet]
    public IEnumerable<Job> Get()
    {
      return job.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Job> Get(int id)
    { 
        var t = job.Get(id);
            if (t == null)
                return NotFound();
             return t;
      
    }
    [HttpPost]
    public ActionResult Post(Job Jobb){
          job.Add(Jobb);
          return CreatedAtAction(nameof(Post), new { id = Jobb.Id }, Jobb);
    }
       [HttpPut("{id}")]
        public ActionResult Put(int id, Job Jobb)
        {
            if (! job.Update(id, Jobb))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! job.Delete(id))
                return NotFound();
            return NoContent();            
        }
}
