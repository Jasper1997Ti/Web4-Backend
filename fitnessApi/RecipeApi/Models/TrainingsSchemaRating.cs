using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Models
{
    public class TrainingsSchemaRating
    {
        #region Properties
        public int UserId { get; set; }

        public int TrainingsSchemaId { get; set; }

        public IdentityUser User { get; set; }

        public TrainingsSchema TrainingsSchema { get; set; }

        public int Rating { get; set; }
        #endregion
    }
}
