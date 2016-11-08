using Gym_sports_training.Repository.Models;
using System.Collections.Generic;

namespace Gym_sports_training.Repository.DAL
{
    public interface ITrainingSessionRepository
    {
        IEnumerable<TrainingSession> GetTrainingSessions();
        TrainingSession GetTrainingSessionByID(int? trainingSessionId);
        void InsertTrainingSession(TrainingSession trainingSession);
        void DeleteTrainingSession(int trainingSessionId);
        void UpdateTrainingSession(TrainingSession trainingSession);
        void Save();
        void Dispose();
    }
}
