using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class adddistanceToHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "latitude",
                table: "Histories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Histories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "distance",
                table: "Histories",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "distance",
                table: "Histories");

            migrationBuilder.AlterColumn<int>(
                name: "latitude",
                table: "Histories",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Longitude",
                table: "Histories",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
