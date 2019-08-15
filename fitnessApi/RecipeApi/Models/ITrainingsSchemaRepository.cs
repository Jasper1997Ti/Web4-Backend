using System.Collections.Generic;

namespace FitnessApi.Models
{
    public interface ITrainingsSchemaRepository
    {
            TrainingsSchema GetBy(int id);
            bool TryGetTrainingsSchema(int id, out TrainingsSchema trainingsSchema);
            IEnumerable<TrainingsSchema> GetAll();
        IEnumerable<TrainingsSchema> GetBy(string name = null, string categorie = null, string exerciseName = null);
            void Add(TrainingsSchema trainingsSchema);
            void Delete(TrainingsSchema trainingsSchema);
            void Update(TrainingsSchema trainingsSchema);
            void SaveChanges();
        }
    }

