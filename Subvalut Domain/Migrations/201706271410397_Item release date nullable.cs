namespace Subvault_Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Itemreleasedatenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "ReleaseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "ReleaseDate", c => c.DateTime(nullable: false));
        }
    }
}
