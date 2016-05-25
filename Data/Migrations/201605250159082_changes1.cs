namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlansDiaries", "Info", c => c.String());
            AddColumn("dbo.PlansDiaries", "IsArchive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlansDiaries", "IsArchive");
            DropColumn("dbo.PlansDiaries", "Info");
        }
    }
}
