namespace LoginChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initliaze : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ToID", "dbo.Users");
            DropIndex("dbo.Messages", new[] { "ToID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Messages", "ToID");
            AddForeignKey("dbo.Messages", "ToID", "dbo.Users", "UserID");
        }
    }
}
