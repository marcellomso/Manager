namespace Manager.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fuels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OpportunitesLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Update = c.DateTime(nullable: false),
                        VendorId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OpportunityStatus", t => t.StatusId)
                .ForeignKey("dbo.Vendors", t => t.VendorId)
                .Index(t => t.VendorId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.OpportunityStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoleId = c.Int(nullable: false),
                        CustomCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Commission = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Opportunities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        Expiration = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OpportunityStatus", t => t.StatusId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId)
                .ForeignKey("dbo.Vendors", t => t.VendorId)
                .Index(t => t.VehicleId)
                .Index(t => t.VendorId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.String(),
                        Model = c.String(),
                        FuelId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsSold = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fuels", t => t.FuelId)
                .Index(t => t.FuelId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.VendorId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Opportunities", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Opportunities", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "FuelId", "dbo.Fuels");
            DropForeignKey("dbo.Opportunities", "StatusId", "dbo.OpportunityStatus");
            DropForeignKey("dbo.OpportunitesLogs", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Vendors", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.OpportunitesLogs", "StatusId", "dbo.OpportunityStatus");
            DropIndex("dbo.Users", new[] { "VendorId" });
            DropIndex("dbo.Vehicles", new[] { "FuelId" });
            DropIndex("dbo.Opportunities", new[] { "StatusId" });
            DropIndex("dbo.Opportunities", new[] { "VendorId" });
            DropIndex("dbo.Opportunities", new[] { "VehicleId" });
            DropIndex("dbo.Vendors", new[] { "RoleId" });
            DropIndex("dbo.OpportunitesLogs", new[] { "StatusId" });
            DropIndex("dbo.OpportunitesLogs", new[] { "VendorId" });
            DropTable("dbo.Users");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Opportunities");
            DropTable("dbo.Roles");
            DropTable("dbo.Vendors");
            DropTable("dbo.OpportunityStatus");
            DropTable("dbo.OpportunitesLogs");
            DropTable("dbo.Fuels");
        }
    }
}
