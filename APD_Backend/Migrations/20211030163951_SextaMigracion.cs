using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace APD_Backend.Migrations
{
    public partial class SextaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    cantidadPedidos = table.Column<string>(type: "text", nullable: true),
                    clasificaciones = table.Column<int>(type: "integer", nullable: false),
                    telefono = table.Column<string>(type: "text", nullable: true),
                    mail = table.Column<string>(type: "text", nullable: true),
                    direccion = table.Column<string>(type: "text", nullable: true),
                    idZona = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Clientes_Roles_idZona",
                        column: x => x.idZona,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormasDePago",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    formaDePago = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasDePago", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estado = table.Column<string>(type: "text", nullable: true),
                    idCliente = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Roles_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    contacto = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    zona = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCliente = table.Column<int>(type: "integer", nullable: false),
                    numero = table.Column<int>(type: "integer", nullable: false),
                    fechaVencimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tarjetas_Clientes_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCliente = table.Column<int>(type: "integer", nullable: false),
                    idPedido = table.Column<int>(type: "integer", nullable: false),
                    idFormaDePago = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Facturas_Clientes_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_FormasDePago_idFormaDePago",
                        column: x => x.idFormaDePago,
                        principalTable: "FormasDePago",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facturas_Pedidos_idPedido",
                        column: x => x.idPedido,
                        principalTable: "Pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    precio = table.Column<float>(type: "real", nullable: false),
                    idProveedor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Articulos_Proveedores_idProveedor",
                        column: x => x.idProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticuloXPedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idArticulo = table.Column<int>(type: "integer", nullable: false),
                    idPedido = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloXPedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_ArticuloXPedido_Articulos_idArticulo",
                        column: x => x.idArticulo,
                        principalTable: "Articulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticuloXPedido_Pedidos_idPedido",
                        column: x => x.idPedido,
                        principalTable: "Pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_idProveedor",
                table: "Articulos",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloXPedido_idArticulo",
                table: "ArticuloXPedido",
                column: "idArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_ArticuloXPedido_idPedido",
                table: "ArticuloXPedido",
                column: "idPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_idZona",
                table: "Clientes",
                column: "idZona");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_idCliente",
                table: "Facturas",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_idFormaDePago",
                table: "Facturas",
                column: "idFormaDePago");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_idPedido",
                table: "Facturas",
                column: "idPedido");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_idCliente",
                table: "Pedidos",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_idCliente",
                table: "Tarjetas",
                column: "idCliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticuloXPedido");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Tarjetas");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "FormasDePago");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
