using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Informacoes.ESocial.Infra.Sql.Migrations
{
    /// <inheritdoc />
    public partial class CreatingFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trabalhadores",
                columns: table => new
                {
                    TrabalhadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexoId = table.Column<int>(type: "int", nullable: false),
                    RacaCorId = table.Column<int>(type: "int", nullable: false),
                    EstadoCivilId = table.Column<int>(type: "int", nullable: false),
                    GrauInstrucaoId = table.Column<int>(type: "int", nullable: false),
                    IsPrimeiroEmprego = table.Column<bool>(type: "bit", nullable: false),
                    CodigoNomeTravTrans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MunicipioNascimentoId = table.Column<int>(type: "int", nullable: false),
                    UfNascimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisNascimentoId = table.Column<int>(type: "int", nullable: false),
                    NacionalidadeId = table.Column<int>(type: "int", nullable: false),
                    NomeMae = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomePai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_NisPisPasep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_NumeroCtps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_NumeroSerieCtps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_UfCtps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_NumeroRg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_EmissaoRg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_DataExpedicaoRg = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentosPessoais_NumeroRegistroOc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_EmissaoOc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_DataExpedOc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentosPessoais_DataValidadeOc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentosPessoais_NumeroCnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_DataExpedicaoCnh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentosPessoais_UfCnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentosPessoais_DataValidadeCnh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentosPessoais_DataPrimeiraHabilitacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentosPessoais_CategoriaCnhId = table.Column<int>(type: "int", nullable: true),
                    EnderecoResidencial_Id = table.Column<int>(type: "int", nullable: true),
                    EnderecoResidencial_TipoLogradouro = table.Column<int>(type: "int", nullable: true),
                    EnderecoResidencial_Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoResidencial_Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoResidencial_Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoResidencial_Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoResidencial_CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoResidencial_MunicipioId = table.Column<int>(type: "int", nullable: true),
                    EnderecoResidencial_Uf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DadosDeficiencia_TemDeficienciaFisica = table.Column<bool>(type: "bit", nullable: true),
                    DadosDeficiencia_TemDeficienciaVisual = table.Column<bool>(type: "bit", nullable: true),
                    DadosDeficiencia_TemDeficienciaAuditiva = table.Column<bool>(type: "bit", nullable: true),
                    DadosDeficiencia_TemDeficienciaMental = table.Column<bool>(type: "bit", nullable: true),
                    DadosDeficiencia_TemDeficienciaIntelectual = table.Column<bool>(type: "bit", nullable: true),
                    RecebeBeneficioPrevidencia = table.Column<bool>(type: "bit", nullable: false),
                    Contato_Telefone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contato_Telefone2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contato_Email1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contato_Email2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contato_sDescricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerificado = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabalhadores", x => x.TrabalhadorId);
                });

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    ArquivoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabalhadorId = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NomeArquivo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.ArquivoId);
                    table.ForeignKey(
                        name: "FK_Arquivos_Trabalhadores_TrabalhadorId",
                        column: x => x.TrabalhadorId,
                        principalTable: "Trabalhadores",
                        principalColumn: "TrabalhadorId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Cedidos",
                columns: table => new
                {
                    CedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabalhadorId = table.Column<int>(type: "int", nullable: false),
                    CnpjEmpregadoCedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaTrabalhador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoRegTrab = table.Column<int>(type: "int", nullable: false),
                    TipoRegPrev = table.Column<int>(type: "int", nullable: false),
                    OnusCessReqId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cedidos", x => x.CedidoId);
                    table.ForeignKey(
                        name: "FK_Cedidos_Trabalhadores_TrabalhadorId",
                        column: x => x.TrabalhadorId,
                        principalTable: "Trabalhadores",
                        principalColumn: "TrabalhadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependentes",
                columns: table => new
                {
                    DependenteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabalhadorId = table.Column<int>(type: "int", nullable: false),
                    TipoDependente = table.Column<int>(type: "int", nullable: false),
                    NomeDependente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CpfDependente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EhDepTrabalhadorIrrf = table.Column<bool>(type: "bit", nullable: false),
                    EhDepIncapaFisMentTrab = table.Column<bool>(type: "bit", nullable: false),
                    EhDependentePensao = table.Column<bool>(type: "bit", nullable: false),
                    Responsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefoneResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependentes", x => x.DependenteId);
                    table.ForeignKey(
                        name: "FK_Dependentes_Trabalhadores_TrabalhadorId",
                        column: x => x.TrabalhadorId,
                        principalTable: "Trabalhadores",
                        principalColumn: "TrabalhadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estagiarios",
                columns: table => new
                {
                    EstagiarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabalhadorId = table.Column<int>(type: "int", nullable: false),
                    NaturezaEstagio = table.Column<int>(type: "int", nullable: false),
                    AreaAtuacaoId = table.Column<int>(type: "int", nullable: true),
                    RazaoSocialInstEnsino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CnpjInstEnsino = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeSupervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstEnsino_Id = table.Column<int>(type: "int", nullable: true),
                    EnderecoInstEnsino_TipoLogradouro = table.Column<int>(type: "int", nullable: true),
                    EnderecoInstEnsino_Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstEnsino_Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstEnsino_Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstEnsino_Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstEnsino_CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoInstEnsino_MunicipioId = table.Column<int>(type: "int", nullable: true),
                    EnderecoInstEnsino_Uf = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estagiarios", x => x.EstagiarioId);
                    table.ForeignKey(
                        name: "FK_Estagiarios_Trabalhadores_TrabalhadorId",
                        column: x => x.TrabalhadorId,
                        principalTable: "Trabalhadores",
                        principalColumn: "TrabalhadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_TrabalhadorId",
                table: "Arquivos",
                column: "TrabalhadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cedidos_TrabalhadorId",
                table: "Cedidos",
                column: "TrabalhadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependentes_TrabalhadorId",
                table: "Dependentes",
                column: "TrabalhadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Estagiarios_TrabalhadorId",
                table: "Estagiarios",
                column: "TrabalhadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "Cedidos");

            migrationBuilder.DropTable(
                name: "Dependentes");

            migrationBuilder.DropTable(
                name: "Estagiarios");

            migrationBuilder.DropTable(
                name: "Trabalhadores");
        }
    }
}
