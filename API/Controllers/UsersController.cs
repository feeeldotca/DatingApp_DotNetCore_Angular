using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
[ApiController]
[Route("api/[Controller]")]
public class UsersController : ControllerBase
{
  
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;       
    }

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers(){
        return _context.Users.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUser(int id){
        return _context.Users.Find(id);
    }

    [HttpPost]
    public int UpdateUser(AppUser user){
        var result = _context.Users.Find(user.Id);
        if (result==null) return -1;
        _context.Users.Attach(user);
        return _context.SaveChanges();
    }

    [HttpDelete]
    public int DeleteUserById(int id) {
        var result = _context.Users.Find(id);
        if (result==null) return -1;    
        _context.Users.Remove(result);
        return _context.SaveChanges();
    }
}

