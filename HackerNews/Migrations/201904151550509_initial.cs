namespace HackerNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        URL = c.String(nullable: false, maxLength: 500, unicode: false),
                        Title = c.String(nullable: false, maxLength: 500, unicode: false),
                        RelatedUserId = c.Int(nullable: false),
                        Vote = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.User", t => t.RelatedUserId)
                .Index(t => t.RelatedUserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "RelatedUserId", "dbo.User");
            DropIndex("dbo.Post", new[] { "RelatedUserId" });
            DropTable("dbo.User");
            DropTable("dbo.Post");
        }
    }
}
