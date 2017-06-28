namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreItemmanytomany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "Item_Id", "dbo.Items");
            DropIndex("dbo.Genres", new[] { "Item_Id" });
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
            
            DropColumn("dbo.Genres", "Item_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "Item_Id", c => c.Int());
            DropForeignKey("dbo.ItemGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.ItemGenres", "Item_Id", "dbo.Items");
            DropIndex("dbo.ItemGenres", new[] { "Genre_Id" });
            DropIndex("dbo.ItemGenres", new[] { "Item_Id" });
            DropTable("dbo.ItemGenres");
            CreateIndex("dbo.Genres", "Item_Id");
            AddForeignKey("dbo.Genres", "Item_Id", "dbo.Items", "Id");
        }
    }
}
