using System.Text.Json.Serialization;

namespace Udemy.Api.Data.VO
{
    public class UserVO
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
