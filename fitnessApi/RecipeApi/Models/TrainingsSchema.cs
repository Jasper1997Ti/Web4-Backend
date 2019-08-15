using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FitnessApi.Models
{
    public class TrainingsSchema
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public string Categorie { get; set; }

        public ICollection<Exercise> Exercises { get; private set; }
        #endregion

        #region Constructors
        public TrainingsSchema()
        {
            Exercises = new List<Exercise>();
            Created = DateTime.Now;
        }

        public TrainingsSchema(string name) : this()
        {
            Name = name;
        }
        #endregion

        #region Methods
        public void AddExercise(Exercise exercise) => Exercises.Add(exercise);

        public Exercise GetExercise(int id) => Exercises.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}