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
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService){
            _context = context;
            _tokenService = tokenService;
        }
        /// <summary>
        /// Entry point of register process
        /// </summary>
        /// <param name="registerDTO">the object contains Username and Password for register form</param>
        /// <returns> a UserDto object that contains a username and a token</returns>
        [HttpPost("register")]  //~/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO){
            
            if(await UserExists(registerDTO.UserName)) return BadRequest($"Username:{registerDTO.UserName} is taken!");
            using var hmac = new HMACSHA512();

            var user = new AppUser{
                UserName = registerDTO.UserName.ToLower(),      //for UserExists() method to compare names
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto{
                Username = user.UserName, 
                Token = _tokenService.CreateToken(user)
            };
        }
        /// <summary>
        /// Entry point of Login process
        /// </summary>
        /// <param name="loginDTO">the object contains Username and Password for login form</param>
        /// <returns></returns>
        [HttpPost("login")]
         public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO){
            // check if the login username is already registered in our Database
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName == loginDTO.Username);
            if(user == null) return Unauthorized("Invalid Username"); //not registed username is not authorized to login
            
            // user is from _context that is DB tables, so it has Id, Username, PasswordHash, PasswordSalt.
            using var hmac = new HMACSHA512(user.PasswordSalt); //to get the key
            // calculate passwordHash by its password string
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            // compare LoginDTO's hash code with DB's existing passwordHash
            for(int i =0; i<computeHash.Length; i++){
                if(computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
         }

        /// <summary>
        /// check if a username being registered already exists in our DB
        /// </summary>
        /// <param name="username">The string used for register as user name</param>
        /// <returns></returns>
        private Task<bool> UserExists(string username) {
            return _context.Users.AnyAsync(x => x.UserName==username.ToLower());  // For comparing two names 
        }

    }
}