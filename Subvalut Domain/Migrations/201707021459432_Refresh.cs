namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refresh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(),
                        PosterURL = c.String(),
                        BackdropURL = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemCastMembers",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        CastMemberId = c.Int(nullable: false),
                        Character = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ItemId, t.CastMemberId, t.Character })
                .ForeignKey("dbo.CastMembers", t => t.CastMemberId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.CastMemberId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemCrewMembers",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        CrewMemberId = c.Int(nullable: false),
                        Job = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ItemId, t.CrewMemberId, t.Job })
                .ForeignKey("dbo.CrewMembers", t => t.CrewMemberId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.CrewMemberId);
            
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
                .ForeignKey("dbo.Movies", t => t.Item_Id)
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
            
            CreateTable(
                "dbo.ItemGenres",
                c => new
                    {
                        Item_Id = c.Int(nullable: false),
                        Genre_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Item_Id, t.Genre_Id })
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Item_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.CastMembers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CrewMembers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Id", "dbo.Items");
            DropForeignKey("dbo.CrewMembers", "Id", "dbo.People");
            DropForeignKey("dbo.CastMembers", "Id", "dbo.People");
            DropForeignKey("dbo.Subtitles", "Uploader_Username", "dbo.Users");
            DropForeignKey("dbo.Subtitles", "Item_Id", "dbo.Movies");
            DropForeignKey("dbo.ItemCrewMembers", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemCrewMembers", "CrewMemberId", "dbo.CrewMembers");
            DropForeignKey("dbo.ItemCastMembers", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemCastMembers", "CastMemberId", "dbo.CastMembers");
            DropForeignKey("dbo.ItemGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.ItemGenres", "Item_Id", "dbo.Items");
            DropIndex("dbo.Movies", new[] { "Id" });
            DropIndex("dbo.CrewMembers", new[] { "Id" });
            DropIndex("dbo.CastMembers", new[] { "Id" });
            DropIndex("dbo.ItemGenres", new[] { "Genre_Id" });
            DropIndex("dbo.ItemGenres", new[] { "Item_Id" });
            DropIndex("dbo.Subtitles", new[] { "Uploader_Username" });
            DropIndex("dbo.Subtitles", new[] { "Item_Id" });
            DropIndex("dbo.ItemCrewMembers", new[] { "CrewMemberId" });
            DropIndex("dbo.ItemCrewMembers", new[] { "ItemId" });
            DropIndex("dbo.ItemCastMembers", new[] { "CastMemberId" });
            DropIndex("dbo.ItemCastMembers", new[] { "ItemId" });
            DropTable("dbo.Movies");
            DropTable("dbo.CrewMembers");
            DropTable("dbo.CastMembers");
            DropTable("dbo.ItemGenres");
            DropTable("dbo.Users");
            DropTable("dbo.Subtitles");
            DropTable("dbo.ItemCrewMembers");
            DropTable("dbo.People");
            DropTable("dbo.ItemCastMembers");
            DropTable("dbo.Items");
            DropTable("dbo.Genres");
        }
    }
}
