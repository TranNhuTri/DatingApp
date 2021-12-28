using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Controllers;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Namespace
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_context.Users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<User>> Get(int Id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}