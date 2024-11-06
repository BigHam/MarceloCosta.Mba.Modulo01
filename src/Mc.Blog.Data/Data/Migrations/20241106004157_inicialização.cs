using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc.Blog.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class inicialização : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "papeis",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    name = table.Column<string>(type: "varchar(150)", nullable: true),
                    normalized_name = table.Column<string>(type: "varchar(150)", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_papeis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nome_completo = table.Column<string>(type: "varchar(150)", nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    user_name = table.Column<string>(type: "varchar(60)", nullable: true),
                    normalized_user_name = table.Column<string>(type: "varchar(60)", nullable: true),
                    email = table.Column<string>(type: "varchar(150)", nullable: true),
                    normalized_email = table.Column<string>(type: "varchar(150)", nullable: true),
                    email_confirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    password_hash = table.Column<string>(type: "varchar(max)", nullable: true),
                    security_stamp = table.Column<string>(type: "varchar(max)", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "varchar(max)", nullable: true),
                    phone_number = table.Column<string>(type: "varchar(15)", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    two_factor_enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    lockout_enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(150)", nullable: false),
                    conteudo = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    alterado_em = table.Column<DateTime>(type: "datetime2", nullable: true),
                    excluido = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    excluido_em = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_posts_x_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    conteudo = table.Column<string>(type: "varchar(1000)", nullable: false),
                    id_post = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    alterado_em = table.Column<DateTime>(type: "datetime2", nullable: true),
                    excluido = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    excluido_em = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_comentarios_x_post",
                        column: x => x.id_post,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_comentarios_x_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_fk_comentarios_x_post",
                table: "comentarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "idx_fk_comentarios_x_usuario",
                table: "comentarios",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_id_post",
                table: "comentarios",
                column: "id_post");

            migrationBuilder.CreateIndex(
                name: "idx_fk_posts_x_usuario",
                table: "posts",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "papeis");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
