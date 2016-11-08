using Gym_sports_training.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Gym_sports_training.Repository.DAL
{
    public class TrainingSessionRepository: ITrainingSessionRepository, IDisposable
    {
        private GymContext context;

        public TrainingSessionRepository(GymContext context)
        {
            this.context = context;
        }

        public IEnumerable<TrainingSession> GetTrainingSessions()
        {
            return context.TrainingSessions.Include(t => t.Client).Include(t => t.Coach).ToList();
        }

        public TrainingSession GetTrainingSessionByID(int? id)
        {
            return context.TrainingSessions.Find(id);
        }

        public void InsertTrainingSession(TrainingSession trainingSession)
        {
            context.TrainingSessions.Add(trainingSession);
        }

        public void DeleteTrainingSession(int trainingSessionId)
        {
            TrainingSession trainingSession = context.TrainingSessions.Find(trainingSessionId);
            context.TrainingSessions.Remove(trainingSession);
        }

        public void UpdateTrainingSession(TrainingSession trainingSession)
        {
            context.Entry(trainingSession).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
