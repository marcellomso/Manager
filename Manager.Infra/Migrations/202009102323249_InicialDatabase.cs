﻿namespace Manager.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InicialDatabase : DbMigration
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
                "dbo.Opportunities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VeicheId = c.Guid(nullable: false),
                        VendorId = c.Guid(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Vehicle_Id = c.Int(),
                        Vendor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .Index(t => t.Vehicle_Id)
                .Index(t => t.Vendor_Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.String(),
                        Model = c.String(),
                        FuelId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Status = c.String(),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fuels", t => t.FuelId)
                .Index(t => t.FuelId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoleId = c.Guid(nullable: false),
                        CustomCommission = c.Double(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opportunities", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.Vendors", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Opportunities", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "FuelId", "dbo.Fuels");
            DropIndex("dbo.Vendors", new[] { "Role_Id" });
            DropIndex("dbo.Vehicles", new[] { "FuelId" });
            DropIndex("dbo.Opportunities", new[] { "Vendor_Id" });
            DropIndex("dbo.Opportunities", new[] { "Vehicle_Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.Vendors");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Opportunities");
            DropTable("dbo.Fuels");
        }
    }
}
