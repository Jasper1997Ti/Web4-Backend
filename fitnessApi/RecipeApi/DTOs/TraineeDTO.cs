using FitnessApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.DTOs
{
    public class TraineeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<KeyValuePair<TrainingsSchema, int>> TrainingsSchemas { get; set; }

        public TraineeDTO() { }

        public TraineeDTO(Trainee trainee) : this()
        {
            FirstName = trainee.FirstName;
            LastName = trainee.LastName;
            Email = trainee.Email;
            TrainingsSchemas = trainee.RatedTrainingsSchemas;
        }
    }
}
