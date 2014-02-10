namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddREview12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WineId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        ReviewerId = c.String(),
                        Score = c.Double(nullable: false),
                        ReviewTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReviewType", t => t.ReviewTypeId, cascadeDelete: true)
                .Index(t => t.ReviewTypeId);
            
            CreateTable(
                "dbo.ReviewType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartValue = c.Int(nullable: false),
                        EndValue = c.Int(nullable: false),
                        ValueType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "ReviewTypeId", "dbo.ReviewType");
            DropIndex("dbo.Review", new[] { "ReviewTypeId" });
            DropTable("dbo.ReviewType");
            DropTable("dbo.Review");
        }
    }
}
