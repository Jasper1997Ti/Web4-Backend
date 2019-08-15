
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Models
{
    public class TraineeRating
    {
        #region Properties
        public int TraineeId { get; set; }
        public int TrainingsSchemaId { get; set; }
        public Trainee Trainee { get; set; }

        public TrainingsSchema TrainingsSchema { get; set; }

        public int Rating { get; set; }
        #endregion
    }
}
