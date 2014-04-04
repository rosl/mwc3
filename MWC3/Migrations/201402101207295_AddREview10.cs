namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddREview10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wine", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Wine", "CountryId", "dbo.Country");
            DropIndex("dbo.Wine", new[] { "CountryId" });
            DropIndex("dbo.Wine", new[] { "CountryId" });
            AddColumn("dbo.Wine", "Country_Id", c => c.Int());
            CreateIndex("dbo.Wine", "CountryId");
            CreateIndex("dbo.Wine", "Country_Id");
            AddForeignKey("dbo.Wine", "CountryId", "dbo.Country", "Id");
            AddForeignKey("dbo.Wine", "Country_Id", "dbo.Country", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wine", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.Wine", "CountryId", "dbo.Country");
            DropIndex("dbo.Wine", new[] { "Country_Id" });
            DropIndex("dbo.Wine", new[] { "CountryId" });
            DropColumn("dbo.Wine", "Country_Id");
            CreateIndex("dbo.Wine", "CountryId");
            CreateIndex("dbo.Wine", "CountryId");
            AddForeignKey("dbo.Wine", "CountryId", "dbo.Country", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Wine", "CountryId", "dbo.Country", "Id", cascadeDelete: true);
        }
    }
}
