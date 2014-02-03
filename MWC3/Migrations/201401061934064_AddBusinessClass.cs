namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBusinessClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Business",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Url = c.String(),
                        Phone = c.String(),
                        IsDistributor = c.Boolean(nullable: false),
                        IsProducer = c.Boolean(nullable: false),
                        Address = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        CountryId = c.Int(nullable: false),
                        AddedBy = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Business", "CountryId", "dbo.Country");
            DropIndex("dbo.Business", new[] { "CountryId" });
            DropTable("dbo.Business");
        }
    }
}
