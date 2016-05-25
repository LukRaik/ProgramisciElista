namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGroupUsers", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroupUsers", "User_Id", "dbo.Users");
            DropIndex("dbo.UserGroupUsers", new[] { "UserGroup_Id" });
            DropIndex("dbo.UserGroupUsers", new[] { "User_Id" });
            AddColumn("dbo.Users", "UserGroup_Id", c => c.Int());
            CreateIndex("dbo.Users", "UserGroup_Id");
            AddForeignKey("dbo.Users", "UserGroup_Id", "dbo.UserGroups", "Id");
            DropTable("dbo.UserGroupUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserGroupUsers",
                c => new
                    {
                        UserGroup_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_Id, t.User_Id });
            
            DropForeignKey("dbo.Users", "UserGroup_Id", "dbo.UserGroups");
            DropIndex("dbo.Users", new[] { "UserGroup_Id" });
            DropColumn("dbo.Users", "UserGroup_Id");
            CreateIndex("dbo.UserGroupUsers", "User_Id");
            CreateIndex("dbo.UserGroupUsers", "UserGroup_Id");
            AddForeignKey("dbo.UserGroupUsers", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserGroupUsers", "UserGroup_Id", "dbo.UserGroups", "Id", cascadeDelete: true);
        }
    }
}
