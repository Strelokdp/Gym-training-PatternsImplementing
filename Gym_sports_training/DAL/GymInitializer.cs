using Gym_sports_training.Repository.DAL;
using Gym_sports_training.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gym_sports_training.DAL
{
    public class GymInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<GymContext>
    {
        protected override void Seed(GymContext context)
        {
            var clients = new List<Client>
            {
                new Client { Name = "Bob", LastName = "Smith", EMail="bob.smith@mail.ru", PhoneNumber= "0671916712" },
                new Client { Name = "Tony", LastName = "Adams", EMail = "tony.adams@mail.ru", PhoneNumber="0671581561" },
                new Client { Name = "Adam", LastName = "Scott", EMail = "adam.scott@mail.ru", PhoneNumber = "0931385858" }
            };

            clients.ForEach(g => context.Clients.Add(g));
            context.SaveChanges();

            var coaches = new List<Coach>
            {
                new Coach {Name="Jack", LastName = "Hobbs", Speciality=Speciality.Cardio, Price=30, TrainingLength=45, Description="Good coach" },
                new Coach {Name="Miki", LastName = "Roque", Speciality=Speciality.Fitness, Price=25, TrainingLength=60, Description="Good coach" },
                new Coach {Name="Craig", LastName = "Lindfield", Speciality=Speciality.Boxing, Price=35, TrainingLength=40, Description="Good coach" },
            };

            coaches.ForEach(g => context.Coaches.Add(g));
            context.SaveChanges();

            var trainingSessions = new List<TrainingSession>
            {
                new TrainingSession { ClientId = 1, CoachId = 1, TrainingTimeStart = DateTime.Now.AddDays(-10)},
                new TrainingSession { ClientId = 2, CoachId = 2, TrainingTimeStart = DateTime.Now},
                new TrainingSession { ClientId = 3, CoachId = 3, TrainingTimeStart = DateTime.Now.AddDays(10) },
                new TrainingSession { ClientId = 1, CoachId = 3, TrainingTimeStart = DateTime.Now.AddDays(12) },
                new TrainingSession { ClientId = 3, CoachId = 2, TrainingTimeStart = DateTime.Now.AddDays(-5) }
            };

            trainingSessions.ForEach(g => context.TrainingSessions.Add(g));
            context.SaveChanges();
        }
    }
}