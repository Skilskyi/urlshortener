using Microsoft.EntityFrameworkCore;
using System;
using UrlShortenerSkilskyi.Models;

namespace UrlShortenerSkilskyi.Data
{
    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
