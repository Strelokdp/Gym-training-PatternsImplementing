using Gym_sports_training.Repository.Models;
using System;

namespace Gym_sports_training.Repository.DAL
{
    public class UnitOfWork : IDisposable
    {
        private GymContext context = new GymContext();
        private GenericRepository<Client> clientRepository;
        private GenericRepository<TrainingSession> trainingSessionRepository;
        private GenericRepository<Coach> coachRepository;

        public GenericRepository<Client> ClientRepository
        {
            get
            {

                if (this.clientRepository == null)
                {
                    this.clientRepository = new GenericRepository<Client>(context);
                }
                return clientRepository;
            }
        }

        public GenericRepository<Coach> CoachRepository
        {
            get
            {

                if (this.coachRepository == null)
                {
                    this.coachRepository = new GenericRepository<Coach>(context);
                }
                return coachRepository;
            }
        }

        public GenericRepository<TrainingSession> TrainingSessionRepository
        {
            get
            {

                if (this.trainingSessionRepository == null)
                {
                    this.trainingSessionRepository = new GenericRepository<TrainingSession>(context);
                }
                return trainingSessionRepository;
            }
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
