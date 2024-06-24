using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Listify.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SOBRENOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DATANASCIMENTO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TELEFONE = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FOTOPERFIL = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DATACRIACAO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DATAAÇTERACAO = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ITEM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TITULO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CATEGORIA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TIPO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DATALANCAMENTO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DATACRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ITEM_USUARIO_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "USUARIO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ITEMFOTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FOTO = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEMFOTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ITEMFOTO_ITEM_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ITEM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITEM_UsuarioID",
                table: "ITEM",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ITEMFOTO_ItemId",
                table: "ITEMFOTO",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITEMFOTO");

            migrationBuilder.DropTable(
                name: "ITEM");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
