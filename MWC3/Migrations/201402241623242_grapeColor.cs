namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grapeColor : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Grape", "ColorId");
            AddForeignKey("dbo.Grape", "ColorId", "dbo.GrapeColor", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grape", "ColorId", "dbo.GrapeColor");
            DropIndex("dbo.Grape", new[] { "ColorId" });
        }
    }
}
