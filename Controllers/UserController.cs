
using Microsoft.AspNetCore.Mvc;
using lesson1.Models;
using lesson1.Interfaces;
using System.Security.Claims;
using lesson1.Services;
using Microsoft.AspNetCore.Authorization;

namespace lesson1.Controllers;


[Route("[controller]")]
public class UserController : ControllerBase
{

private ToDoInterface<User> user;
public UserController(ToDoInterface<User> user){
    this.user=user;
}
    [HttpPost]
    [Route("[action]")]
        public ActionResult<String> Login([FromBody] User user)
        {  
            System.Console.WriteLine(user.Name!+"*"+user.Password);
             bool isAdmin=false;
            var dt = DateTime.Now;
            var claims=new List<Claim>{};
            if (user.Name == "Dina"
            && user.Password == $"W{dt.Year}#{dt.Day}!")
            {
                isAdmin=true;
                claims.Add(new Claim("type", "Admin"));
            }
            else{
             System.Console.WriteLine(user.Name!+"*"+user.Password+"user");
             int userId=UserService.isExist(user.Name!,user.Password!);
              if(userId==-1)
                  return NotFound();
            claims.Add(new Claim("type","User"));
            claims.Add(new Claim("id",userId.ToString()));
            }
           var token = TokenService.GetToken(claims);
            System.Console.WriteLine(token);
            System.Console.WriteLine("token" + TokenService.WriteToken(token)+"isAdmin" + isAdmin);
            return new OkObjectResult(new {token = TokenService.WriteToken(token),isAdmin = isAdmin}) ;
        }
    [HttpGet] 
    [Authorize(Policy = "Admin")]
    public IEnumerable<User> Get()
    {
      return user.GetAll();
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult<User> Get(int id)
    { 
        var t = user.Get(id);
            if (t == null)
                return NotFound();
             return t;
      
    }
    [Route("[action]")]
    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post(User user){
         System.Console.WriteLine("hello");
         this.user.Add(user);
        
          return CreatedAtAction(nameof(Post), new { id = user.Id }, user);
    }


       [HttpPut("{id}")]
       [Authorize(Policy = "Admin")]
        public ActionResult Put(int id, User u)
        {
             System.Console.WriteLine(u.Name);
            if (! user.Update(id, u))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult Delete (int id)
        {
            if (! user.Delete(id))
                return NotFound();
            return NoContent();            
        }
}
