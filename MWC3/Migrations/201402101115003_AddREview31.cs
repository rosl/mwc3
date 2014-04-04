namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddREview31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Country", "TempCuntry", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Country", "TempCuntry");
        }
    }
}
