namespace UrlShortenerSkilskyi.Models
{
    public class Url
    {
        public int Id { get; set; }
        public string OriginalLink { get; set; }
        public string ShortLink { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
