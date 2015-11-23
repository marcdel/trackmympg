namespace TrackMyMpg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMakeIdToVehicle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vehicles", "Make_Id", "dbo.Makes");
            DropIndex("dbo.Vehicles", new[] { "Make_Id" });
            RenameColumn(table: "dbo.Vehicles", name: "Make_Id", newName: "MakeId");
            AlterColumn("dbo.Vehicles", "MakeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Vehicles", "MakeId");
            AddForeignKey("dbo.Vehicles", "MakeId", "dbo.Makes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "MakeId", "dbo.Makes");
            DropIndex("dbo.Vehicles", new[] { "MakeId" });
            AlterColumn("dbo.Vehicles", "MakeId", c => c.Int());
            RenameColumn(table: "dbo.Vehicles", name: "MakeId", newName: "Make_Id");
            CreateIndex("dbo.Vehicles", "Make_Id");
            AddForeignKey("dbo.Vehicles", "Make_Id", "dbo.Makes", "Id");
        }
    }
}
