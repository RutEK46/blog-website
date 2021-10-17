using Blog.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;

namespace Blog.Database
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<BlogItem> BlogItems { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureDiscriminator(builder.Model.GetEntityTypes());

            base.OnModelCreating(builder);
        }

        public void ConfigureDiscriminator(IEnumerable<IMutableEntityType> entitityTypes)
        {
            foreach (var entityType in entitityTypes)
            {
                var property = entityType.FindProperty("Discriminator");

                if (property != null)
                {
                    property.SetIsUnicode(false);
                    property.SetMaxLength(128);
                }
            }
        }
    }
}
