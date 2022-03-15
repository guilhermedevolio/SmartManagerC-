using System.ComponentModel.DataAnnotations;

namespace SmartManager.Services.Requests
{
    public class RefreshTokenRequest {

        [Required]
        public string Token {get; set;}
    }
}