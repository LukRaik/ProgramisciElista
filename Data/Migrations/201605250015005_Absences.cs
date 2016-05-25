namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Absences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Absences", "Accepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Absences", "Accepted");
        }
    }
}
