using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaLogEnvioEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEmailRecebido",
                table: "LogStatusCadastros",
                newName: "IsEmailEnviado");

            migrationBuilder.CreateTable(
                name: "LogEnvioEmails",
                columns: table => new
                {
                    LogEnvioEmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabalhadorId = table.Column<int>(type: "int", nullable: false),
                    EmailTrabalhador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoLog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEventoLog = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEnvioEmails", x => x.LogEnvioEmailId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEnvioEmails");

            migrationBuilder.RenameColumn(
                name: "IsEmailEnviado",
                table: "LogStatusCadastros",
                newName: "IsEmailRecebido");
        }
    }
}
