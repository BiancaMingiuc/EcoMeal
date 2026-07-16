using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoMeal1.Migrations
{
    /// <inheritdoc />
    public partial class LinkBusinessToOwner3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Businesses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_AspNetUsers_OwnerId",
                table: "Businesses",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_AspNetUsers_OwnerId",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_OwnerId",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Businesses");
        }
    }
}
