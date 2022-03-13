using System.ComponentModel.DataAnnotations;

namespace SmartManager.Services.Requests
{
    public class createUserRequest {
        [Required]
        public string Name {get; set;}

        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                ErrorMessage = "O email informado não é válido.")]
        public string Email {get; set;}

        [Required]
        public string Password {get; set;}
    }
}