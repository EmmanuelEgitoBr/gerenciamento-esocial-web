using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class ModifyingForeginKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Estagiarios_TrabalhadorId",
                table: "Estagiarios");

            migrationBuilder.DropIndex(
                name: "IX_Cedidos_TrabalhadorId",
                table: "Cedidos");

            migrationBuilder.CreateIndex(
                name: "IX_Estagiarios_TrabalhadorId",
                table: "Estagiarios",
                column: "TrabalhadorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cedidos_TrabalhadorId",
                table: "Cedidos",
                column: "TrabalhadorId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Estagiarios_TrabalhadorId",
                table: "Estagiarios");

            migrationBuilder.DropIndex(
                name: "IX_Cedidos_TrabalhadorId",
                table: "Cedidos");

            migrationBuilder.CreateIndex(
                name: "IX_Estagiarios_TrabalhadorId",
                table: "Estagiarios",
                column: "TrabalhadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cedidos_TrabalhadorId",
                table: "Cedidos",
                column: "TrabalhadorId");
        }
    }
}
