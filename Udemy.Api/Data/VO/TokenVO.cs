using System.Text.Json.Serialization;

namespace Udemy.Api.Data.VO
{
    public class TokenVO
    {
        public TokenVO(bool authenticated, string created, string expiration, string accessToken, string refreshToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        [JsonPropertyName("authenticated")]
        public bool Authenticated { get; set; }

        [JsonPropertyName("created")]
        public string Created { get; set; }

        [JsonPropertyName("expiration")]
        public string Expiration { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
