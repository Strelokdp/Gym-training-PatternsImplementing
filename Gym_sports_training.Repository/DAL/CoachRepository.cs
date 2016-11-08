using Gym_sports_training.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Gym_sports_training.Repository.DAL
{
    public class CoachRepository: ICoachRepository, IDisposable
    {
        private GymContext context;

        public CoachRepository(GymContext context)
        {
            this.context = context;
        }

        public IEnumerable<Coach> GetCoaches()
        {
            return context.Coaches.ToList();
        }

        public Coach GetCoachByID(int? id)
        {
            return context.Coaches.Find(id);
        }

        public void InsertCoach(Coach coach)
        {
            context.Coaches.Add(coach);
        }

        public void DeleteCoach(int coachId)
        {
            Coach coach = context.Coaches.Find(coachId);
            context.Coaches.Remove(coach);
        }

        public void UpdateCoach(Coach coach)
        {
            context.Entry(coach).State = EntityState.Modified;
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
