using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Project.Migrations
{
    public partial class initprovincescities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<Point>(type: "geography", nullable: true),
                    Hits = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    VoiceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TridimensionalUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellingType = table.Column<int>(type: "int", nullable: false),
                    EstateType = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    LocationType = table.Column<int>(type: "int", nullable: false),
                    SellerType = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: true),
                    BuildYear = table.Column<int>(type: "int", nullable: true),
                    ParkingSpaces = table.Column<int>(type: "int", nullable: true),
                    Rooms = table.Column<int>(type: "int", nullable: true),
                    Elevator = table.Column<int>(type: "int", nullable: true),
                    Masters = table.Column<int>(type: "int", nullable: true),
                    StoreRoomArea = table.Column<int>(type: "int", nullable: true),
                    Laundry = table.Column<bool>(type: "bit", nullable: true),
                    SwimmingPool = table.Column<bool>(type: "bit", nullable: true),
                    Gym = table.Column<bool>(type: "bit", nullable: true),
                    Lobby = table.Column<bool>(type: "bit", nullable: true),
                    UsageType = table.Column<int>(type: "int", nullable: true),
                    BuildStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BuildCompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceRent = table.Column<double>(type: "float", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advert_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvertId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertImage_Advert_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Advert",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4",
                column: "ConcurrencyStamp",
                value: "8e3fe07c-7b25-45b2-84ca-465e32a4735c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41d4cbe4-195f-4296-b881-5a372aa7916a",
                column: "ConcurrencyStamp",
                value: "d7891eca-57ea-49bd-a505-de15143c73de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5f97684-4793-47af-af01-b1eb97433f4d",
                column: "ConcurrencyStamp",
                value: "8b1a4bd2-4ef5-413d-b1b0-c4a257a17c9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1eed7e7-396f-45f0-9c1a-cfcfd576d609",
                column: "ConcurrencyStamp",
                value: "53736b47-3735-43a5-b6bc-a13a901089ce");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a9bbe2a-00df-4e89-9116-bee610e6b41b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10d5a6ec-a700-40b3-93f3-191f9dc41d1a", "AQAAAAEAACcQAAAAEHp4XJ52yewSUgqgwF/70Tgx6nl+dlfITd0sU+/wphAF9vydidFskfPIjMUN4zXFnw==", "dde17794-53c3-47f8-9f4e-53d435bcc2f1" });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2022, 1, 25, 7, 10, 56, 897, DateTimeKind.Utc).AddTicks(9440), null, true, "مازندران", new DateTime(2022, 1, 25, 7, 10, 56, 897, DateTimeKind.Utc).AddTicks(9456), null });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Name", "ProvinceId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, new DateTime(2022, 1, 25, 7, 10, 56, 899, DateTimeKind.Utc).AddTicks(1853), null, true, "ساری", 1, new DateTime(2022, 1, 25, 7, 10, 56, 899, DateTimeKind.Utc).AddTicks(1875), null });

            migrationBuilder.CreateIndex(
                name: "IX_Advert_CityId",
                table: "Advert",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertImage_AdvertId",
                table: "AdvertImage",
                column: "AdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertImage");

            migrationBuilder.DropTable(
                name: "Advert");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4",
                column: "ConcurrencyStamp",
                value: "06af638a-3daa-463f-83a3-131d78d4a641");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41d4cbe4-195f-4296-b881-5a372aa7916a",
                column: "ConcurrencyStamp",
                value: "1805e3fe-5d80-4e48-b603-ed7e7582e15b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5f97684-4793-47af-af01-b1eb97433f4d",
                column: "ConcurrencyStamp",
                value: "b327eb0c-0176-4eb3-a359-917aa0c8c02e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1eed7e7-396f-45f0-9c1a-cfcfd576d609",
                column: "ConcurrencyStamp",
                value: "6ccb67c0-df54-40d3-a341-cea26f0bea38");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a9bbe2a-00df-4e89-9116-bee610e6b41b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "059b071d-e719-43db-853e-2786556edb19", "AQAAAAEAACcQAAAAEIIEVjo1Nva/SYvvtrv5Kfsn0MSs7ks4ytzxmCgbfVkmxU4ReRqPwaYSxst2kpV14Q==", "e711641f-dff4-46c1-9961-fb4962b8a23d" });
        }
    }
}
