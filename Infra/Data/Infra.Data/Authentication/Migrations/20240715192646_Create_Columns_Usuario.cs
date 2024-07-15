using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class Create_Columns_Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usu_Nome",
                schema: "Authentication",
                table: "Usuario");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Usu_DataDeCadastro",
                schema: "Authentication",
                table: "Usuario",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "Usu_Status",
                schema: "Authentication",
                table: "Usuario",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Usu_UltimoLogin",
                schema: "Authentication",
                table: "Usuario",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usu_DataDeCadastro",
                schema: "Authentication",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Usu_Status",
                schema: "Authentication",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Usu_UltimoLogin",
                schema: "Authentication",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "Usu_Nome",
                schema: "Authentication",
                table: "Usuario",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
