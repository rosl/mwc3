namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transaction", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transaction", "Comment");
        }
    }
}
