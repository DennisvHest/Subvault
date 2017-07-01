namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemPersonmanytomany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Person_Id", c => c.Int());
            AddColumn("dbo.People", "Item_Id", c => c.Int());
            AddColumn("dbo.People", "Item_Id1", c => c.Int());
            CreateIndex("dbo.Items", "Person_Id");
            CreateIndex("dbo.People", "Item_Id");
            CreateIndex("dbo.People", "Item_Id1");
            AddForeignKey("dbo.People", "Item_Id", "dbo.Items", "Id");
            AddForeignKey("dbo.People", "Item_Id1", "dbo.Items", "Id");
            AddForeignKey("dbo.Items", "Person_Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Person_Id", "dbo.People");
            DropForeignKey("dbo.People", "Item_Id1", "dbo.Items");
            DropForeignKey("dbo.People", "Item_Id", "dbo.Items");
            DropIndex("dbo.People", new[] { "Item_Id1" });
            DropIndex("dbo.People", new[] { "Item_Id" });
            DropIndex("dbo.Items", new[] { "Person_Id" });
            DropColumn("dbo.People", "Item_Id1");
            DropColumn("dbo.People", "Item_Id");
            DropColumn("dbo.Items", "Person_Id");
        }
    }
}
