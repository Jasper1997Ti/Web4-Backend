using FitnessApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.DTOs
{
    public class RatedTrainingsSchemaDTO
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public int Rating { get; set; }

        public RatedTrainingsSchemaDTO(TrainingsSchema trainingsSchema, int rating)
        {
            Id = trainingsSchema.Id;
            Rating = rating;
        }
    }
}
