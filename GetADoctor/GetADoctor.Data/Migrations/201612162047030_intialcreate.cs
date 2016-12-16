namespace GetADoctor.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        Address = c.String(nullable: false, maxLength: 100),
                        Zipcode = c.String(maxLength: 10),
                        CityId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CityId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 40),
                        StateId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.CityName, unique: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(nullable: false, maxLength: 40),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.StateName, unique: true);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        WaitingTime = c.String(maxLength: 20),
                        UserId = c.String(maxLength: 128),
                        DoctorId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        UIN = c.String(nullable: false, maxLength: 10),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Specialization = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        MobileNumber = c.String(),
                        ImageUrl = c.String(),
                        SpecialityId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.Specialities", t => t.SpecialityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UIN, unique: true)
                .Index(t => t.SpecialityId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        DoctorId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Specialities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 80),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        MobileNumber = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        VisitingTime = c.String(),
                        Date = c.String(),
                        Serial = c.Int(nullable: false),
                        Uid = c.String(),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dates = c.String(),
                        FromHour = c.Int(nullable: false),
                        FromMinute = c.Int(nullable: false),
                        ToHour = c.Int(nullable: false),
                        ToMinute = c.Int(nullable: false),
                        PatientNumber = c.Int(nullable: false),
                        VisitingFee = c.Int(nullable: false),
                        RegistrationFee = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Waitings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Serial = c.Int(nullable: false),
                        Date = c.String(),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Waitings", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Waitings", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Schedules", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Locations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Patients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Doctors", "SpecialityId", "dbo.Specialities");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropForeignKey("dbo.Locations", "CityId", "dbo.Cities");
            DropIndex("dbo.Waitings", new[] { "DoctorId" });
            DropIndex("dbo.Waitings", new[] { "PatientId" });
            DropIndex("dbo.Schedules", new[] { "DoctorId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Patients", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Specialities", new[] { "Name" });
            DropIndex("dbo.Ratings", new[] { "DoctorId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Doctors", new[] { "UserId" });
            DropIndex("dbo.Doctors", new[] { "SpecialityId" });
            DropIndex("dbo.Doctors", new[] { "UIN" });
            DropIndex("dbo.Comments", new[] { "DoctorId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.States", new[] { "StateName" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropIndex("dbo.Cities", new[] { "CityName" });
            DropIndex("dbo.Locations", new[] { "UserId" });
            DropIndex("dbo.Locations", new[] { "CityId" });
            DropTable("dbo.Waitings");
            DropTable("dbo.Schedules");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Appointments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Patients");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Specialities");
            DropTable("dbo.Ratings");
            DropTable("dbo.Doctors");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Locations");
        }
    }
}
