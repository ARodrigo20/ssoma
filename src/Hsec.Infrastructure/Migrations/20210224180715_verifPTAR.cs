using Microsoft.EntityFrameworkCore.Migrations;

namespace Hsec.Infrastructure.Migrations
{
    public partial class verifPTAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "P1_IdentificoRelacionados",
                table: "TVerificacionPTAR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "P2_ControlesImplementados",
                table: "TVerificacionPTAR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "P3_ReviseElContenido",
                table: "TVerificacionPTAR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "P4_CorrespondeAlEjecutado",
                table: "TVerificacionPTAR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "P5_NoSeEjecutaElTrabajo",
                table: "TVerificacionPTAR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "P6_RevisadoyFirmado",
                table: "TVerificacionPTAR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CodAnterior",
                table: "TAnalisisCausa",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "P1_IdentificoRelacionados",
                table: "TVerificacionPTAR");

            migrationBuilder.DropColumn(
                name: "P2_ControlesImplementados",
                table: "TVerificacionPTAR");

            migrationBuilder.DropColumn(
                name: "P3_ReviseElContenido",
                table: "TVerificacionPTAR");

            migrationBuilder.DropColumn(
                name: "P4_CorrespondeAlEjecutado",
                table: "TVerificacionPTAR");

            migrationBuilder.DropColumn(
                name: "P5_NoSeEjecutaElTrabajo",
                table: "TVerificacionPTAR");

            migrationBuilder.DropColumn(
                name: "P6_RevisadoyFirmado",
                table: "TVerificacionPTAR");

            migrationBuilder.DropColumn(
                name: "CodAnterior",
                table: "TAnalisisCausa");
        }
    }
}
