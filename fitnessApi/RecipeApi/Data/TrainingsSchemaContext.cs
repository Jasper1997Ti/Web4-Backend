using FitnessApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace RecipeApi.Data
{
    public class TrainingsSchemaContext : IdentityDbContext
    {
        public TrainingsSchemaContext(DbContextOptions<TrainingsSchemaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TrainingsSchema>()
                .HasMany(p => p.Exercises)
                .WithOne()
                .IsRequired()
                .HasForeignKey("TrainingsSchemaId"); //Shadow property
            builder.Entity<TrainingsSchema>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Entity<TrainingsSchema>().Property(r => r.Categorie).HasMaxLength(50);

            builder.Entity<Exercise>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Exercise>().Property(r => r.Sets);
            builder.Entity<Exercise>().Property(r => r.Reps);

            builder.Entity<Trainee>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Trainee>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Trainee>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Trainee>().Ignore(c => c.RatedTrainingsSchemas);

            builder.Entity<TraineeRating>().HasKey(f => new { f.TraineeId, f.TrainingsSchemaId });
            builder.Entity<TraineeRating>().HasOne(f => f.Trainee).WithMany(u => u.Ratings).HasForeignKey(f => f.TraineeId);
            builder.Entity<TraineeRating>().HasOne(f => f.TrainingsSchema).WithMany().HasForeignKey(f => f.TrainingsSchemaId);

            //Another way to seed the database
            builder.Entity<TrainingsSchema>().HasData(
                new TrainingsSchema { Id = 1, Name = "Chest", Created = DateTime.Now, Categorie="Bulk"},
                new TrainingsSchema { Id = 2, Name = "Back", Created = DateTime.Now }
  );

            builder.Entity<Exercise>().HasData(
                    //Shadow property can be used for the foreign key, in combination with anaonymous objects
                    new { Id = 1, Name = "Incline DB", Sets = 3, Reps = 8, TrainingsSchemaId = 1 },
                    new { Id = 2, Name = "Flyes",Sets = 4, Reps = 12, TrainingsSchemaId = 1 },
                    new { Id = 3, Name = "Lat Pulldown", Sets = 4, Reps = 10, TrainingsSchemaId = 2 }
                 );
        }

        public DbSet<TrainingsSchema> TrainingsSchemas { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
    }
}