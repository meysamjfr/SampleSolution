using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class advertswithimages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advert_Cities_CityId",
                table: "Advert");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertImage_Advert_AdvertId",
                table: "AdvertImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertImage",
                table: "AdvertImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advert",
                table: "Advert");

            migrationBuilder.RenameTable(
                name: "AdvertImage",
                newName: "AdvertImages");

            migrationBuilder.RenameTable(
                name: "Advert",
                newName: "Adverts");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertImage_AdvertId",
                table: "AdvertImages",
                newName: "IX_AdvertImages_AdvertId");

            migrationBuilder.RenameIndex(
                name: "IX_Advert_CityId",
                table: "Adverts",
                newName: "IX_Adverts_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertImages",
                table: "AdvertImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adverts",
                table: "Adverts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e5cf5ba-9aa6-48a3-b7ce-a4ec8a478ce4",
                column: "ConcurrencyStamp",
                value: "ecf978b4-9d5d-47b1-94eb-225d87f9ed2e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41d4cbe4-195f-4296-b881-5a372aa7916a",
                column: "ConcurrencyStamp",
                value: "2ef4b22e-5dbd-4636-a789-3c054b1742bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5f97684-4793-47af-af01-b1eb97433f4d",
                column: "ConcurrencyStamp",
                value: "9e1cf6d4-1a62-491c-b8ed-190632b762a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1eed7e7-396f-45f0-9c1a-cfcfd576d609",
                column: "ConcurrencyStamp",
                value: "5b1c7513-f1e5-41da-842d-61eda8417c79");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a9bbe2a-00df-4e89-9116-bee610e6b41b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a66289aa-b49d-402a-b224-885702748aa6", "AQAAAAEAACcQAAAAENYv8QSjav8XjH8XjYnE9b0/5pFUQDcP+HZEwYdk7g7vOqfFrHPuA+RKD/8SUWwvQA==", "7a501d67-f0a1-4f34-b36d-726e95282f7e" });

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(3970), new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(3978) });

            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(780), new DateTime(2022, 1, 25, 7, 11, 24, 849, DateTimeKind.Utc).AddTicks(836) });

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertImages_Adverts_AdvertId",
                table: "AdvertImages",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Cities_CityId",
                table: "Adverts",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertImages_Adverts_AdvertId",
                table: "AdvertImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Cities_CityId",
                table: "Adverts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adverts",
                table: "Adverts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertImages",
                table: "AdvertImages");

            migrationBuilder.RenameTable(
                name: "Adverts",
                newName: "Advert");

            migrationBuilder.RenameTable(
                name: "AdvertImages",
                newName: "AdvertImage");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_CityId",
                table: "Advert",
                newName: "IX_Advert_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_AdvertImages_AdvertId",
                table: "AdvertImage",
                newName: "IX_AdvertImage_AdvertId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advert",
                table: "Advert",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertImage",
                table: "AdvertImage",
                column: "Id");

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

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 25, 7, 10, 56, 899, DateTimeKind.Utc).AddTicks(1853), new DateTime(2022, 1, 25, 7, 10, 56, 899, DateTimeKind.Utc).AddTicks(1875) });

            migrationBuilder.UpdateData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 1, 25, 7, 10, 56, 897, DateTimeKind.Utc).AddTicks(9440), new DateTime(2022, 1, 25, 7, 10, 56, 897, DateTimeKind.Utc).AddTicks(9456) });

            migrationBuilder.AddForeignKey(
                name: "FK_Advert_Cities_CityId",
                table: "Advert",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertImage_Advert_AdvertId",
                table: "AdvertImage",
                column: "AdvertId",
                principalTable: "Advert",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
