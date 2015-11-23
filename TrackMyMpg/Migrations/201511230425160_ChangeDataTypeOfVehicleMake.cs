namespace TrackMyMpg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDataTypeOfVehicleMake : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Make_Id", c => c.Int());
            CreateIndex("dbo.Vehicles", "Make_Id");
            AddForeignKey("dbo.Vehicles", "Make_Id", "dbo.Makes", "Id");
            DropColumn("dbo.Vehicles", "Make");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Make", c => c.String());
            DropForeignKey("dbo.Vehicles", "Make_Id", "dbo.Makes");
            DropIndex("dbo.Vehicles", new[] { "Make_Id" });
            DropColumn("dbo.Vehicles", "Make_Id");
        }
    }
}
