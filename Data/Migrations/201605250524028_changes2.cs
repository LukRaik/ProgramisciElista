namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkTimes", "HourEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkTimes", "HourEnd", c => c.DateTime(nullable: false));
        }
    }
}
