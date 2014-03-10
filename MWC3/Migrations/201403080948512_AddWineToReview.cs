namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWineToReview : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Review", "WineId");
            AddForeignKey("dbo.Review", "WineId", "dbo.Wine", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "WineId", "dbo.Wine");
            DropIndex("dbo.Review", new[] { "WineId" });
        }
    }
}
