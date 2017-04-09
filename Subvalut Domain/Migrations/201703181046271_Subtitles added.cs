namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subtitlesadded : DbMigration
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
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubtitlesMovies",
                c => new
                    {
                        Subtitles_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subtitles_Id, t.Movie_Id })
                .ForeignKey("dbo.Subtitles", t => t.Subtitles_Id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Subtitles_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubtitlesMovies", "Movie_Id", "dbo.Items");
            DropForeignKey("dbo.SubtitlesMovies", "Subtitles_Id", "dbo.Subtitles");
            DropIndex("dbo.SubtitlesMovies", new[] { "Movie_Id" });
            DropIndex("dbo.SubtitlesMovies", new[] { "Subtitles_Id" });
            DropTable("dbo.SubtitlesMovies");
            DropTable("dbo.Subtitles");
            DropTable("dbo.Items");
        }
    }
}
