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
		public DbSet<CharacterSkill> CharacterSkills { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<CharacterWeapon> Weapons { get; set; }
	

		// Configuring the relations between the table
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>()
                .HasOne(c => c.CharacterStatistic)
                .WithOne(cs => cs.Character)
                .HasForeignKey<CharacterStatistic>(cs => cs.CharacterID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<CharacterClass>()
                .HasOne(cc => cc.Character)
                .WithMany(c => c.CharacterClasses)
                .HasForeignKey(cc => cc.CharacterID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Character>()
                .HasOne(c => c.CharacterHealth)
                .WithOne(ch => ch.Character)
                .HasForeignKey<CharacterHealth>(ch => ch.CharacterID)
                .OnDelete(DeleteBehavior.Cascade); 

            // For many-to-many relationships, consider carefully if cascade delete is appropriate,
            // especially on join tables, as it can lead to unintended deletions.
            modelBuilder.Entity<CharacterSavingThrow>()
                .HasOne(cst => cst.Character)
                .WithMany(c => c.CharacterSavingThrows)
                .HasForeignKey(cst => cst.CharacterID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CharacterSavingThrow>()
                .HasOne(cst => cst.SavingThrow)
                .WithMany(st => st.CharacterSavingThrows)
                .HasForeignKey(cst => cst.SavingThrowID)
                .OnDelete(DeleteBehavior.ClientCascade); 

            modelBuilder.Entity<CharacterSkill>()
                .HasOne(cs => cs.Character)
                .WithMany(c => c.CharacterSkills)
                .HasForeignKey(cs => cs.CharacterID)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<CharacterSkill>()
                .HasOne(cs => cs.Skill)
                .WithMany(s => s.CharacterSkills)
                .HasForeignKey(cs => cs.SkillID)
                .OnDelete(DeleteBehavior.ClientCascade);

			modelBuilder.Entity<Character>()
				.HasMany(c => c.CharacterWeapons)
				.WithOne(w => w.Character)
				.HasForeignKey(w => w.CharacterID)
				.OnDelete(DeleteBehavior.Cascade);
     

            // Add the data to the table SavingThrow on Migration
            modelBuilder.Entity<SavingThrow>().HasData(
			   new SavingThrow { SavingThrowID = 1, Name = "Strength", Description = "Used for physical tasks and resisting physical force" },
			   new SavingThrow { SavingThrowID = 2, Name = "Dexterity", Description = "Used for agility, reflexes, and balance tasks" },
			   new SavingThrow { SavingThrowID = 3, Name = "Constitution", Description = "Used for endurance and health" },
			   new SavingThrow { SavingThrowID = 4, Name = "Intelligence", Description = "Used for memory and reasoning" },
			   new SavingThrow { SavingThrowID = 5, Name = "Wisdom", Description = "Used for perception and insight" },
			   new SavingThrow { SavingThrowID = 6, Name = "Charisma", Description = "Used for interaction and leadership" }
		  );

			modelBuilder.Entity<Skill>().HasData(
				new Skill { SkillID = 1, Name = "Acrobatics", Description = "Dexterity checks for staying on your feet in tricky situations." },
				new Skill { SkillID = 2, Name = "Animal Handling", Description = "Wisdom checks for calming down animals, understanding their intentions, or predicting their actions." },
				new Skill { SkillID = 3, Name = "Arcana", Description = "Intelligence checks for recalling lore about spells, magic items, eldritch symbols, magical traditions, the planes of existence, and the inhabitants of those planes." },
				new Skill { SkillID = 4, Name = "Athletics", Description = "Strength checks for climbing, jumping, or swimming." },
				new Skill { SkillID = 5, Name = "Deception", Description = "Charisma checks when you try to lie, bluff, or mislead someone." },
				new Skill { SkillID = 6, Name = "History", Description = "Intelligence checks for recalling lore about historical events, legendary people, ancient kingdoms, past disputes, recent wars, and lost civilizations." },
				new Skill { SkillID = 7, Name = "Insight", Description = "Wisdom checks for determining the true intentions of a creature, such as when searching out a lie or predicting someone’s next move." },
				new Skill { SkillID = 8, Name = "Intimidation", Description = "Charisma checks for influencing someone through overt threats, hostile actions, and physical violence." },
				new Skill { SkillID = 9, Name = "Investigation", Description = "Intelligence checks for looking around for clues and making deductions based on those clues." },
				new Skill { SkillID = 10, Name = "Medicine", Description = "Wisdom checks for stabilizing a dying companion or diagnosing an illness." },
				new Skill { SkillID = 11, Name = "Nature", Description = "Intelligence checks for recalling lore about terrain, plants and animals, the weather, and natural cycles." },
				new Skill { SkillID = 12, Name = "Perception", Description = "Wisdom checks for noticing things." },
				new Skill { SkillID = 13, Name = "Performance", Description = "Charisma checks for delighting an audience with music, dance, acting, storytelling, or some other form of entertainment." },
				new Skill { SkillID = 14, Name = "Persuasion", Description = "Charisma checks for influencing someone or a group of people with tact, social graces, or good nature." },
				new Skill { SkillID = 15, Name = "Religion", Description = "Intelligence checks for recalling lore about deities, rites and prayers, religious hierarchies, holy symbols, and the practices of secret cults." },
				new Skill { SkillID = 16, Name = "Sleight of Hand", Description = "Dexterity checks for tasks like planting something on someone else or concealing an object on your person." },
				new Skill { SkillID = 17, Name = "Stealth", Description = "Dexterity checks for hiding or moving silently." },
				new Skill { SkillID = 18, Name = "Survival", Description = "Wisdom checks for following tracks, hunting wild game, guiding your group through frozen wastelands, identifying signs that owlbears live nearby, predicting the weather, or avoiding quicksand and other natural hazards." }
			);


		}

	}
}
