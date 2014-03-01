namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlcohol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "Alcohol", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transaction", "Alcohol");
        }
    }
}
