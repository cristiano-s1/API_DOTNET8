using System.Text.Json.Serialization;
using Udemy.Api.Hypermedia;
using Udemy.Api.Hypermedia.Abstract;

namespace Udemy.Api.Data.VO
{
    public class PersonVO : ISupportsHyperMedia
    {
        [JsonPropertyName("code")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonIgnore]
        public bool Enabled { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
