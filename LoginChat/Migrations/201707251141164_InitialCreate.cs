namespace LoginChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        ToID = c.Int(),
                        FromID = c.Int(),
                        MessageContent = c.String(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .ForeignKey("dbo.Users", t => t.FromID)
                .ForeignKey("dbo.Users", t => t.ToID)
                .Index(t => t.ToID)
                .Index(t => t.FromID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ToID", "dbo.Users");
            DropForeignKey("dbo.Messages", "FromID", "dbo.Users");
            DropForeignKey("dbo.Messages", "User_UserID", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "User_UserID" });
            DropIndex("dbo.Messages", new[] { "FromID" });
            DropIndex("dbo.Messages", new[] { "ToID" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
        }
    }
}
