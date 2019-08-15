using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessApi.DTOs;
using FitnessApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitnessApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TraineeController : ControllerBase
    {
        
            private readonly ITraineeRepository _traineeRepository;

            public TraineeController(ITraineeRepository traineeRepository)
            {
            _traineeRepository = traineeRepository;
            }

            [HttpGet()]
            public ActionResult<TraineeDTO> GetTrainee()
            {
            Trainee trainee = _traineeRepository.GetBy(User.Identity.Name);
                return new TraineeDTO(trainee);
            


        }
    }
}
