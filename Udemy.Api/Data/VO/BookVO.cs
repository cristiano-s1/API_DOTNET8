using System.Text.Json.Serialization;
using Udemy.Api.Hypermedia;
using Udemy.Api.Hypermedia.Abstract;

namespace Udemy.Api.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        [JsonPropertyName("code")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("price")]
        public Decimal Price { get; set; }

        [JsonPropertyName("launch_date")]
        public DateTime LaunchDate { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
