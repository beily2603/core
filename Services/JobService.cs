using lesson1.Models;
using System.Collections.Generic;
using System.Linq;

namespace lesson1.Controllers{

   public static class JobService{

    private static List <Job> ListJobs = new List<Job>{
        new Job {Id=1,Name="home work in core",IsDone=false},
        new Job {Id=2,Name="learnning to c#'s test",IsDone=false}
    };
       public static List<Job> GetAll() => ListJobs;

       public static Job Get(int id)
        {
            return ListJobs.FirstOrDefault(t => t.Id == id);
        }

        public static void Add(Job Job){
        Job.Id=ListJobs.Max(t=>t.Id)+1;
        ListJobs.Add(Job);
        
       }

       public static bool Update(int id, Job newJob)
        {
            if (newJob.Id != id)
                return false;
            
            var Job = ListJobs.FirstOrDefault(t => t.Id == id);
            Job.Name = newJob.Name;
            Job.IsDone=newJob.IsDone;
            return true;
        }

        public static bool Delete(int id)
        {
            var Job = ListJobs.FirstOrDefault(t => t.Id == id);
            if (Job == null)
                return false;
            ListJobs.Remove(Job);
            return true;
        }
   }
}