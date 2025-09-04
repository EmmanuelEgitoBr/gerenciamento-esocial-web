using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaLogCadastro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogStatusCadastros",
                columns: table => new
                {
                    LogStatusCadastroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabalhadorId = table.Column<int>(type: "int", nullable: false),
                    EmailTrabalhador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailRecebido = table.Column<bool>(type: "bit", nullable: false),
                    StatusCadastro = table.Column<int>(type: "int", nullable: false),
                    Pendencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEventoLog = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogStatusCadastros", x => x.LogStatusCadastroId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogStatusCadastros");
        }
    }
}
