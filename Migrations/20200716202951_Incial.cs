using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDiarioNegociacoes.Migrations
{
    public partial class Incial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estrategias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 400, nullable: false),
                    IdUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estrategias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estrategias_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Negociacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    Ativo = table.Column<string>(nullable: false),
                    IdEstrategia = table.Column<int>(nullable: false),
                    Operacao = table.Column<string>(nullable: false),
                    Lote = table.Column<double>(nullable: false),
                    PrecoEntrada = table.Column<double>(nullable: false),
                    PrecoSaida = table.Column<double>(nullable: false),
                    Encerramento = table.Column<string>(nullable: false),
                    IdUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Negociacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Negociacoes_Estrategias_IdEstrategia",
                        column: x => x.IdEstrategia,
                        principalTable: "Estrategias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Negociacoes_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estrategias_IdUser",
                table: "Estrategias",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Negociacoes_IdEstrategia",
                table: "Negociacoes",
                column: "IdEstrategia");

            migrationBuilder.CreateIndex(
                name: "IX_Negociacoes_IdUser",
                table: "Negociacoes",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Negociacoes");

            migrationBuilder.DropTable(
                name: "Estrategias");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
