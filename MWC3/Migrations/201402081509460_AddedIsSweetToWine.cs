namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsSweetToWine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wine", "IsSweet", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wine", "IsSweet");
        }
    }
}
