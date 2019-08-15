using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessApi.DTOs
{
    public class TrainingsSchemaDTO
    {
        [Required]
        public string Name { get; set; }

        public string categorie { get; set; }

        public IList<ExerciseDTO> Exercises { get; set; }
    }
}
