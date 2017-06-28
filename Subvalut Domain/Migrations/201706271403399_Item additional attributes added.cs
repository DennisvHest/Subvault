namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Itemadditionalattributesadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Description", c => c.String());
            AddColumn("dbo.Items", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "BackdropURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "BackdropURL");
            DropColumn("dbo.Items", "ReleaseDate");
            DropColumn("dbo.Items", "Description");
        }
    }
}
