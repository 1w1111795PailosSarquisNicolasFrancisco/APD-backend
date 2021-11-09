using Microsoft.EntityFrameworkCore.Migrations;

namespace APD_Backend.Migrations
{
    public partial class SeptimaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Roles_idZona",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Roles_idCliente",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "cantidad",
                table: "ArticuloXPedido",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Zonas_idZona",
                table: "Clientes",
                column: "idZona",
                principalTable: "Zonas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_idCliente",
                table: "Pedidos",
                column: "idCliente",
                principalTable: "Clientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Zonas_idZona",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_idCliente",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "cantidad",
                table: "ArticuloXPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Roles_idZona",
                table: "Clientes",
                column: "idZona",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Roles_idCliente",
                table: "Pedidos",
                column: "idCliente",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
