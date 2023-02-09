using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Forum.Context
{
    public class DefaultDbContext: DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options){}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
