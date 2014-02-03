namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Transaction1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TransactionTypeId = c.Int(nullable: false),
                        UserId = c.String(),
                        BusinessId = c.Int(),
                        BottleTypeId = c.Int(nullable: false),
                        WineId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        AddedBy = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BottleType", t => t.BottleTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Wine", t => t.WineId, cascadeDelete: true)
                .ForeignKey("dbo.Business", t => t.BusinessId)
                .ForeignKey("dbo.TransactionType", t => t.TransactionTypeId, cascadeDelete: true)
                .Index(t => t.BottleTypeId)
                .Index(t => t.WineId)
                .Index(t => t.BusinessId)
                .Index(t => t.TransactionTypeId);
            
            CreateTable(
                "dbo.TransactionType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Multiplier = c.Int(nullable: false),
                        LanguageCode = c.String(),
                        ParentId = c.Int(nullable: false),
                        AddedBy = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "TransactionTypeId", "dbo.TransactionType");
            DropForeignKey("dbo.Transaction", "BusinessId", "dbo.Business");
            DropForeignKey("dbo.Transaction", "WineId", "dbo.Wine");
            DropForeignKey("dbo.Transaction", "BottleTypeId", "dbo.BottleType");
            DropIndex("dbo.Transaction", new[] { "TransactionTypeId" });
            DropIndex("dbo.Transaction", new[] { "BusinessId" });
            DropIndex("dbo.Transaction", new[] { "WineId" });
            DropIndex("dbo.Transaction", new[] { "BottleTypeId" });
            DropTable("dbo.TransactionType");
            DropTable("dbo.Transaction");
        }
    }
}
