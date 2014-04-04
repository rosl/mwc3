namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddREview6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Country", "TempCuntry");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Country", "TempCuntry", c => c.String());
        }
    }
}
