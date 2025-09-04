using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingTrabalhadorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerificado",
                table: "Trabalhadores");

            migrationBuilder.AddColumn<int>(
                name: "StatusCadastro",
                table: "Trabalhadores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCadastro",
                table: "Trabalhadores");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerificado",
                table: "Trabalhadores",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
