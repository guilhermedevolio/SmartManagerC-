using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SmartManager.API.Response
{
    public class AuthenticateResponse {
        public AuthenticateResponse(int status, string token)
        {
            Status = status;
            Token = token;
        }

        public int Status {get; set;}
        public String Token {get; set;}
    }
}