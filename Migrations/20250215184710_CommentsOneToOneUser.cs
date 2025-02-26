using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaPlatformAPI.Migrations
{
    /// <inheritdoc />
    public partial class CommentsOneToOneUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44c40e8c-e617-43dc-9939-b2ca3087ac65");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa60a286-18c7-494f-9e0c-84c33b08b728");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32eb6236-45d0-4a6d-8afe-a83baf395c86", "aa37ae56-7c7c-42a8-bbf8-bc7e68a8df6d", "User", "USER" },
                    { "981285ab-f5d3-4bff-89ed-c020b4fdaef8", "72181347-d483-48eb-b30c-07c930150ba9", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AppUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32eb6236-45d0-4a6d-8afe-a83baf395c86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "981285ab-f5d3-4bff-89ed-c020b4fdaef8");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44c40e8c-e617-43dc-9939-b2ca3087ac65", "db8cfdbc-2d07-486c-bcc8-a1e66e27aefe", "User", "USER" },
                    { "fa60a286-18c7-494f-9e0c-84c33b08b728", "464a2fe6-d626-42b7-9ce1-41bb71c1d525", "Admin", "ADMIN" }
                });
        }
    }
}
