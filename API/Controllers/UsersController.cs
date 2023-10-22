using System.Runtime.CompilerServices;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using DTOs.RegisterDTO;

namespace API.Controllers;
/// <summary>
/// this arritribute would allow only authenticated users to the AccountController, 
/// except for GetUsers() action, which is accessible by everyone, regardless of their
/// authenticated or unauthenticated/anonymous status.
/// [AllowAnonmous] on controller level> [Authorize] on action or even controller level
/// </summary>
[Authorize]
public class UsersController : BaseApiController
{
    /// <summary>
    /// UsersController: used for DB's exsiting user read/update/delete process
    /// we can allow authorize attribute or allowanonymous attribute here
    /// </summary>
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<AppUser>> UpdateUser(AppUser user)
    {
        var result = await _context.Users.FindAsync(user.Id);
        if (result == null) return NotFound();
        _context.Users.Where(u=>u.Id==user.Id); //.ExecuteUpdate();
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult<int>> DeleteUserById(int id)
    {
        var result = await _context.Users.FindAsync(id);
        if (result == null) return -1;
        _context.Users.Remove(result);
        return _context.SaveChanges();
    }
}

