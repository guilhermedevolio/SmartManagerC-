using System.Text.Json.Serialization;

namespace SmartManager.Services.DTOS
{
    public class UserDTO {

        public long Id { get; set; }
        public string ?Name { get;  set; } 

        public string ?Email { get;  set; } 

        [JsonIgnore]
        public string ?Password { get;  set; } 

        [JsonIgnore]
        public string ?Role { get; private set; } 

       [JsonIgnore]
        public int ?AccessAttempts { get; private set; } 

       [JsonIgnore]
        public DateTime ?UnlockDate { get; private set; }

        public UserDTO()
        {}

        public UserDTO(long id, string? name, string? email, string? password, string? role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}