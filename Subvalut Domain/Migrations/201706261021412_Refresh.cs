namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refresh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(),
                        PosterURL = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subtitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                        SyncType = c.String(),
                        ForHearingImpaired = c.Boolean(nullable: false),
                        IsForeignOnly = c.Boolean(nullable: false),
                        FileName = c.String(),
                        FilePath = c.String(),
                        Item_Id = c.Int(),
                        Uploader_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .ForeignKey("dbo.Users", t => t.Uploader_Username)
                .Index(t => t.Item_Id)
                .Index(t => t.Uploader_Username);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        HashedPassword = c.String(),
                        Salt = c.String(),
                        EmailAdress = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subtitles", "Uploader_Username", "dbo.Users");
            DropForeignKey("dbo.Subtitles", "Item_Id", "dbo.Items");
            DropIndex("dbo.Subtitles", new[] { "Uploader_Username" });
            DropIndex("dbo.Subtitles", new[] { "Item_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Subtitles");
            DropTable("dbo.Items");
        }
    }
}
