namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class worktimeplansdiaryfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlansDiaries", "Day", c => c.Int(nullable: false));
            AddColumn("dbo.WorkTimes", "IsLate", c => c.Boolean(nullable: false));
            DropColumn("dbo.WorkTimes", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkTimes", "Day", c => c.Int(nullable: false));
            DropColumn("dbo.WorkTimes", "IsLate");
            DropColumn("dbo.PlansDiaries", "Day");
        }
    }
}
