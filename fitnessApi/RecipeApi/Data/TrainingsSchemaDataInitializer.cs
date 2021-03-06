﻿using FitnessApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Data
{
    public class TrainingsSchemaDataInitializer
    {
        private readonly TrainingsSchemaContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public TrainingsSchemaDataInitializer(TrainingsSchemaContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        //public void InitializeData()
        //{
        //    _dbContext.Database.EnsureDeleted();
        //    if (_dbContext.Database.EnsureCreated())
        //    {
        //        //seeding the database with recipes, see DBContext               
        //    }
        //}

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with recipes, see DBContext         
                Trainee trainee = new Trainee { Email = "recipemaster@hogent.be", FirstName = "Adam", LastName = "Master" };
                _dbContext.Trainees.Add(trainee);
                await CreateUser(trainee.Email, "P@ssword1111");
                Trainee student = new Trainee { Email = "student@hogent.be", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Trainees.Add(student);
                student.RateTrainingsSchema(_dbContext.TrainingsSchemas.First(), 4);
                await CreateUser(student.Email, "P@ssword1111");
                _dbContext.SaveChanges();
            }
        }
        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}

