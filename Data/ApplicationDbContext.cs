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
        public DbSet<CharacterSavingThrow> CharacterSavingThrows { get; set; }
        public DbSet<SavingThrow> SavingThrows { get; set; }


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

			// Configure the many-to-many relationship between Character and SavingThrow through CharacterSavingThrow
			modelBuilder.Entity<CharacterSavingThrow>()
				.HasOne(cst => cst.Character)
				.WithMany(c => c.CharacterSavingThrows)
				.HasForeignKey(cst => cst.CharacterID);

			modelBuilder.Entity<CharacterSavingThrow>()
				.HasOne(cst => cst.SavingThrow)
				.WithMany(st => st.CharacterSavingThrows)
				.HasForeignKey(cst => cst.SavingThrowID);


			// Add the data to the table SavingThrow on Migration
			modelBuilder.Entity<SavingThrow>().HasData(
		   new SavingThrow { SavingThrowID = 1, Name = "Strength", Description = "Used for physical tasks and resisting physical force" },
		   new SavingThrow { SavingThrowID = 2, Name = "Dexterity", Description = "Used for agility, reflexes, and balance tasks" },
		   new SavingThrow { SavingThrowID = 3, Name = "Constitution", Description = "Used for endurance and health" },
		   new SavingThrow { SavingThrowID = 4, Name = "Intelligence", Description = "Used for memory and reasoning" },
		   new SavingThrow { SavingThrowID = 5, Name = "Wisdom", Description = "Used for perception and insight" },
		   new SavingThrow { SavingThrowID = 6, Name = "Charisma", Description = "Used for interaction and leadership" }
	   );

		}

    }
}
