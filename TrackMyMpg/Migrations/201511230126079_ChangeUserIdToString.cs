namespace TrackMyMpg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserIdToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "Userid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "Userid", c => c.Int(nullable: false));
        }
    }
}
