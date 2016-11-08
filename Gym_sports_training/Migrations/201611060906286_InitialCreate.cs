namespace Gym_sports_training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        EMail = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrainingSession",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        CoachId = c.Int(nullable: false),
                        TrainingTimeStart = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Coach", t => t.CoachId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.CoachId);
            
            CreateTable(
                "dbo.Coach",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Speciality = c.Int(),
                        Price = c.Int(nullable: false),
                        TrainingLength = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainingSession", "CoachId", "dbo.Coach");
            DropForeignKey("dbo.TrainingSession", "ClientId", "dbo.Client");
            DropIndex("dbo.TrainingSession", new[] { "CoachId" });
            DropIndex("dbo.TrainingSession", new[] { "ClientId" });
            DropTable("dbo.Coach");
            DropTable("dbo.TrainingSession");
            DropTable("dbo.Client");
        }
    }
}
