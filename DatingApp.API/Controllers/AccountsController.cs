using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Controllers;
using DatingApp.API.Database;
using DatingApp.API.Database.Entities;
using DatingApp.API.Database.Repositories;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Namespace
{
    public class AccountsController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepo;
        public AccountsController(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;

        }

        [HttpPost("register")]
        public ActionResult<string> Register(RegisterDto registerDTO)
        {
            registerDTO.Username.ToLower();
            if (_userRepo.GetUserByUsername(registerDTO.Username) != null)
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
            _userRepo.CreateUser(user);
            _userRepo.SaveChanges();

            return Ok(new UserResponse()
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            });
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDto loginDTO)
        {
            var user = _userRepo.GetUserByUsername(loginDTO.Username);
            if (user == null) return Unauthorized("Invalid username or password");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
            }

            return Ok(new UserResponse()
            {
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            });
        }
    }
}