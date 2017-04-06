namespace RSS_db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Link = c.String(),
                        Description = c.String(),
                        Category = c.String(),
                        PubDate = c.DateTime(nullable: false),
                        SourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.Sources", t => t.SourceId, cascadeDelete: true)
                .Index(t => t.SourceId);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        SourceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        url = c.String(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.SourceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "SourceId", "dbo.Sources");
            DropIndex("dbo.News", new[] { "SourceId" });
            DropTable("dbo.Sources");
            DropTable("dbo.News");
        }
    }
}
