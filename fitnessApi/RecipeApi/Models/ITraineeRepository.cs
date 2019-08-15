using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Models
{
    public interface ITraineeRepository
    {
        Trainee GetBy(string email);
        void Add(Trainee trainee);
        void SaveChanges();
    }
}
