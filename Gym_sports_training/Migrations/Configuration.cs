namespace Gym_sports_training.Migrations
{
    using System.Data.Entity.Migrations;
    using Gym_sports_training.Repository.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<GymContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Gym_sports_training.DAL.GymContext";
        }

        protected override void Seed(GymContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
