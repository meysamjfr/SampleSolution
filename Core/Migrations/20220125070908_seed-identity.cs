using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class seedidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4", "06af638a-3daa-463f-83a3-131d78d4a641", "Admin", "Admin" },
                    { "a5f97684-4793-47af-af01-b1eb97433f4d", "b327eb0c-0176-4eb3-a359-917aa0c8c02e", "User", "User" },
                    { "d1eed7e7-396f-45f0-9c1a-cfcfd576d609", "6ccb67c0-df54-40d3-a341-cea26f0bea38", "Operator", "Operator" },
                    { "41d4cbe4-195f-4296-b881-5a372aa7916a", "1805e3fe-5d80-4e48-b603-ed7e7582e15b", "Developer", "Developer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastLogin", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4a9bbe2a-00df-4e89-9116-bee610e6b41b", 0, "059b071d-e719-43db-853e-2786556edb19", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin@zillow.ir", true, "Admin", false, null, "", false, null, "Admin@zillow.ir", "Admin", "AQAAAAEAACcQAAAAEIIEVjo1Nva/SYvvtrv5Kfsn0MSs7ks4ytzxmCgbfVkmxU4ReRqPwaYSxst2kpV14Q==", null, false, "e711641f-dff4-46c1-9961-fb4962b8a23d", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4", "4a9bbe2a-00df-4e89-9116-bee610e6b41b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41d4cbe4-195f-4296-b881-5a372aa7916a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5f97684-4793-47af-af01-b1eb97433f4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1eed7e7-396f-45f0-9c1a-cfcfd576d609");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4", "4a9bbe2a-00df-4e89-9116-bee610e6b41b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a9bbe2a-00df-4e89-9116-bee610e6b41b");
        }
    }
}
