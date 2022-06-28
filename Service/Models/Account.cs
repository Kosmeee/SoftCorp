using System.Text.Json.Serialization;

namespace Service.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
