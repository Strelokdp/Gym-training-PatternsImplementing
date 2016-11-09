using Gym_sports_training.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_sports_training.Repository.Contracts
{
    public interface ICoachRepository
    {
        IEnumerable<Coach> GetCoaches();
        Coach GetCoachByID(int? coachId);
        void InsertCoach(Coach coach);
        void DeleteCoach(int coachId);
        void UpdateCoach(Coach coach);
        void Save();
        void Dispose();
    }
}
