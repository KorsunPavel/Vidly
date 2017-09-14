namespace Vidly.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddUsersandRoles : DbMigration {
        public override void Up() {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6c523e58-a580-4af5-af5b-fc87c62c8006', N'pavel@mail.ua', 0, N'AJvR86PFlit32RvYIxiUUBEQ8ACqbepooe8pnprCTbCjVO4CgTL9ToWMi5u060oBTw==', N'54ea86bf-926d-434c-ad87-5047707a116f', NULL, 0, 0, NULL, 1, 0, N'pavel@mail.ua')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c4b661c7-d136-4164-8509-d75067f1d3bd', N'admin2@mail.ua', 0, N'ADdHrCkJCTBQv7qfHaY5PWo4UrFfD1UvpEzUJVm2uHSpgadPBUDbg2F+wJ5EI4DByA==', N'e626ae36-b584-498e-b8b2-bc2ad7a15f6f', NULL, 0, 0, NULL, 1, 0, N'admin2@mail.ua')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'55c7bc45-e430-4c00-a87f-90da856b90f1', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c4b661c7-d136-4164-8509-d75067f1d3bd', N'55c7bc45-e430-4c00-a87f-90da856b90f1')
            ");
        }

        public override void Down() {
        }
    }
}
