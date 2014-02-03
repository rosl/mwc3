namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBusiness : DbMigration
    {
        public override void Up()
        {
            CreateTable(
            "Business",
                 c => new
                 {
                      Id = c.Int(nullable: false, identity: true),
                      Name = c.String(),
                  })
                  .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("Business");
        }
    }
}
