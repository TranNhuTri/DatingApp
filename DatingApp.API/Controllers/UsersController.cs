using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using DatingApp.API.Controllers;
using DatingApp.API.Database.Entities;
using DatingApp.API.Database.Repositories;
using DatingApp.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Namespace
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserLikeRepository _userLikeRepo;
        public UsersController(IUserRepository userRepo, IUserLikeRepository userLikeRepo)
        {
            _userLikeRepo = userLikeRepo;
            _userRepo = userRepo;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<MemberDto>> Get()
        {
            return Ok(_userRepo.GetMembers());
        }
        
        [HttpGet("{username}")]
        public ActionResult<MemberDto> Get(string username)
        {
            var user = _userRepo.GetUserByUsername(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        public ActionResult Put(ProfileDto profile)
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(username)) return NotFound();

            _userRepo.UpdateProfile(username, profile);
            if (_userRepo.SaveChanges())
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost]
        public ActionResult Like([FromBody] string likedUsername)
        {
            var sourceUsername = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(_userLikeRepo.LikeUser(sourceUsername, likedUsername))
                return NoContent();
            return BadRequest();
        }
    }
}