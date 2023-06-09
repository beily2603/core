using lesson1.Models;
using lesson1.Interfaces;
using System.Text.Json;



namespace lesson1.Services{


   public  class UserService: ToDoInterface<User>{
  
        List<User>ListUsers { get; }
        private IWebHostEnvironment  webHost;
        private string filePath;
        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "user.json");

            using (var jsonFile = File.OpenText(filePath))
            {
               ListUsers = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
            }
        }
        public static int isExist(string name,string password){
            string filePath =Path.Combine("Data","user.json");
            List<User>? ListUsers;
            using (var jsonFile = System.IO.File.OpenText(filePath))//File.OpenText(filePath)
            {
               ListUsers = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            User user= ListUsers!.FirstOrDefault(u=>u.Name==name&&u.Password==password)!;
            if(user!=null){
                return user.Id;
            }
            return -1;
        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(ListUsers));
        }

       public  List<User> GetAll() => ListUsers;

       public  User Get(int id)
        {
            return ListUsers.FirstOrDefault(t => t.Id == id)!;
        }

        public  void Add(User user){
        user.Id=ListUsers.Max(t=>t.Id)+1;
        ListUsers.Add(user);
        saveToFile();
        
       }

       public  bool Update(int id,User newUser)
        {
             System.Console.WriteLine("update");
            if (newUser.Id != id)
                return false;
            
            User user = ListUsers.FirstOrDefault(t => t.Id == id)!;
            if(user==null)
                return false;
            user.Name = newUser.Name;
            user.Password=newUser.Password;
             System.Console.WriteLine("end "+user.Name);
            saveToFile();
            return true;
           
        }

        public  bool Delete(int id)
        {
            var user = ListUsers.FirstOrDefault(t => t.Id == id);
            if (user == null)
                return false;
            ListUsers.Remove(user);
            saveToFile();
            return true;
        }
   }
}