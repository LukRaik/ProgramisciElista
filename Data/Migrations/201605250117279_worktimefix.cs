namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class worktimefix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "Name", c => c.String());
            AddColumn("dbo.WorkTimes", "Day", c => c.Int(nullable: false));
            AddColumn("dbo.WorkTimes", "HourStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkTimes", "HourEnd", c => c.DateTime(nullable: false));
            DropColumn("dbo.WorkTimes", "DateStart");
            DropColumn("dbo.WorkTimes", "DateEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkTimes", "DateEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkTimes", "DateStart", c => c.DateTime(nullable: false));
            DropColumn("dbo.WorkTimes", "HourEnd");
            DropColumn("dbo.WorkTimes", "HourStart");
            DropColumn("dbo.WorkTimes", "Day");
            DropColumn("dbo.Teams", "Name");
        }
    }
}
