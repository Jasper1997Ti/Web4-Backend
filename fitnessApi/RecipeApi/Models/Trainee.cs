using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Models
{
    public class Trainee
    {
        #region Properties
        //add extra properties if needed
        public int TraineeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<TraineeRating> Ratings { get; private set; }

        public IEnumerable<KeyValuePair<TrainingsSchema, int>> RatedTrainingsSchemas
        {
            get
            {
                var dict = new Dictionary<TrainingsSchema, int>();
                foreach (var x in Ratings)
                {
                    dict.TryAdd(x.TrainingsSchema, x.Rating);
                }
                return dict;
            }
        }
        #endregion

        #region Constructors
        public Trainee()
        {
            Ratings = new List<TraineeRating>();
        }
        #endregion

        #region Methods

        public void RateTrainingsSchema(TrainingsSchema trainingsSchema, int rating)
        {
            foreach (var el in Ratings.Where(item => item.TrainingsSchema == trainingsSchema && item.TraineeId == TraineeId).ToList())
            {
                Ratings.Remove(el);
            }
            Ratings.Add(new TraineeRating()
            {
                TrainingsSchemaId = trainingsSchema.Id,
                TraineeId = TraineeId,
                TrainingsSchema = trainingsSchema,
                Trainee = this,
                Rating = rating
            });
        }
        #endregion
    }
}
