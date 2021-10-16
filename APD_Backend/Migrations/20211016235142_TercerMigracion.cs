using Microsoft.EntityFrameworkCore.Migrations;

namespace APD_Backend.Migrations
{
    public partial class TercerMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tokenSesion",
                table: "Usuarios",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tokenSesion",
                table: "Usuarios");
        }
    }
}
