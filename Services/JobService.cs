using lesson1.Models;
using System.Collections.Generic;
using System.Linq;
using lesson1.Interfaces;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using lesson1.Utilities;

namespace lesson1.Services{

   public  class JobService:jobInterface{

    private  List <Job> ListJobs = new List<Job>{
        new Job {Id=1,Name="home work in core",IsDone=false},
        new Job {Id=2,Name="learnning to c#'s test",IsDone=false}
    };
       public  List<Job> GetAll() => ListJobs;

       public  Job Get(int id)
        {
            return ListJobs.FirstOrDefault(t => t.Id == id);
        }

        public  void Add(Job Job){
        Job.Id=ListJobs.Max(t=>t.Id)+1;
        ListJobs.Add(Job);
        
       }

       public  bool Update(int id, Job newJob)
        {
            if (newJob.Id != id)
                return false;
            
            var Job = ListJobs.FirstOrDefault(t => t.Id == id);
            Job.Name = newJob.Name;
            Job.IsDone=newJob.IsDone;
            return true;
        }

        public  bool Delete(int id)
        {
            var Job = ListJobs.FirstOrDefault(t => t.Id == id);
            if (Job == null)
                return false;
            ListJobs.Remove(Job);
            return true;
        }
   }
}