using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using FitnessApi.DTOs;
using FitnessApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FitnessApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrainingsSchemaController : ControllerBase
    {
        private readonly ITrainingsSchemaRepository _trainingsSchemaRepository;
        private readonly ITraineeRepository _traineeRepository;

        public TrainingsSchemaController(ITrainingsSchemaRepository context, ITraineeRepository traineeRepository)
        {
            _trainingsSchemaRepository = context;
            _traineeRepository = traineeRepository;
        }

        // GET: api/TrainingsSchemas
        /// <summary>
        /// Get all trainingsSchemas ordered by name
        /// </summary>
        /// <returns>array of trainingsSchemas</returns>

        [HttpGet]
        public IEnumerable<TrainingsSchema> GetTrainingsSchemas( string name = null, string categorie = null, string exerciseName = null)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(categorie) && string.IsNullOrEmpty(exerciseName))
               return _trainingsSchemaRepository.GetAll();
            return _trainingsSchemaRepository.GetBy(name, categorie, exerciseName);
        }

        // GET: api/TrainingsSchemas/5
        /// <summary>
        /// Get the trainingsSchema with given id
        /// </summary>
        /// <param name="id">the id of the trainingsSchema</param>
        /// <returns>The trainingsSchema</returns>
        [HttpGet("{id}")]
        public ActionResult<TrainingsSchema> GetTrainingsSchema(int id)
        {
            TrainingsSchema trainingsSchema = _trainingsSchemaRepository.GetBy(id);
            if (trainingsSchema == null) return NotFound();
            return trainingsSchema;
        }

        /// <summary>
        /// Get ratings of trainingsschemas with the provided id's by current user
        /// </summary>
        /// 
        [HttpGet("Rated")]
        public IEnumerable<RatedTrainingsSchemaDTO> GetRatedTrainingsSchemas([FromQuery(Name = "id")] int[] trainingsSchemaIds)
        {
            Trainee trainee = _traineeRepository.GetBy(User.Identity.Name);
            List<RatedTrainingsSchemaDTO> ratedTrainingsSchemas = new List<RatedTrainingsSchemaDTO>();
            foreach (var ratedTrainingsSchema in trainee.RatedTrainingsSchemas.Where(p => trainingsSchemaIds.Contains(p.Key.Id)))
            {
                ratedTrainingsSchemas.Add(new RatedTrainingsSchemaDTO(ratedTrainingsSchema.Key, ratedTrainingsSchema.Value));
            }
            return ratedTrainingsSchemas;
        }

        /// <summary>
        /// Rate a trainingsschema with a certain id
        /// </summary>
        /// <param name="id">id of the trainingsschema being rated</param>
        /// <param name="rating">rating of the trainingsschema</param>
        [HttpPut("Rate/{id}/{rating}")]
        public ActionResult<RatedTrainingsSchemaDTO> RateTrainingsSchema(int id, int rating)
        {
            Trainee trainee = _traineeRepository.GetBy(User.Identity.Name);
            TrainingsSchema trainingsSchema = _trainingsSchemaRepository.GetBy(id);
            if (trainingsSchema == null)
            {
                return NotFound();
            }
            trainee.RateTrainingsSchema(trainingsSchema, rating);
            _traineeRepository.SaveChanges();
            return new RatedTrainingsSchemaDTO(trainingsSchema, rating);
        }

        // POST: api/TrainingsSchemas
        /// <summary>
        /// Adds a new trainingsSchema
        /// </summary>
        /// <param name="trainingsSchema">the new trainingsSchema</param>
        [HttpPost]
        public ActionResult<TrainingsSchema> PostTrainingsSchema(TrainingsSchemaDTO trainingsSchema)
        {
            TrainingsSchema trainingsSchemaToCreate = new TrainingsSchema() { Name = trainingsSchema.Name, Categorie = trainingsSchema.categorie };
            foreach (var i in trainingsSchema.Exercises)
                trainingsSchemaToCreate.AddExercise(new Exercise(i.Name, i.Sets, i.Reps));
            _trainingsSchemaRepository.Add(trainingsSchemaToCreate);
            _trainingsSchemaRepository.SaveChanges();

            return CreatedAtAction(nameof(GetTrainingsSchema), new { id = trainingsSchemaToCreate.Id }, trainingsSchemaToCreate);
        }

        // PUT: api/TrainingsSchemas/5
        /// <summary>
        /// Modifies a trainingsSchema
        /// </summary>
        /// <param name="id">id of the trainingsSchema to be modified</param>
        /// <param name="trainingsSchema">the modified trainingsSchema</param>
        [HttpPut("{id}")]
        public IActionResult PutTrainingsSchema(int id, TrainingsSchema trainingsSchema)
        {
            if (id != trainingsSchema.Id)
            {
                return BadRequest();
            }
            _trainingsSchemaRepository.Update(trainingsSchema);
            _trainingsSchemaRepository.SaveChanges();
            return NoContent();
        }

        // DELETE: api/TrainingsSchemas/5
        /// <summary>
        /// Deletes a trainingsSchema
        /// </summary>
        /// <param name="id">the id of the trainingsSchema to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<TrainingsSchema> DeleteTrainingsSchema(int id)
        {
            TrainingsSchema trainingsSchema = _trainingsSchemaRepository.GetBy(id);
            if (trainingsSchema == null)
            {
                return NotFound();
            }
            _trainingsSchemaRepository.Delete(trainingsSchema);
            _trainingsSchemaRepository.SaveChanges();
            return trainingsSchema;
        }

        /// <summary>
        /// Get an ingredient for a trainingsSchema
        /// </summary>
        /// <param name="id">id of the trainingsSchema</param>
        /// <param name="exerciseId">id of the exercise</param>
        [HttpGet("{id}/exercises/{exerciseId}")]
        public ActionResult<Exercise> GetExercise(int id, int exerciseId)
        {
            if (!_trainingsSchemaRepository.TryGetTrainingsSchema(id, out var trainingsSchema))
            {
                return NotFound();
            }
            Exercise exercise = trainingsSchema.GetExercise(exerciseId);
            if (exercise == null)
                return NotFound();
            return exercise;
        }

        /// <summary>
        /// Adds an exercise to a trainingsSchema
        /// </summary>
        /// <param name="id">the id of the trainingsSchema</param>
        /// <param name="exercise">the exercise to be added</param>
        [HttpPost("{id}/exercises")]
        public ActionResult<Exercise> PostExercise(int id, ExerciseDTO exercise)
        {
            if (!_trainingsSchemaRepository.TryGetTrainingsSchema(id, out var trainingsSchema))
            {
                return NotFound();
            }
            var exerciseToCreate = new Exercise(exercise.Name, exercise.Reps, exercise.Sets);
            trainingsSchema.AddExercise(exerciseToCreate);
            _trainingsSchemaRepository.SaveChanges();
            return CreatedAtAction("GetExercise", new { id = trainingsSchema.Id, exerciseId = exerciseToCreate.Id }, exerciseToCreate);
        }
    }
}