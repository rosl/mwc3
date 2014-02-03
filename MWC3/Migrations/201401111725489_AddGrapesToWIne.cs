namespace MWC3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrapesToWIne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegionId = c.Int(),
                        WineColorId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        QualificationId = c.Int(),
                        BusinessId = c.Int(nullable: false),
                        AddedBy = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Business", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .ForeignKey("dbo.Qualification", t => t.QualificationId)
                .ForeignKey("dbo.Region", t => t.RegionId)
                .ForeignKey("dbo.WineColor", t => t.WineColorId, cascadeDelete: true)
                .ForeignKey("dbo.Country", t => t.Country_Id)
                .Index(t => t.BusinessId)
                .Index(t => t.CountryId)
                .Index(t => t.QualificationId)
                .Index(t => t.RegionId)
                .Index(t => t.WineColorId)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.GrapeWine",
                c => new
                    {
                        Grape_Id = c.Int(nullable: false),
                        Wine_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Grape_Id, t.Wine_Id })
                .ForeignKey("dbo.Grape", t => t.Grape_Id, cascadeDelete: true)
                .ForeignKey("dbo.Wine", t => t.Wine_Id, cascadeDelete: true)
                .Index(t => t.Grape_Id)
                .Index(t => t.Wine_Id);
            
            AlterColumn("dbo.WineColor", "LanguageCode", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wine", "Country_Id", "dbo.Country");
            DropForeignKey("dbo.Wine", "WineColorId", "dbo.WineColor");
            DropForeignKey("dbo.Wine", "RegionId", "dbo.Region");
            DropForeignKey("dbo.Wine", "QualificationId", "dbo.Qualification");
            DropForeignKey("dbo.GrapeWine", "Wine_Id", "dbo.Wine");
            DropForeignKey("dbo.GrapeWine", "Grape_Id", "dbo.Grape");
            DropForeignKey("dbo.Wine", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Wine", "BusinessId", "dbo.Business");
            DropIndex("dbo.Wine", new[] { "Country_Id" });
            DropIndex("dbo.Wine", new[] { "WineColorId" });
            DropIndex("dbo.Wine", new[] { "RegionId" });
            DropIndex("dbo.Wine", new[] { "QualificationId" });
            DropIndex("dbo.GrapeWine", new[] { "Wine_Id" });
            DropIndex("dbo.GrapeWine", new[] { "Grape_Id" });
            DropIndex("dbo.Wine", new[] { "CountryId" });
            DropIndex("dbo.Wine", new[] { "BusinessId" });
            AlterColumn("dbo.WineColor", "LanguageCode", c => c.String());
            DropTable("dbo.GrapeWine");
            DropTable("dbo.Wine");
        }
    }
}
