using System.ComponentModel.DataAnnotations;

namespace FitnessApi.DTOs
{
    public class ExerciseDTO
    {
        [Required]
        public string Name { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }
    }
}
