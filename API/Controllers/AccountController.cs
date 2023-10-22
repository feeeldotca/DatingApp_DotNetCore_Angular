using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;

        public AccountController(DataContext context){
            _context = context;
        }

        [HttpPost("register")]  //~/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO){
            
            if(await UserExists(registerDTO.UserName)) return BadRequest($"Username:{registerDTO.UserName} is taken!");
            using var hmac = new HMACSHA512();

            var user = new AppUser{
                UserName = registerDTO.UserName.ToLower(),      //for UserExists() method to compare names
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPost("login")]
         public async Task<AppUser> Login(LoginDTO loginDTO){
         
            if(UserExists(loginDTO.u))
         }


        private Task<bool> UserExists(string username) {
            return _context.Users.AnyAsync(x => x.UserName==username.ToLower());  // For comparing two names 
        }

    }
}