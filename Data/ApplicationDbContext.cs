using CharacterSheetDnD.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CharacterSheetDnD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Adding DbSet to each of my Class (Migration)
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<CharacterStatistic> CharacterStatistics { get; set; }
        public DbSet<CharacterHealth> CharacterHealths { get; set; }


        // Configuring the relations between the table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship between Character and CharacterStatistic
            modelBuilder.Entity<Character>()
                .HasOne(c => c.CharacterStatistic) 
                .WithOne(cs => cs.Character)
                .HasForeignKey<CharacterStatistic>(cs => cs.CharacterID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the one-to-many relationship between Character and CharacterClass
            modelBuilder.Entity<CharacterClass>()
                .HasOne(cc => cc.Character)
                .WithMany(c => c.CharacterClasses)
                .HasForeignKey(cc => cc.CharacterID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-one relationship between Character and CharacterHealth
            modelBuilder.Entity<Character>()
                .HasOne(c => c.CharacterHealth)
                .WithOne(ch => ch.Character)
                .HasForeignKey<CharacterHealth>(ch => ch.CharacterID)
                .OnDelete(DeleteBehavior.Cascade);
                
        }

    }
}
