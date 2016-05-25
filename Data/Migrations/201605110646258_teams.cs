namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamLeader_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.TeamLeader_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.TeamLeader_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Users", "Team_Id", c => c.Int());
            CreateIndex("dbo.Users", "Team_Id");
            AddForeignKey("dbo.Users", "Team_Id", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "TeamLeader_Id", "dbo.Users");
            DropIndex("dbo.Teams", new[] { "User_Id" });
            DropIndex("dbo.Teams", new[] { "TeamLeader_Id" });
            DropIndex("dbo.Users", new[] { "Team_Id" });
            DropColumn("dbo.Users", "Team_Id");
            DropTable("dbo.Teams");
        }
    }
}
