using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addrelationbetweenUserAndHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Histories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_UserId",
                table: "Histories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_AspNetUsers_UserId",
                table: "Histories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_AspNetUsers_UserId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_UserId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Histories");
        }
    }
}
