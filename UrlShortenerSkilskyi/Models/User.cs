﻿namespace UrlShortenerSkilskyi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public ICollection<Url> Urls { get; set; }
    }
}
