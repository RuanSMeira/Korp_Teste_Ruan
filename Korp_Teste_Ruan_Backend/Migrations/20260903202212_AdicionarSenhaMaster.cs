using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Korp_Teste_Ruan_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarSenhaMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenhaMaster",
                table: "Empresas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "Korp@123");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenhaMaster",
                table: "Empresas");
        }
    }
}
