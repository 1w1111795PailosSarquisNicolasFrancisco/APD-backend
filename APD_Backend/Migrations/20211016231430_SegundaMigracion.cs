using Microsoft.EntityFrameworkCore.Migrations;

namespace APD_Backend.Migrations
{
    public partial class SegundaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idRol",
                table: "Usuarios",
                column: "idRol");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_idRol",
                table: "Usuarios",
                column: "idRol",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_idRol",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_idRol",
                table: "Usuarios");
        }
    }
}
