using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("auth")]
        [Authorize]
        public ActionResult<string> GetSecret() => Ok("This is secret");

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Users.Find(-1);
            if(thing==null) return NotFound();
            return thing;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            try{
                var thing = _context.Users.Find(-1);
                var thingString = thing.ToString();
                return thingString;
            }
            catch (Exception ex){
                return StatusCode(500, "NO Pass");
            }
            
        }

    }
}