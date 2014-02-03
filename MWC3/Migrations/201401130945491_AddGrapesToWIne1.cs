namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrapesToWIne1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Business", "Owner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Business", "Owner");
        }
    }
}
