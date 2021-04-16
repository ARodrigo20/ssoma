using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hsec.Infrastructure.Migrations
{
    public partial class movil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TComentario",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroReferencia = table.Column<string>(maxLength: 20, nullable: true),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    FecCreacion = table.Column<DateTime>(nullable: true),
                    UsuCreacion = table.Column<string>(maxLength: 20, nullable: true),
                    FecModifica = table.Column<DateTime>(nullable: true),
                    UsuModifica = table.Column<string>(maxLength: 20, nullable: true),
                    Estado = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TComentario", x => x.Correlativo);
                });

            migrationBuilder.CreateTable(
                name: "TCursoAsistencia",
                columns: table => new
                {
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CodCurso = table.Column<string>(maxLength: 20, nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FecCreacion = table.Column<DateTime>(nullable: true),
                    UsuCreacion = table.Column<string>(maxLength: 20, nullable: true),
                    FecModifica = table.Column<DateTime>(nullable: true),
                    UsuModifica = table.Column<string>(maxLength: 20, nullable: true),
                    Estado = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("XPKTCursoAsistencia", x => new { x.CodPersona, x.CodCurso, x.Fecha });
                });

            migrationBuilder.CreateTable(
                name: "TFeedback",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodUsuario = table.Column<string>(maxLength: 20, nullable: true),
                    Fecha = table.Column<DateTime>(nullable: true),
                    Asunto = table.Column<string>(maxLength: 100, nullable: true),
                    Mensaje = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFeedback", x => x.Correlativo);
                });

            migrationBuilder.CreateTable(
                name: "TNoticias",
                columns: table => new
                {
                    CodNoticia = table.Column<string>(maxLength: 20, nullable: false),
                    Titulo = table.Column<string>(maxLength: 200, nullable: true),
                    Autor = table.Column<string>(maxLength: 20, nullable: true),
                    Tipo = table.Column<string>(maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    DescripcionCorta = table.Column<string>(maxLength: 200, nullable: true),
                    Fecha = table.Column<DateTime>(nullable: true),
                    FecCreacion = table.Column<DateTime>(nullable: true),
                    UsuCreacion = table.Column<string>(maxLength: 20, nullable: true),
                    FecModifica = table.Column<DateTime>(nullable: true),
                    UsuModifica = table.Column<string>(maxLength: 20, nullable: true),
                    Estado = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TNoticias", x => x.CodNoticia);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionFacilito",
                columns: table => new
                {
                    CodObsFacilito = table.Column<string>(maxLength: 20, nullable: false),
                    Tipo = table.Column<string>(maxLength: 1, nullable: true),
                    CodPosicionGer = table.Column<string>(maxLength: 20, nullable: true),
                    CodPosicionSup = table.Column<string>(maxLength: 20, nullable: true),
                    UbicacionExacta = table.Column<string>(maxLength: 50, nullable: true),
                    Observacion = table.Column<string>(maxLength: 500, nullable: true),
                    Accion = table.Column<string>(maxLength: 500, nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    RespAuxiliar = table.Column<string>(maxLength: 50, nullable: true),
                    FecCreacion = table.Column<DateTime>(nullable: true),
                    UsuCreacion = table.Column<string>(maxLength: 20, nullable: true),
                    FecModifica = table.Column<DateTime>(nullable: true),
                    UsuModifica = table.Column<string>(maxLength: 20, nullable: true),
                    Estado = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservacionFacilito", x => x.CodObsFacilito);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionFHistorial",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodObsFacilito = table.Column<string>(maxLength: 20, nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    Comentario = table.Column<string>(maxLength: 500, nullable: true),
                    FecCreacion = table.Column<DateTime>(nullable: true),
                    UsuCreacion = table.Column<string>(maxLength: 20, nullable: true),
                    FecModifica = table.Column<DateTime>(nullable: true),
                    UsuModifica = table.Column<string>(maxLength: 20, nullable: true),
                    Estado = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservacionFHistorial", x => x.Correlativo);
                });

            migrationBuilder.CreateTable(
                name: "TResponsables",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CodPosGerencia = table.Column<int>(nullable: true),
                    CodPosSuperInt = table.Column<int>(nullable: false),
                    CodTipo = table.Column<string>(maxLength: 2, nullable: false),
                    FecCreacion = table.Column<DateTime>(nullable: true),
                    UsuCreacion = table.Column<string>(maxLength: 20, nullable: true),
                    FecModifica = table.Column<DateTime>(nullable: true),
                    UsuModifica = table.Column<string>(maxLength: 20, nullable: true),
                    Estado = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TResponsables", x => x.Correlativo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TComentario");

            migrationBuilder.DropTable(
                name: "TCursoAsistencia");

            migrationBuilder.DropTable(
                name: "TFeedback");

            migrationBuilder.DropTable(
                name: "TNoticias");

            migrationBuilder.DropTable(
                name: "TObservacionFacilito");

            migrationBuilder.DropTable(
                name: "TObservacionFHistorial");

            migrationBuilder.DropTable(
                name: "TResponsables");
        }
    }
}
