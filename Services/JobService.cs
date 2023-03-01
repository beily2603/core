using lesson1.Models;
// using System.Collections.Generic;
// using System.Linq;
using lesson1.Interfaces;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.OpenApi.Models;
// using lesson1.Utilities;
using System.Text.Json;
// using Microsoft.AspNetCore.Hosting;


namespace lesson1.Services{


   public  class JobService:jobInterface{
  
        List<Job> ListJobs { get; }
        private IWebHostEnvironment  webHost;
        private string filePath;
        public JobService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "job.json");
            //this.filePath = webHost.ContentRootPath+@"/Data/Pizza.json";
            using (var jsonFile = File.OpenText(filePath))
            {
               ListJobs = JsonSerializer.Deserialize<List<Job>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(ListJobs));
        }

       public  List<Job> GetAll() => ListJobs;

       public  Job Get(int id)
        {
            return ListJobs.FirstOrDefault(t => t.Id == id);
        }

        public  void Add(Job Job){
        Job.Id=ListJobs.Max(t=>t.Id)+1;
        ListJobs.Add(Job);
        saveToFile();
        
       }

       public  bool Update(int id, Job newJob)
        {
            if (newJob.Id != id)
                return false;
            
            var Jobb = ListJobs.FirstOrDefault(t => t.Id == id);
            Jobb.Name = newJob.Name;
            Jobb.IsDone=newJob.IsDone;
            saveToFile();
            return true;
        }

        public  bool Delete(int id)
        {
            var Job = ListJobs.FirstOrDefault(t => t.Id == id);
            if (Job == null)
                return false;
            ListJobs.Remove(Job);
            saveToFile();
            return true;
        }
   }
}