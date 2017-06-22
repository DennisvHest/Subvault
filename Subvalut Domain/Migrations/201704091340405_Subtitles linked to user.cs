namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subtitleslinkedtouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subtitles", "Uploader_Username", c => c.String(maxLength: 128));
            CreateIndex("dbo.Subtitles", "Uploader_Username");
            AddForeignKey("dbo.Subtitles", "Uploader_Username", "dbo.Users", "Username");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subtitles", "Uploader_Username", "dbo.Users");
            DropIndex("dbo.Subtitles", new[] { "Uploader_Username" });
            DropColumn("dbo.Subtitles", "Uploader_Username");
        }
    }
}
