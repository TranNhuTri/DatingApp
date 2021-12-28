using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Controllers;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Namespace
{
    public class AccountsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountsController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }

        [HttpPost("register")]
        public ActionResult<string> Register(RegisterDTO registerDTO)
        {
            registerDTO.Username.ToLower();
            if (_context.Users.Any(u => u.Username == registerDTO.Username))
            {
                return BadRequest("Username is exisited");
            }

            using var hmac = new HMACSHA512();

            var user = new User()
            {
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(_tokenService.CreateToken(user));
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDTO loginDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == loginDTO.Username);
            if (user == null) return Unauthorized("Invalid username or password");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
            }

             return _tokenService.CreateToken(user);
        }
    }
}