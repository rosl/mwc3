namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrapesToWIne2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wine", "IsSparkling", c => c.Boolean(nullable: false));
            AddColumn("dbo.Wine", "IsFortified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wine", "IsFortified");
            DropColumn("dbo.Wine", "IsSparkling");
        }
    }
}
