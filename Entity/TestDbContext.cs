using Microsoft.EntityFrameworkCore;

namespace Entity
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(32);
                entity.Property(e => e.Email).HasMaxLength(32);
            });

        }
    }
}
