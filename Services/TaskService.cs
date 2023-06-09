using lesson1.Models;
using lesson1.Interfaces;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;



namespace lesson1.Services{
   public  class TaskService:ToDoInterface<task>{
  
        List<task>? ListTasks { get; }
        private int userId;
        private IWebHostEnvironment  webHost;
        private string filePath;
        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "job.json");
            //this.filePath = webHost.ContentRootPath+@"/Data/Pizza.json";
            using (var jsonFile = File.OpenText(filePath))
            {
               ListTasks = JsonSerializer.Deserialize<List<task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }


        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(ListTasks));
        }

       public  List<task> GetAll() => ListTasks!;

       public  task Get(int id)
        {
            return ListTasks!.FirstOrDefault(t => t.Id == id)!;
        }

        public  void Add(task Job){
        Job.Id=ListTasks!.Max(t=>t.Id)+1;
        ListTasks!.Add(Job);
        saveToFile();
        
       }

       public  bool Update(int id, task newTask)
        {        
            var task = Get(id);
            if(task==null){
              System.Console.WriteLine(task+"false");
                 return false;}
            task.Name = newTask.Name;
            task.IsDone=newTask.IsDone;
            System.Console.WriteLine(task.Name,newTask.Name,"rtyui",newTask.IsDone);
            saveToFile();
            return true;
        }

        public  bool Delete(int id)
        {
            var Job = ListTasks!.FirstOrDefault(t => t.Id == id);
            if (Job == null)
                return false;
            ListTasks!.Remove(Job);
            saveToFile();
            return true;
        }
          public static int getId(string token){
            token = token.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token) as JwtSecurityToken;
            var id = decodedValue.Claims.First(claim => claim.Type == "id").Value;
            return Convert.ToInt32(id);
        }

   }
}