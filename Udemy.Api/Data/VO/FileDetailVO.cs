using System.Text.Json.Serialization;

namespace Udemy.Api.Data.VO
{
    public class FileDetailVO
    {
        [JsonPropertyName("document_name")]
        public string DocumentName { get; set; }

        [JsonPropertyName("doc_type")]
        public string DocType { get; set; }

        [JsonPropertyName("doc_url")]
        public string DocUrl { get; set; }
    }
}
