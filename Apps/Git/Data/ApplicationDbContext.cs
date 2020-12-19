namespace Git.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Commit> Commits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Git;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //  .HasMany(x => x.Commits)
            //  .WithOne(x => x.Creator)
            //  .HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<User>()
            //    .HasMany(x => x.Repositories)
            //    .WithOne(x => x.Owner)
            //    .HasForeignKey(x => x.OwnerId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Commit>()
                .HasOne(x => x.Repository)
                .WithMany(x => x.Commits)
                .HasForeignKey(x => x.RepositoryId).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}