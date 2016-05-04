namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGroups", "User_Id", "dbo.Users");
            DropIndex("dbo.UserGroups", new[] { "User_Id" });
            CreateTable(
                "dbo.UserGroupUsers",
                c => new
                    {
                        UserGroup_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_Id, t.User_Id })
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id)
                .Index(t => t.User_Id);
            
            DropColumn("dbo.UserGroups", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserGroups", "User_Id", c => c.Int());
            DropForeignKey("dbo.UserGroupUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserGroupUsers", "UserGroup_Id", "dbo.UserGroups");
            DropIndex("dbo.UserGroupUsers", new[] { "User_Id" });
            DropIndex("dbo.UserGroupUsers", new[] { "UserGroup_Id" });
            DropTable("dbo.UserGroupUsers");
            CreateIndex("dbo.UserGroups", "User_Id");
            AddForeignKey("dbo.UserGroups", "User_Id", "dbo.Users", "Id");
        }
    }
}
