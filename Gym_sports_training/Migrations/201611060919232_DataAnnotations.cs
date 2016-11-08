namespace Gym_sports_training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coach", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Coach", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Coach", "Speciality", c => c.Int(nullable: false));
            AlterColumn("dbo.Coach", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Coach", "Description", c => c.String());
            AlterColumn("dbo.Coach", "Speciality", c => c.Int());
            AlterColumn("dbo.Coach", "LastName", c => c.String());
            AlterColumn("dbo.Coach", "Name", c => c.String());
        }
    }
}
