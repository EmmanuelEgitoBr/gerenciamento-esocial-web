using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingEntitiesFromDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaAtuacaoId",
                table: "Estagiarios");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Cedidos");

            migrationBuilder.RenameColumn(
                name: "SexoId",
                table: "Trabalhadores",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "RacaCorId",
                table: "Trabalhadores",
                newName: "RacaCor");

            migrationBuilder.RenameColumn(
                name: "GrauInstrucaoId",
                table: "Trabalhadores",
                newName: "GrauInstrucao");

            migrationBuilder.RenameColumn(
                name: "EstadoCivilId",
                table: "Trabalhadores",
                newName: "EstadoCivil");

            migrationBuilder.RenameColumn(
                name: "DocumentosPessoais_CategoriaCnhId",
                table: "Trabalhadores",
                newName: "DocumentosPessoais_CategoriaCnh");

            migrationBuilder.RenameColumn(
                name: "OnusCessReqId",
                table: "Cedidos",
                newName: "OnusCessReq");

            migrationBuilder.AddColumn<int>(
                name: "AreaAtuacao",
                table: "Estagiarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Cedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Dados",
                table: "Arquivos",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUpload",
                table: "Arquivos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Tamanho",
                table: "Arquivos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaAtuacao",
                table: "Estagiarios");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Cedidos");

            migrationBuilder.DropColumn(
                name: "Dados",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "DataUpload",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "Arquivos");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "Trabalhadores",
                newName: "SexoId");

            migrationBuilder.RenameColumn(
                name: "RacaCor",
                table: "Trabalhadores",
                newName: "RacaCorId");

            migrationBuilder.RenameColumn(
                name: "GrauInstrucao",
                table: "Trabalhadores",
                newName: "GrauInstrucaoId");

            migrationBuilder.RenameColumn(
                name: "EstadoCivil",
                table: "Trabalhadores",
                newName: "EstadoCivilId");

            migrationBuilder.RenameColumn(
                name: "DocumentosPessoais_CategoriaCnh",
                table: "Trabalhadores",
                newName: "DocumentosPessoais_CategoriaCnhId");

            migrationBuilder.RenameColumn(
                name: "OnusCessReq",
                table: "Cedidos",
                newName: "OnusCessReqId");

            migrationBuilder.AddColumn<int>(
                name: "AreaAtuacaoId",
                table: "Estagiarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Cedidos",
                type: "int",
                nullable: true);
        }
    }
}
