using FitnessApi.Models;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Data.Repositories
{
    public class TraineeRepository : ITraineeRepository
    {
        
        private readonly TrainingsSchemaContext _context;
        private readonly DbSet<Trainee> _trainees;
        public TraineeRepository(TrainingsSchemaContext dbContext)
        {
            _context = dbContext;
            _trainees = dbContext.Trainees;
        }

        public Trainee GetBy(string email)
        {
            return _trainees.Include(c => c.Ratings).ThenInclude(f => f.TrainingsSchema).ThenInclude(r => r.Exercises).SingleOrDefault(c => c.Email == email);
        }

        public void Add(Trainee trainee)
        {
            _trainees.Add(trainee);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
