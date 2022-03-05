using System.Linq;
using DatingApp.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Database.Repositories
{
    public class UserLikeRepository : IUserLikeRepository
    {
        private readonly IUserRepository _userRepo;
        private readonly DataContext _context;
        public UserLikeRepository(IUserRepository userRepo, DataContext context)
        {
            _context = context;
            _userRepo = userRepo;
        }
        public bool LikeUser(string sourceUsername, string likedUsername)
        {
            var sourceUser = _context.Users.Where(u => u.Username == sourceUsername).Include(u => u.SourceUsers).FirstOrDefault();
            if(sourceUser == null) return false;

            var likedUser = _userRepo.GetUserByUsername(likedUsername);
            if(likedUser == null) return false;
            if(sourceUser.SourceUsers.FirstOrDefault(u => u.LikeUserId == likedUser.Id) != null) return false;

            _context.UserLikes.Add(new UserLike()
            {
                LikeUserId = likedUser.Id,
                SourceUserId = sourceUser.Id
            });
            return _context.SaveChanges() > 0;
        }
    }
}