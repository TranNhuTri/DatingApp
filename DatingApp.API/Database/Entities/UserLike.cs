using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.API.Database.Entities
{
    public class UserLike
    {
        public int SourceUserId { get; set; }
        public int LikeUserId { get; set; }
        public virtual User SoureUser {get; set; }
        public virtual User LikeUser { get; set; }
    }
}