using FitnessApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RecipeApi.Data.Repositories
{
    public class TrainingsSchemaRepository : ITrainingsSchemaRepository
    {
        private readonly TrainingsSchemaContext _context;
        private readonly DbSet<TrainingsSchema> _trainingsSchemas;

        public TrainingsSchemaRepository(TrainingsSchemaContext dbContext)
        {
            _context = dbContext;
            _trainingsSchemas = dbContext.TrainingsSchemas;
        }

        public IEnumerable<TrainingsSchema> GetAll()
        {
            return _trainingsSchemas.Include(r => r.Exercises);
        }

        public TrainingsSchema GetBy(int id)
        {
            return _trainingsSchemas.Include(r => r.Exercises).SingleOrDefault(r => r.Id == id);
        }

        public bool TryGetTrainingsSchema(int id, out TrainingsSchema trainingsSchema)
        {
            trainingsSchema = _context.TrainingsSchemas.Include(t => t.Exercises).FirstOrDefault(t => t.Id == id);
            return trainingsSchema != null;
        }

        public void Add(TrainingsSchema trainingsSchema)
        {
            _trainingsSchemas.Add(trainingsSchema);
        }

        public void Update(TrainingsSchema trainingsSchema)
        {
            _context.Update(trainingsSchema);
        }

        public void Delete(TrainingsSchema trainingsSchema)
        {
            _trainingsSchemas.Remove(trainingsSchema);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<TrainingsSchema> GetBy(string name = null, string categorie = null, string exerciseName = null)
        {
            var trainingsSchemas = _trainingsSchemas.Include(r => r.Exercises).AsQueryable();
            if (!string.IsNullOrEmpty(name))
                trainingsSchemas = trainingsSchemas.Where(r => r.Name.IndexOf(name, System.StringComparison.OrdinalIgnoreCase) >= 0);
            if (!string.IsNullOrEmpty(categorie))
                trainingsSchemas = trainingsSchemas.Where(r => r.Categorie.Equals(categorie, System.StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(exerciseName))
                trainingsSchemas = trainingsSchemas.Where(r => r.Exercises.Any(i => i.Name.Equals(exerciseName, System.StringComparison.OrdinalIgnoreCase)));
            return trainingsSchemas.OrderBy(r => r.Name).ToList();
        }
    }
}
