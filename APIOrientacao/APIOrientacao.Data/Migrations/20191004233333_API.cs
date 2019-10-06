using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIOrientacao.Data.Migrations
{
    public partial class API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Curso",
                schema: "dbo",
                columns: table => new
                {
                    IdCurso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdCurso", x => x.IdCurso);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoa", x => x.IdPessoa);
                });

            migrationBuilder.CreateTable(
                name: "Situacao",
                schema: "dbo",
                columns: table => new
                {
                    IdSituacao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdSituacao", x => x.IdSituacao);
                });

            migrationBuilder.CreateTable(
                name: "TipoOrientacao",
                schema: "dbo",
                columns: table => new
                {
                    IdTipoOrientacao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdTipoOrientacao", x => x.IdTipoOrientacao);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    Matricula = table.Column<string>(maxLength: 8, nullable: false),
                    RegistroAtivo = table.Column<bool>(nullable: false),
                    IdCurso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoaAluno", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "FK_CursoAluno",
                        column: x => x.IdCurso,
                        principalSchema: "dbo",
                        principalTable: "Curso",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "PFK_PessoaAluno",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Pessoa",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    RegistroAtivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoaProfessor", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "PFK_PessoaProfessor",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Pessoa",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                schema: "dbo",
                columns: table => new
                {
                    IdProjeto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdPessoa = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    Encerrado = table.Column<bool>(nullable: false),
                    Nota = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdProjeto", x => x.IdProjeto);
                    table.ForeignKey(
                        name: "FK_AlunoProjeto",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Aluno",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orientacao",
                schema: "dbo",
                columns: table => new
                {
                    IdProjeto = table.Column<int>(nullable: false),
                    IdPessoa = table.Column<int>(nullable: false),
                    IdTipoOrientacao = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FK_Orientacao", x => new { x.IdProjeto, x.IdPessoa });
                    table.ForeignKey(
                        name: "FK_PessoaOrientacao",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Professor",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjetoOrientacao",
                        column: x => x.IdProjeto,
                        principalSchema: "dbo",
                        principalTable: "Projeto",
                        principalColumn: "IdProjeto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoOrientacaoOrientacao",
                        column: x => x.IdTipoOrientacao,
                        principalSchema: "dbo",
                        principalTable: "TipoOrientacao",
                        principalColumn: "IdTipoOrientacao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SituacaoProjeto",
                schema: "dbo",
                columns: table => new
                {
                    IdSituacao = table.Column<int>(nullable: false),
                    IdProjeto = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FK_SituacaoProjeto", x => new { x.IdProjeto, x.IdSituacao });
                    table.ForeignKey(
                        name: "FK_ProjetoSituacaoProjeto",
                        column: x => x.IdProjeto,
                        principalSchema: "dbo",
                        principalTable: "Projeto",
                        principalColumn: "IdProjeto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SituacaoSituacaoProjeto",
                        column: x => x.IdSituacao,
                        principalSchema: "dbo",
                        principalTable: "Situacao",
                        principalColumn: "IdSituacao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_IdCurso",
                schema: "dbo",
                table: "Aluno",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Orientacao_IdPessoa",
                schema: "dbo",
                table: "Orientacao",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_Orientacao_IdTipoOrientacao",
                schema: "dbo",
                table: "Orientacao",
                column: "IdTipoOrientacao");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_IdPessoa",
                schema: "dbo",
                table: "Projeto",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_SituacaoProjeto_IdSituacao",
                schema: "dbo",
                table: "SituacaoProjeto",
                column: "IdSituacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orientacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SituacaoProjeto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Professor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TipoOrientacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Projeto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Situacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Aluno",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Curso",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Pessoa",
                schema: "dbo");
        }
    }
}
