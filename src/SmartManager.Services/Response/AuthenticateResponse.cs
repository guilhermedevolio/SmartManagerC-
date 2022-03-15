using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SmartManager.API.Response
{
    public class AuthenticateResponse {
        public AuthenticateResponse(int status, string token, string refreshToken)
        {
            Status = status;
            Token = token;
            RefreshToken = refreshToken;
        }

        public int Status {get; set;}
        public String Token {get; set;}

        public String RefreshToken {get; set;}

    }
}