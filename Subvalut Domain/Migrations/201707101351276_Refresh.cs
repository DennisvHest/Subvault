namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refresh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CastMembers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
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
                .ForeignKey("dbo.CastMembers", t => t.CastMemberId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.CastMemberId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        ReleaseDate = c.DateTime(),
                        PosterPath = c.String(),
                        BackdropURL = c.String(),
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
                .ForeignKey("dbo.CrewMembers", t => t.CrewMemberId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.CrewMemberId);
            
            CreateTable(
                "dbo.CrewMembers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemGenres",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemId, t.GenreId })
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
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
                        Episode_Id = c.Int(),
                        Movie_Id = c.Int(),
                        Uploader_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Episodes", t => t.Episode_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .ForeignKey("dbo.Users", t => t.Uploader_Username)
                .Index(t => t.Episode_Id)
                .Index(t => t.Movie_Id)
                .Index(t => t.Uploader_Username);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EpisodeNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Overview = c.String(),
                        StillPath = c.String(),
                        AirDate = c.DateTime(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.Season_Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Overview = c.String(),
                        PosterPath = c.String(),
                        AirDate = c.DateTime(),
                        Series_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Series_Id)
                .Index(t => t.Series_Id);
            
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
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Series",
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
            DropForeignKey("dbo.Series", "Id", "dbo.Items");
            DropForeignKey("dbo.Movies", "Id", "dbo.Items");
            DropForeignKey("dbo.Subtitles", "Uploader_Username", "dbo.Users");
            DropForeignKey("dbo.Subtitles", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Subtitles", "Episode_Id", "dbo.Episodes");
            DropForeignKey("dbo.Seasons", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.Episodes", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.ItemGenres", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.ItemCrewMembers", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemCrewMembers", "CrewMemberId", "dbo.CrewMembers");
            DropForeignKey("dbo.ItemCastMembers", "ItemId", "dbo.Items");
            DropForeignKey("dbo.ItemCastMembers", "CastMemberId", "dbo.CastMembers");
            DropIndex("dbo.Series", new[] { "Id" });
            DropIndex("dbo.Movies", new[] { "Id" });
            DropIndex("dbo.Seasons", new[] { "Series_Id" });
            DropIndex("dbo.Episodes", new[] { "Season_Id" });
            DropIndex("dbo.Subtitles", new[] { "Uploader_Username" });
            DropIndex("dbo.Subtitles", new[] { "Movie_Id" });
            DropIndex("dbo.Subtitles", new[] { "Episode_Id" });
            DropIndex("dbo.ItemGenres", new[] { "GenreId" });
            DropIndex("dbo.ItemGenres", new[] { "ItemId" });
            DropIndex("dbo.ItemCrewMembers", new[] { "CrewMemberId" });
            DropIndex("dbo.ItemCrewMembers", new[] { "ItemId" });
            DropIndex("dbo.ItemCastMembers", new[] { "CastMemberId" });
            DropIndex("dbo.ItemCastMembers", new[] { "ItemId" });
            DropTable("dbo.Series");
            DropTable("dbo.Movies");
            DropTable("dbo.Users");
            DropTable("dbo.Seasons");
            DropTable("dbo.Episodes");
            DropTable("dbo.Subtitles");
            DropTable("dbo.Genres");
            DropTable("dbo.ItemGenres");
            DropTable("dbo.CrewMembers");
            DropTable("dbo.ItemCrewMembers");
            DropTable("dbo.Items");
            DropTable("dbo.ItemCastMembers");
            DropTable("dbo.CastMembers");
        }
    }
}
