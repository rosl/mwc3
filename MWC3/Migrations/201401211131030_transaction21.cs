namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transaction21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LanguageInfo", "CultureCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LanguageInfo", "CultureCode");
        }
    }
}
