namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemPersonmanytomanyfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.People", "Item_Id1", "dbo.Items");
            DropForeignKey("dbo.Items", "Person_Id", "dbo.People");
            DropIndex("dbo.Items", new[] { "Person_Id" });
            DropIndex("dbo.People", new[] { "Item_Id" });
            DropIndex("dbo.People", new[] { "Item_Id1" });
            CreateTable(
                "dbo.PersonItems",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Item_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Item_Id })
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.Item_Id);
            
            DropColumn("dbo.Items", "Person_Id");
            DropColumn("dbo.People", "Item_Id");
            DropColumn("dbo.People", "Item_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Item_Id1", c => c.Int());
            AddColumn("dbo.People", "Item_Id", c => c.Int());
            AddColumn("dbo.Items", "Person_Id", c => c.Int());
            DropForeignKey("dbo.PersonItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.PersonItems", "Person_Id", "dbo.People");
            DropIndex("dbo.PersonItems", new[] { "Item_Id" });
            DropIndex("dbo.PersonItems", new[] { "Person_Id" });
            DropTable("dbo.PersonItems");
            CreateIndex("dbo.People", "Item_Id1");
            CreateIndex("dbo.People", "Item_Id");
            CreateIndex("dbo.Items", "Person_Id");
            AddForeignKey("dbo.Items", "Person_Id", "dbo.People", "Id");
            AddForeignKey("dbo.People", "Item_Id1", "dbo.Items", "Id");
            AddForeignKey("dbo.People", "Item_Id", "dbo.Items", "Id");
        }
    }
}
