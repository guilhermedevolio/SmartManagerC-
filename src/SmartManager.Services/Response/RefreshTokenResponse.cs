using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SmartManager.API.Response
{
    public class RefreshTokenResponse {
        public String RevokedToken {get; set;}
        public String AccessToken {get; set;}
    }
}