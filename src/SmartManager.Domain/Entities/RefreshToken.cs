using System.ComponentModel.DataAnnotations.Schema;
using SmartManager.Domain.Entities;

namespace SmartManager.Entities
{
    public class RefreshToken : Base
    {
  
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime? Revoked { get; set; }
        public Boolean IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;

        [ForeignKey("UserId")]
        public long UserId {get; set;}
        public virtual User User {get; set;}
        public override bool Validate()
        {
            return true;
        }
    }
}