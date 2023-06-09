
using lesson1.Interfaces;
using lesson1.Services;
using lesson1.Models;
namespace lesson1.Utilities{
    public static class Helper{
        public static void bulidObj(this IServiceCollection services){
             services.AddSingleton<ToDoInterface<task>, TaskService>();
             services.AddSingleton<ToDoInterface<User>,UserService>();
             services.AddSingleton<ILogService,LogService>();
        }
    }
}