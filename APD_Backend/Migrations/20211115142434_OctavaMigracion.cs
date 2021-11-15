﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APD_Backend.Migrations
{
    public partial class OctavaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "fecha",
                table: "Pedidos",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha",
                table: "Pedidos");
        }
    }
}
