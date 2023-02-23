using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using lesson1.Interfaces;
using lesson1.Services;
using lesson1.Utilities;
namespace lesson1.Utilities{
    public static class Helper{
        public static void bulidObj(this IServiceCollection services){
             services.AddSingleton<jobInterface, JobService>();
        }
    }
}