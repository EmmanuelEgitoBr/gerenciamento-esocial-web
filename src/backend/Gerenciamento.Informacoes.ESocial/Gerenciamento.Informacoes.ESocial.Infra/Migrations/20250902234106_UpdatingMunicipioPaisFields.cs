using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingMunicipioPaisFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnderecoResidencial_MunicipioId",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "MunicipioNascimentoId",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "NacionalidadeId",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "PaisNascimentoId",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "EnderecoInstEnsino_MunicipioId",
                table: "Estagiarios");

            migrationBuilder.AddColumn<string>(
                name: "EnderecoResidencial_Municipio",
                table: "Trabalhadores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MunicipioNascimento",
                table: "Trabalhadores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nacionalidade",
                table: "Trabalhadores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaisNascimento",
                table: "Trabalhadores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoInstEnsino_Municipio",
                table: "Estagiarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnderecoResidencial_Municipio",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "MunicipioNascimento",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "Nacionalidade",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "PaisNascimento",
                table: "Trabalhadores");

            migrationBuilder.DropColumn(
                name: "EnderecoInstEnsino_Municipio",
                table: "Estagiarios");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoResidencial_MunicipioId",
                table: "Trabalhadores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MunicipioNascimentoId",
                table: "Trabalhadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NacionalidadeId",
                table: "Trabalhadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaisNascimentoId",
                table: "Trabalhadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoInstEnsino_MunicipioId",
                table: "Estagiarios",
                type: "int",
                nullable: true);
        }
    }
}
