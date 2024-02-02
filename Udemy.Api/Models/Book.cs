namespace Udemy.Api.Models
{
    public class Book : Base
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public Decimal Price { get; set; }
    }
}
