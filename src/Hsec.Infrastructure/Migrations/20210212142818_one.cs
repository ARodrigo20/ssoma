using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hsec.Infrastructure.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TAcceso",
                columns: table => new
                {
                    CodAcceso = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPadre = table.Column<int>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 200, nullable: true),
                    Componente = table.Column<string>(maxLength: 100, nullable: false),
                    Icono = table.Column<string>(maxLength: 40, nullable: true),
                    BadgeVariant = table.Column<string>(nullable: true),
                    BadgeText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAcceso_CodAcceso", x => x.CodAcceso);
                    table.ForeignKey(
                        name: "FK_1n_Acceso_Acceso",
                        column: x => x.CodPadre,
                        principalTable: "TAcceso",
                        principalColumn: "CodAcceso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TAccion",
                columns: table => new
                {
                    CodAccion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodActiRelacionada = table.Column<string>(nullable: true),
                    CodAreaHsec = table.Column<string>(nullable: true),
                    CodEstadoAccion = table.Column<string>(nullable: true),
                    CodNivelRiesgo = table.Column<string>(nullable: true),
                    CodTipoAccion = table.Column<string>(nullable: true),
                    CodSolicitadoPor = table.Column<string>(nullable: true),
                    DocReferencia = table.Column<string>(nullable: true),
                    DocSubReferencia = table.Column<string>(nullable: true),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    Tarea = table.Column<string>(nullable: true),
                    FechaInicial = table.Column<DateTime>(nullable: false),
                    FechaFinal = table.Column<DateTime>(nullable: false),
                    CodTablaRef = table.Column<string>(nullable: true),
                    Aprobador = table.Column<string>(nullable: true),
                    EstadoAprobacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAccion", x => x.CodAccion);
                });

            migrationBuilder.CreateTable(
                name: "TAcreditacionCurso",
                columns: table => new
                {
                    CodCurso = table.Column<string>(maxLength: 50, nullable: false),
                    CodPersona = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodStiker = table.Column<string>(nullable: true),
                    Candado = table.Column<string>(nullable: true),
                    FechaStiker = table.Column<DateTime>(nullable: true),
                    FechaTarjeta = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAcreditacionCurso", x => new { x.CodCurso, x.CodPersona });
                });

            migrationBuilder.CreateTable(
                name: "TAnalisisCausa",
                columns: table => new
                {
                    CodAnalisis = table.Column<string>(maxLength: 10, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPadre = table.Column<string>(maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Nivel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAnalisisCausa", x => x.CodAnalisis);
                    table.ForeignKey(
                        name: "FK_TAnalisisCausa_TAnalisisCausa_CodPadre",
                        column: x => x.CodPadre,
                        principalTable: "TAnalisisCausa",
                        principalColumn: "CodAnalisis",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TAnalisisHallazgo",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false),
                    CodHallazgo = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipoHallazgo = table.Column<string>(maxLength: 20, nullable: true),
                    CodTipoNoConfor = table.Column<string>(maxLength: 20, nullable: true),
                    DescripcionNoConf = table.Column<string>(maxLength: 8000, nullable: true),
                    CodRespAccInmediata = table.Column<string>(maxLength: 20, nullable: true),
                    FecAccInmediata = table.Column<DateTime>(nullable: true),
                    DescripcionAcc = table.Column<string>(maxLength: 8000, nullable: true),
                    CodRespVeriSegui = table.Column<string>(maxLength: 20, nullable: true),
                    CodAceptada = table.Column<string>(maxLength: 20, nullable: true),
                    FecVeriSegui = table.Column<DateTime>(nullable: true),
                    DescripcionVerSegui = table.Column<string>(maxLength: 8000, nullable: true),
                    CodRespConEfec = table.Column<string>(maxLength: 20, nullable: true),
                    CodEfectividadAc = table.Column<string>(maxLength: 20, nullable: true),
                    FecConEfec = table.Column<DateTime>(nullable: true),
                    DescripcionConEfec = table.Column<string>(maxLength: 8000, nullable: true),
                    CodRespCierNoConfor = table.Column<string>(maxLength: 20, nullable: true),
                    FecCierNoConfor = table.Column<DateTime>(nullable: true),
                    DescripcionCierNoConfor = table.Column<string>(maxLength: 8000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAnalisisHallazgo", x => new { x.Correlativo, x.CodHallazgo });
                });

            migrationBuilder.CreateTable(
                name: "TAprobacion",
                columns: table => new
                {
                    CodAprobacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    DocReferencia = table.Column<string>(maxLength: 20, nullable: true),
                    ProcesoAprobacion = table.Column<string>(maxLength: 400, nullable: true),
                    Version = table.Column<int>(maxLength: 5, nullable: false),
                    CadenaAprobacion = table.Column<string>(maxLength: 400, nullable: true),
                    EstadoDoc = table.Column<string>(maxLength: 1, nullable: true),
                    CodResponsable = table.Column<string>(nullable: true),
                    CodJerResponsable = table.Column<string>(nullable: true),
                    CodTabla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAprobacion", x => x.CodAprobacion);
                });

            migrationBuilder.CreateTable(
                name: "TAprobacionPlan",
                columns: table => new
                {
                    CodAprobacion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodAccion = table.Column<int>(nullable: false),
                    DocReferencia = table.Column<string>(maxLength: 50, nullable: true),
                    EstadoDoc = table.Column<string>(maxLength: 1, nullable: true),
                    CodTabla = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAprobacionPlan", x => x.CodAprobacion);
                });

            migrationBuilder.CreateTable(
                name: "TAprobacionPlanHistorial",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodAprobacion = table.Column<int>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 30, nullable: true),
                    Comentario = table.Column<string>(nullable: true),
                    EstadoAprobacion = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAprobacionPlanHistorial", x => x.Correlativo);
                });

            migrationBuilder.CreateTable(
                name: "TAudCartilla",
                columns: table => new
                {
                    CodAuditoria = table.Column<string>(maxLength: 20, nullable: false),
                    CodCartilla = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPeligroFatal = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAudCartilla", x => new { x.CodAuditoria, x.CodCartilla });
                });

            migrationBuilder.CreateTable(
                name: "TAuditoria",
                columns: table => new
                {
                    CodAuditoria = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    AuditoriaDescripcion = table.Column<string>(maxLength: 8000, nullable: true),
                    CodTipoAuditoria = table.Column<string>(maxLength: 20, nullable: true),
                    CodAreaAlcance = table.Column<string>(maxLength: 20, nullable: true),
                    CodPosicionGer = table.Column<string>(maxLength: 20, nullable: true),
                    CodPosicionSup = table.Column<string>(maxLength: 20, nullable: true),
                    CodContrata = table.Column<string>(maxLength: 20, nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    CodRespAuditoria = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAuditoria", x => x.CodAuditoria);
                });

            migrationBuilder.CreateTable(
                name: "TAuditoriaAnalisisCausalidad",
                columns: table => new
                {
                    CodAnalisis = table.Column<string>(maxLength: 20, nullable: false),
                    CodHallazgo = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodCausa = table.Column<string>(nullable: true),
                    Comentario = table.Column<string>(maxLength: 600, nullable: true),
                    CodCondicion = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAuditoriaAnalisisCausalidad", x => new { x.CodAnalisis, x.CodHallazgo });
                });

            migrationBuilder.CreateTable(
                name: "TCargo",
                columns: table => new
                {
                    Ocupacion = table.Column<string>(maxLength: 35, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodWebControl = table.Column<string>(maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCargo_Ocupacion", x => x.Ocupacion);
                });

            migrationBuilder.CreateTable(
                name: "TCartilla",
                columns: table => new
                {
                    CodCartilla = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    DesCartilla = table.Column<string>(maxLength: 350, nullable: true),
                    TipoCartilla = table.Column<string>(maxLength: 40, nullable: false),
                    PeligroFatal = table.Column<string>(maxLength: 50, nullable: false),
                    Modulo = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCartilla", x => x.CodCartilla);
                });

            migrationBuilder.CreateTable(
                name: "TComite",
                columns: table => new
                {
                    CodComite = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Hora = table.Column<string>(nullable: true),
                    CodTipoComite = table.Column<string>(nullable: true),
                    CodCategoria = table.Column<string>(nullable: true),
                    CodPosicionGer = table.Column<string>(nullable: true),
                    CodPosicionSup = table.Column<string>(nullable: true),
                    Lugar = table.Column<string>(nullable: true),
                    DetalleApertura = table.Column<string>(nullable: true),
                    CodSecretario = table.Column<string>(nullable: true),
                    ResumenSalud = table.Column<string>(nullable: true),
                    ResumenSeguridad = table.Column<string>(nullable: true),
                    ResumenMedioAmbiente = table.Column<string>(nullable: true),
                    ResumenComunidad = table.Column<string>(nullable: true),
                    FechaCierre = table.Column<DateTime>(nullable: false),
                    HoraCierre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TComite", x => x.CodComite);
                });

            migrationBuilder.CreateTable(
                name: "TControlCritico",
                columns: table => new
                {
                    CodCC = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodRiesgo = table.Column<string>(maxLength: 40, nullable: true),
                    DesCC = table.Column<string>(maxLength: 200, nullable: false),
                    TipoCC = table.Column<string>(maxLength: 50, nullable: true),
                    PeligroFatal = table.Column<string>(maxLength: 50, nullable: true),
                    Modulo = table.Column<string>(maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TControlCritico", x => x.CodCC);
                });

            migrationBuilder.CreateTable(
                name: "TCursoRules",
                columns: table => new
                {
                    RecurrenceID = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    RecurrenceRule = table.Column<string>(nullable: true),
                    RecurrenceException = table.Column<string>(nullable: true),
                    TipoRecurrenceRule = table.Column<bool>(nullable: false),
                    CodTemaCapacita = table.Column<string>(nullable: true),
                    CodTipoTema = table.Column<string>(nullable: true),
                    CodAreaCapacita = table.Column<string>(nullable: true),
                    CodEmpCapacita = table.Column<string>(nullable: true),
                    PuntajeTotal = table.Column<decimal>(nullable: true),
                    PorcAprobacion = table.Column<int>(nullable: false),
                    CodLugarCapacita = table.Column<string>(nullable: true),
                    CodSala = table.Column<string>(nullable: true),
                    Capacidad = table.Column<int>(nullable: true),
                    Vigencia = table.Column<int>(nullable: true),
                    CodVigenciaCapacita = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    FechaLimite = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCursoRules", x => x.RecurrenceID);
                });

            migrationBuilder.CreateTable(
                name: "TDatosHallazgo",
                columns: table => new
                {
                    Correlativo = table.Column<int>(maxLength: 20, nullable: false),
                    CodHallazgo = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipoHallazgo = table.Column<string>(maxLength: 20, nullable: true),
                    Descripcion = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDatosHallazgo", x => new { x.Correlativo, x.CodHallazgo });
                });

            migrationBuilder.CreateTable(
                name: "TFile",
                columns: table => new
                {
                    CorrelativoArchivos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    NroDocReferencia = table.Column<string>(nullable: true),
                    NroSubDocReferencia = table.Column<string>(nullable: true),
                    GrupoPertenece = table.Column<int>(nullable: true),
                    ArchivoData = table.Column<byte[]>(nullable: true),
                    PreviewData = table.Column<byte[]>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    TipoArchivo = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    CodTablaRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TFile", x => x.CorrelativoArchivos);
                });

            migrationBuilder.CreateTable(
                name: "TGestionRiesgo",
                columns: table => new
                {
                    CodGestionRiesgo = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodGestionRiesgoPadre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    DetalleAsociado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TGestionRiesgo", x => x.CodGestionRiesgo);
                    table.ForeignKey(
                        name: "FK_TGestionRiesgo_TGestionRiesgo_CodGestionRiesgoPadre",
                        column: x => x.CodGestionRiesgoPadre,
                        principalTable: "TGestionRiesgo",
                        principalColumn: "CodGestionRiesgo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TIncidente",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(nullable: true),
                    CodTituloInci = table.Column<string>(nullable: true),
                    DescripcionIncidente = table.Column<string>(nullable: true),
                    CodPosicionGer = table.Column<string>(nullable: true),
                    CodRespGerencia = table.Column<string>(nullable: true),
                    CodPosicionSup = table.Column<string>(nullable: true),
                    CodRespSuperint = table.Column<string>(nullable: true),
                    CodProveedor = table.Column<string>(nullable: true),
                    CodRespProveedor = table.Column<string>(nullable: true),
                    CodPerReporta = table.Column<string>(nullable: true),
                    CodAreaHsec = table.Column<string>(nullable: true),
                    FechaDelSuceso = table.Column<DateTime>(nullable: true),
                    HoraDelSuceso = table.Column<string>(nullable: true),
                    CodTurno = table.Column<string>(nullable: true),
                    CodUbicacion = table.Column<string>(nullable: true),
                    CodSubUbicacion = table.Column<string>(nullable: true),
                    CodUbicacionEspecifica = table.Column<string>(nullable: true),
                    DesUbicacion = table.Column<string>(nullable: true),
                    CodTipoIncidente = table.Column<string>(nullable: true),
                    CodSubTipoIncidente = table.Column<string>(nullable: true),
                    CodClasificaInci = table.Column<int>(nullable: true),
                    CodClasiPotencial = table.Column<string>(nullable: true),
                    CodLesAper = table.Column<string>(nullable: true),
                    CodMedioAmbiente = table.Column<int>(nullable: true),
                    CodActiRelacionada = table.Column<int>(nullable: true),
                    CodHha = table.Column<string>(nullable: true),
                    ResumenInfMedico = table.Column<string>(nullable: true),
                    Conclusiones = table.Column<string>(nullable: true),
                    Aprendizajes = table.Column<string>(nullable: true),
                    CodGrupoRiesgo = table.Column<string>(nullable: true),
                    CodRiesgo = table.Column<string>(nullable: true),
                    Oculto = table.Column<int>(nullable: true),
                    EstadoIncidente = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIncidente", x => x.CodIncidente);
                });

            migrationBuilder.CreateTable(
                name: "TInspeccion",
                columns: table => new
                {
                    CodInspeccion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 20, nullable: true),
                    CodTipo = table.Column<string>(maxLength: 20, nullable: true),
                    CodContrata = table.Column<string>(maxLength: 20, nullable: true),
                    AreaAlcance = table.Column<string>(nullable: true),
                    FechaP = table.Column<DateTime>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: true),
                    Hora = table.Column<string>(maxLength: 10, nullable: true),
                    Gerencia = table.Column<string>(maxLength: 10, nullable: true),
                    SuperInt = table.Column<string>(maxLength: 10, nullable: true),
                    CodUbicacion = table.Column<string>(maxLength: 10, nullable: true),
                    CodSubUbicacion = table.Column<string>(maxLength: 10, nullable: true),
                    Objetivo = table.Column<string>(maxLength: 2000, nullable: true),
                    Conclusion = table.Column<string>(maxLength: 2000, nullable: true),
                    Dispositivo = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TInspeccion", x => x.CodInspeccion);
                });

            migrationBuilder.CreateTable(
                name: "TJerarquia",
                columns: table => new
                {
                    CodPosicion = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    CodPosicionPadre = table.Column<int>(nullable: true),
                    CodElipse = table.Column<string>(nullable: true),
                    CodElipsePadre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    PathJerarquia = table.Column<string>(nullable: true),
                    NivelJerarquia = table.Column<short>(nullable: true),
                    PathJerarquiaOriginal = table.Column<string>(nullable: true),
                    NivelJerarquiaOriginal = table.Column<short>(nullable: true),
                    CodTipoPersona = table.Column<string>(nullable: true),
                    Visibilidad = table.Column<bool>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    FlagDescMod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TJerarquia", x => x.CodPosicion);
                    table.ForeignKey(
                        name: "FK_TJerarquia_TJerarquia_CodPosicionPadre",
                        column: x => x.CodPosicionPadre,
                        principalTable: "TJerarquia",
                        principalColumn: "CodPosicion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TJerarquiaResponsable",
                columns: table => new
                {
                    CodPosicion = table.Column<int>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TJerarquiaResponsable_CodPosicion_CodPersona", x => new { x.CodPosicion, x.CodPersona });
                });

            migrationBuilder.CreateTable(
                name: "TMaestro",
                columns: table => new
                {
                    CodTabla = table.Column<string>(maxLength: 10, nullable: false),
                    CodTipo = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 800, nullable: false),
                    DescripcionCorta = table.Column<string>(maxLength: 600, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMaestro_TablaTipo", x => new { x.CodTabla, x.CodTipo });
                });

            migrationBuilder.CreateTable(
                name: "TModulo",
                columns: table => new
                {
                    CodModulo = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodModuloPadre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TModulo", x => x.CodModulo);
                    table.ForeignKey(
                        name: "FK_TModulo_TModulo_CodModuloPadre",
                        column: x => x.CodModuloPadre,
                        principalTable: "TModulo",
                        principalColumn: "CodModulo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionComportamientos",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CompObservada = table.Column<string>(nullable: true),
                    CompAccionInmediata = table.Column<string>(nullable: true),
                    CodActiRelacionada = table.Column<string>(nullable: true),
                    CodEstado = table.Column<string>(nullable: true),
                    CodHha = table.Column<string>(nullable: true),
                    CodErrorObs = table.Column<string>(nullable: true),
                    CodActoSubEstandar = table.Column<string>(nullable: true),
                    CodStopWork = table.Column<string>(nullable: true),
                    CodCorreccion = table.Column<string>(nullable: true),
                    CodSubTipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservacionComportamientos", x => x.CodObservacion);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionCondiciones",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(nullable: true),
                    CondObservada = table.Column<string>(nullable: true),
                    CondAccionInmediata = table.Column<string>(nullable: true),
                    CodActiRelacionada = table.Column<string>(nullable: true),
                    CodHha = table.Column<string>(nullable: true),
                    CodCondicionSubEstandar = table.Column<string>(nullable: true),
                    CodStopWork = table.Column<string>(nullable: true),
                    CodCorreccion = table.Column<string>(nullable: true),
                    CodSubTipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservacionCondiciones", x => x.CodObservacion);
                });

            migrationBuilder.CreateTable(
                name: "TObservaciones",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPosicionGer = table.Column<string>(nullable: true),
                    CodPosicionSup = table.Column<string>(nullable: true),
                    CodAreaHsec = table.Column<string>(nullable: true),
                    CodTipoObservacion = table.Column<int>(nullable: false),
                    CodSubTipoObs = table.Column<string>(nullable: true),
                    CodNivelRiesgo = table.Column<string>(nullable: true),
                    CodObservadoPor = table.Column<string>(nullable: true),
                    FechaObservacion = table.Column<DateTime>(nullable: true),
                    HoraObservacion = table.Column<string>(nullable: true),
                    CodUbicacion = table.Column<string>(nullable: true),
                    CodSubUbicacion = table.Column<string>(nullable: true),
                    CodUbicacionEspecifica = table.Column<string>(nullable: true),
                    DesUbicacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservaciones", x => x.CodObservacion);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionIteracciones",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodLiderPersona = table.Column<string>(nullable: true),
                    CodContratista = table.Column<string>(nullable: true),
                    EquipoInvolucrado = table.Column<string>(nullable: true),
                    DetalleComportamiento = table.Column<string>(nullable: true),
                    AccionesInmediatas = table.Column<string>(nullable: true),
                    IteraccionSeguridad = table.Column<string>(nullable: true),
                    OtroActividadRiesgo = table.Column<string>(nullable: true),
                    OtroComponente = table.Column<string>(nullable: true),
                    ActividadTareaObs = table.Column<string>(nullable: true),
                    CodStopWork = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservacionIteracciones", x => x.CodObservacion);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionTareas",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    TareaObservada = table.Column<string>(nullable: true),
                    Pet = table.Column<string>(nullable: true),
                    CodActiRelacionada = table.Column<string>(nullable: true),
                    CodHha = table.Column<string>(nullable: true),
                    CodErrorObs = table.Column<string>(nullable: true),
                    CodEstadoObs = table.Column<string>(nullable: true),
                    ComenRecoOpor = table.Column<string>(nullable: true),
                    CodStopWork = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservacionTareas", x => x.CodObservacion);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionVerControlCritico",
                columns: table => new
                {
                    CodVcc = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodCartilla = table.Column<string>(maxLength: 50, nullable: true),
                    CodObservacion = table.Column<string>(maxLength: 50, nullable: true),
                    CodObservadoPor = table.Column<string>(maxLength: 50, nullable: true),
                    CodPosicionGer = table.Column<string>(maxLength: 50, nullable: true),
                    TareaObservada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empresa = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObservacionVerControlCritico", x => x.CodVcc);
                });

            migrationBuilder.CreateTable(
                name: "TObsISRegistroEncuestas",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    CodDescripcion = table.Column<string>(nullable: false),
                    CodEncuesta = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsISRegistroEncuestas", x => new { x.CodObservacion, x.CodDescripcion, x.CodEncuesta });
                });

            migrationBuilder.CreateTable(
                name: "TObsTaComentarios",
                columns: table => new
                {
                    Orden = table.Column<int>(nullable: false),
                    CodObservacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipoComentario = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsTaComentarios", x => new { x.CodObservacion, x.Orden });
                });

            migrationBuilder.CreateTable(
                name: "TObsTaEtapaTareas",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    TituloEtapaTarea = table.Column<string>(nullable: true),
                    DescripcionEtapaTarea = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsTaEtapaTareas", x => new { x.Correlativo, x.CodObservacion });
                });

            migrationBuilder.CreateTable(
                name: "TObsTaPersonaObservadas",
                columns: table => new
                {
                    CodPersonaMiembro = table.Column<string>(nullable: false),
                    CodObservacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsTaPersonaObservadas", x => new { x.CodObservacion, x.CodPersonaMiembro });
                });

            migrationBuilder.CreateTable(
                name: "TObsTaRegistroEncuestas",
                columns: table => new
                {
                    CodObservacion = table.Column<string>(nullable: false),
                    CodPregunta = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodRespuesta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsTaRegistroEncuestas", x => new { x.CodObservacion, x.CodPregunta });
                });

            migrationBuilder.CreateTable(
                name: "TObsVCCCierreIteraccion",
                columns: table => new
                {
                    CodCierreIt = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodVcc = table.Column<string>(maxLength: 50, nullable: true),
                    CodDesCierreIter = table.Column<string>(maxLength: 50, nullable: true),
                    Respuesta = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsVCCCierreIteraccion", x => x.CodCierreIt);
                });

            migrationBuilder.CreateTable(
                name: "TObsVCCHerramienta",
                columns: table => new
                {
                    CodHerram = table.Column<int>(maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodVcc = table.Column<string>(maxLength: 50, nullable: true),
                    CodDesHe = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsVCCHerramienta", x => x.CodHerram);
                });

            migrationBuilder.CreateTable(
                name: "TObsVCCRespuesta",
                columns: table => new
                {
                    CodRespuesta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodigoCriterio = table.Column<string>(maxLength: 50, nullable: true),
                    CodigoCC = table.Column<string>(maxLength: 50, nullable: true),
                    CodVcc = table.Column<string>(maxLength: 50, nullable: true),
                    Respuesta = table.Column<string>(maxLength: 50, nullable: true),
                    Justificacion = table.Column<string>(nullable: true),
                    AccionCorrectiva = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsVCCRespuesta", x => x.CodRespuesta);
                });

            migrationBuilder.CreateTable(
                name: "TObsVCCVerCCEfectividad",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodVcc = table.Column<string>(maxLength: 50, nullable: true),
                    CodCartilla = table.Column<string>(maxLength: 50, nullable: true),
                    CodCC = table.Column<string>(maxLength: 50, nullable: true),
                    Efectividad = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TObsVCCVerCCEfectividad", x => x.Correlativo);
                });

            migrationBuilder.CreateTable(
                name: "ToleranciaCero",
                columns: table => new
                {
                    CodTolCero = table.Column<string>(maxLength: 11, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    FechaTolerancia = table.Column<DateTime>(nullable: false),
                    CodPosicionGer = table.Column<string>(nullable: true),
                    CodPosicionSup = table.Column<string>(nullable: true),
                    Proveedor = table.Column<string>(nullable: true),
                    AntTolerancia = table.Column<string>(nullable: true),
                    IncumpDesc = table.Column<string>(nullable: true),
                    ConsecReales = table.Column<string>(nullable: true),
                    ConsecPot = table.Column<string>(nullable: true),
                    ConclusionesTol = table.Column<string>(nullable: true),
                    CodDetSancion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToleranciaCero", x => x.CodTolCero);
                });

            migrationBuilder.CreateTable(
                name: "TPais",
                columns: table => new
                {
                    CodPais = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPais", x => x.CodPais);
                });

            migrationBuilder.CreateTable(
                name: "TPeligro",
                columns: table => new
                {
                    CodPeligro = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTarea = table.Column<string>(nullable: true),
                    TipoPeligro = table.Column<string>(nullable: true),
                    DescripcionPeligro = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPeligro", x => x.CodPeligro);
                });

            migrationBuilder.CreateTable(
                name: "TPlanAnual",
                columns: table => new
                {
                    Anio = table.Column<string>(maxLength: 20, nullable: false),
                    CodMes = table.Column<string>(maxLength: 20, nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CodReferencia = table.Column<string>(maxLength: 20, nullable: false),
                    Valor = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPlanAnual", x => new { x.Anio, x.CodMes, x.CodPersona, x.CodReferencia });
                });

            migrationBuilder.CreateTable(
                name: "TPlanAnualGeneral",
                columns: table => new
                {
                    Anio = table.Column<string>(maxLength: 20, nullable: false),
                    CodMes = table.Column<string>(maxLength: 20, nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CodReferencia = table.Column<string>(maxLength: 20, nullable: false),
                    Valor = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPlanAnualGeneral", x => new { x.Anio, x.CodMes, x.CodPersona, x.CodReferencia });
                });

            migrationBuilder.CreateTable(
                name: "TPlanAnualVerConCri",
                columns: table => new
                {
                    Anio = table.Column<string>(maxLength: 20, nullable: false),
                    CodMes = table.Column<string>(maxLength: 20, nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CodReferencia = table.Column<string>(maxLength: 20, nullable: false),
                    Valor = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPlanAnualVerConCri", x => new { x.Anio, x.CodMes, x.CodPersona, x.CodReferencia });
                });

            migrationBuilder.CreateTable(
                name: "TProceso",
                columns: table => new
                {
                    CodProceso = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CadenaAprobacion = table.Column<string>(maxLength: 1000, nullable: true),
                    Descripcion = table.Column<string>(maxLength: 1000, nullable: true),
                    CodTabla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TProceso", x => x.CodProceso);
                });

            migrationBuilder.CreateTable(
                name: "TProveedor",
                columns: table => new
                {
                    CodProveedor = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: true),
                    Ruc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TProveedor", x => x.CodProveedor);
                });

            migrationBuilder.CreateTable(
                name: "TReportePF",
                columns: table => new
                {
                    CodReportePF = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodPelFatal = table.Column<string>(nullable: true),
                    CodPosicionGer = table.Column<string>(nullable: true),
                    Anio = table.Column<string>(nullable: true),
                    Mes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TReportePF", x => x.CodReportePF);
                });

            migrationBuilder.CreateTable(
                name: "TReportePFDetalle",
                columns: table => new
                {
                    CodReportePF = table.Column<int>(nullable: false),
                    CodCC = table.Column<string>(nullable: false),
                    Observacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TReporte__F758CEC892D8B74D", x => new { x.CodReportePF, x.CodCC });
                });

            migrationBuilder.CreateTable(
                name: "TRespuestaParticipante",
                columns: table => new
                {
                    CodCurso = table.Column<string>(maxLength: 50, nullable: false),
                    CodPersona = table.Column<string>(maxLength: 50, nullable: false),
                    CodPregunta = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Nota = table.Column<decimal>(nullable: true),
                    Respuesta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRespuestaParticipante", x => new { x.CodCurso, x.CodPersona, x.CodPregunta });
                });

            migrationBuilder.CreateTable(
                name: "TReunion",
                columns: table => new
                {
                    CodReunion = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Reunion = table.Column<string>(nullable: true),
                    Lugar = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Hora = table.Column<string>(nullable: true),
                    CodPerFacilitador = table.Column<string>(nullable: true),
                    Acuerdos = table.Column<string>(nullable: true),
                    Comentarios = table.Column<string>(nullable: true),
                    Otros = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TReunion", x => x.CodReunion);
                });

            migrationBuilder.CreateTable(
                name: "TRiesgo",
                columns: table => new
                {
                    CodRiesgo = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPeligro = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRiesgo", x => x.CodRiesgo);
                });

            migrationBuilder.CreateTable(
                name: "TRol",
                columns: table => new
                {
                    CodRol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRol_CodRol", x => x.CodRol);
                });

            migrationBuilder.CreateTable(
                name: "TSimulacro",
                columns: table => new
                {
                    CodSimulacro = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 10, nullable: true),
                    CodUbicacion = table.Column<string>(maxLength: 20, nullable: true),
                    CodPosicionGer = table.Column<string>(maxLength: 20, nullable: true),
                    CodRespGerencia = table.Column<string>(maxLength: 20, nullable: true),
                    CodPosicionSup = table.Column<string>(maxLength: 20, nullable: true),
                    CodRespSuperint = table.Column<string>(maxLength: 20, nullable: true),
                    CodContrata = table.Column<string>(maxLength: 20, nullable: true),
                    Suceso = table.Column<string>(maxLength: 100, nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    Hora = table.Column<string>(maxLength: 10, nullable: true),
                    HoraInicio = table.Column<string>(maxLength: 10, nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    HoraFinalizacion = table.Column<string>(maxLength: 10, nullable: true),
                    TiempoRespuesta = table.Column<string>(maxLength: 15, nullable: true),
                    Conclusiones = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSimulacro", x => x.CodSimulacro);
                });

            migrationBuilder.CreateTable(
                name: "TTemaCapacitacion",
                columns: table => new
                {
                    CodTemaCapacita = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipoTema = table.Column<string>(nullable: true),
                    CodAreaCapacita = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CompetenciaHs = table.Column<string>(nullable: true),
                    CodHha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTemaCapacitacion", x => x.CodTemaCapacita);
                });

            migrationBuilder.CreateTable(
                name: "TTipoIncidente",
                columns: table => new
                {
                    CodTipoIncidente = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPadreTipoIncidente = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTipoIncidente", x => x.CodTipoIncidente);
                    table.ForeignKey(
                        name: "FK_TTipoIncidente_TTipoIncidente_CodPadreTipoIncidente",
                        column: x => x.CodPadreTipoIncidente,
                        principalTable: "TTipoIncidente",
                        principalColumn: "CodTipoIncidente",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TUbicacion",
                columns: table => new
                {
                    CodUbicacion = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodUbicacionPadre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUbicacion", x => x.CodUbicacion);
                    table.ForeignKey(
                        name: "FK_TUbicacion_TUbicacion_CodUbicacionPadre",
                        column: x => x.CodUbicacionPadre,
                        principalTable: "TUbicacion",
                        principalColumn: "CodUbicacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TUsuario",
                columns: table => new
                {
                    CodUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Usuario = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    CodPersona = table.Column<string>(nullable: true),
                    TipoLogueo = table.Column<bool>(nullable: false),
                    Token = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUsuario_CodUsuario", x => x.CodUsuario);
                });

            migrationBuilder.CreateTable(
                name: "TValidadorArchivo",
                columns: table => new
                {
                    Correlativo = table.Column<int>(maxLength: 25, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    NroDocReferencia = table.Column<string>(nullable: true),
                    CodPersona = table.Column<string>(nullable: true),
                    CodArchivo = table.Column<int>(nullable: false),
                    EstadoAccion = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TValidadorArchivo", x => x.Correlativo);
                });

            migrationBuilder.CreateTable(
                name: "TVerificacionControlCritico",
                columns: table => new
                {
                    CodigoVCC = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    CodResponsable = table.Column<string>(maxLength: 20, nullable: true),
                    Gerencia = table.Column<string>(maxLength: 20, nullable: true),
                    SuperIndendecnia = table.Column<string>(maxLength: 20, nullable: true),
                    Empresa = table.Column<string>(maxLength: 200, nullable: true),
                    Cartilla = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVerificacionControlCritico", x => x.CodigoVCC);
                });

            migrationBuilder.CreateTable(
                name: "TVerificacionControlCriticoCartilla",
                columns: table => new
                {
                    CodigoVCC = table.Column<string>(maxLength: 20, nullable: false),
                    CodCartilla = table.Column<string>(maxLength: 20, nullable: false),
                    CodCC = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Cumplimiento = table.Column<string>(maxLength: 10, nullable: true),
                    Efectividad = table.Column<string>(maxLength: 10, nullable: true),
                    Justificacion = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVerificacionControlCriticoCartilla", x => new { x.CodigoVCC, x.CodCartilla, x.CodCC });
                });

            migrationBuilder.CreateTable(
                name: "TVerificaciones",
                columns: table => new
                {
                    CodVerificacion = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 10, nullable: true),
                    CodPosicionGer = table.Column<string>(maxLength: 20, nullable: true),
                    CodPosicionSup = table.Column<string>(maxLength: 20, nullable: true),
                    CodAreaHSEC = table.Column<string>(maxLength: 20, nullable: true),
                    CodTipoVerificacion = table.Column<string>(maxLength: 10, nullable: true),
                    CodNivelRiesgo = table.Column<string>(maxLength: 4, nullable: true),
                    CodVerificacionPor = table.Column<string>(maxLength: 20, nullable: true),
                    FechaVerificacion = table.Column<DateTime>(nullable: false),
                    HoraVerificacion = table.Column<string>(maxLength: 20, nullable: true),
                    CodUbicacion = table.Column<string>(maxLength: 20, nullable: true),
                    CodSubUbicacion = table.Column<string>(maxLength: 20, nullable: true),
                    CodUbicacionEspecifica = table.Column<string>(maxLength: 20, nullable: true),
                    DesUbicacion = table.Column<string>(maxLength: 500, nullable: true),
                    Dispositivo = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVerificaciones", x => x.CodVerificacion);
                });

            migrationBuilder.CreateTable(
                name: "TVerificacionIPERC",
                columns: table => new
                {
                    CodVerificacion = table.Column<string>(maxLength: 20, nullable: false),
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 10, nullable: true),
                    CondObservada = table.Column<string>(nullable: true),
                    CondAccionInmediata = table.Column<string>(nullable: true),
                    StopWork = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVerificacionIPERC", x => new { x.CodVerificacion, x.Correlativo });
                });

            migrationBuilder.CreateTable(
                name: "TVerificacionPTAR",
                columns: table => new
                {
                    CodVerificacion = table.Column<string>(maxLength: 20, nullable: false),
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 10, nullable: true),
                    CondObservada = table.Column<string>(nullable: true),
                    CondAccionInmediata = table.Column<string>(nullable: true),
                    StopWork = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVerificacionPTAR", x => new { x.CodVerificacion, x.Correlativo });
                });

            migrationBuilder.CreateTable(
                name: "TYoAseguro",
                columns: table => new
                {
                    CodYoAseguro = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPosGerencia = table.Column<string>(maxLength: 20, nullable: true),
                    CodPersonaResponsable = table.Column<string>(maxLength: 20, nullable: true),
                    Fecha = table.Column<DateTime>(nullable: true),
                    FechaEvalucion = table.Column<DateTime>(nullable: true),
                    ReportadosObservaciones = table.Column<int>(nullable: false),
                    CorregidosObservaciones = table.Column<int>(nullable: false),
                    ObsCriticaDia = table.Column<string>(maxLength: 2000, nullable: true),
                    Calificacion = table.Column<string>(maxLength: 10, nullable: true),
                    Comentario = table.Column<string>(maxLength: 2000, nullable: true),
                    Reunion = table.Column<string>(maxLength: 500, nullable: true),
                    Recomendaciones = table.Column<string>(maxLength: 2000, nullable: true),
                    TituloReunion = table.Column<string>(maxLength: 2000, nullable: true),
                    TemaReunion = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYoAseguro", x => x.CodYoAseguro);
                });

            migrationBuilder.CreateTable(
                name: "TYoAseguroLugar",
                columns: table => new
                {
                    CodUbicacion = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodUbicacionPadre = table.Column<string>(maxLength: 20, nullable: true),
                    Descripcion = table.Column<string>(maxLength: 100, nullable: true),
                    DiasLaborados = table.Column<int>(nullable: false),
                    DiasDescanso = table.Column<int>(nullable: false),
                    FechaReferencia = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYoAseguroLugar", x => x.CodUbicacion);
                });

            migrationBuilder.CreateTable(
                name: "TLevantamientoPlan",
                columns: table => new
                {
                    CodAccion = table.Column<int>(nullable: false),
                    CodPersona = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(type: "Date", nullable: true),
                    PorcentajeAvance = table.Column<double>(nullable: false),
                    Rechazado = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLevantamientoPlan", x => new { x.CodAccion, x.CodPersona, x.Correlativo });
                    table.ForeignKey(
                        name: "FK_TLevantamientoPlan_TAccion_CodAccion",
                        column: x => x.CodAccion,
                        principalTable: "TAccion",
                        principalColumn: "CodAccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TResponsable",
                columns: table => new
                {
                    CodAccion = table.Column<int>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TResponsable", x => new { x.CodAccion, x.CodPersona });
                    table.ForeignKey(
                        name: "FK_TResponsable_TAccion_CodAccion",
                        column: x => x.CodAccion,
                        principalTable: "TAccion",
                        principalColumn: "CodAccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "THistorialAprobacion",
                columns: table => new
                {
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodAprobacion = table.Column<int>(maxLength: 20, nullable: false),
                    CodAprobador = table.Column<string>(maxLength: 20, nullable: true),
                    Comentario = table.Column<string>(maxLength: 500, nullable: true),
                    EstadoAprobacion = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THistorialAprobacion", x => x.Correlativo);
                    table.ForeignKey(
                        name: "FK_Aprobacion_Historial",
                        column: x => x.CodAprobacion,
                        principalTable: "TAprobacion",
                        principalColumn: "CodAprobacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAudCCCriterio",
                columns: table => new
                {
                    CodAuditoria = table.Column<string>(maxLength: 20, nullable: false),
                    CodCartilla = table.Column<string>(maxLength: 20, nullable: false),
                    CodCC = table.Column<string>(maxLength: 20, nullable: false),
                    CodCriterio = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Resultado = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAudCCCriterio", x => new { x.CodAuditoria, x.CodCartilla, x.CodCC, x.CodCriterio });
                    table.ForeignKey(
                        name: "FK_TAudCCCriterio_TAudCartilla_CodAuditoria_CodCartilla",
                        columns: x => new { x.CodAuditoria, x.CodCartilla },
                        principalTable: "TAudCartilla",
                        principalColumns: new[] { "CodAuditoria", "CodCartilla" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAuditoriaTregNoConfoObserva",
                columns: table => new
                {
                    CodAuditoria = table.Column<string>(nullable: false),
                    CodNoConformidad = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAuditoriaTregNoConfoObserva", x => new { x.CodAuditoria, x.CodNoConformidad });
                    table.ForeignKey(
                        name: "FK_TAuditoriaTregNoConfoObserva_TAuditoria_CodNoConformidad",
                        column: x => x.CodNoConformidad,
                        principalTable: "TAuditoria",
                        principalColumn: "CodAuditoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEquipoAuditor",
                columns: table => new
                {
                    CodAuditoria = table.Column<string>(maxLength: 20, nullable: false),
                    Correlativo = table.Column<long>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 20, nullable: true),
                    NroEquipo = table.Column<int>(nullable: true),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: true),
                    Lider = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEquipoAuditor", x => new { x.Correlativo, x.CodAuditoria });
                    table.ForeignKey(
                        name: "FK_TEquipoAuditor_TAuditoria_CodAuditoria",
                        column: x => x.CodAuditoria,
                        principalTable: "TAuditoria",
                        principalColumn: "CodAuditoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "THallazgos",
                columns: table => new
                {
                    CodHallazgo = table.Column<string>(maxLength: 20, nullable: false),
                    CodAuditoria = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 20, nullable: true),
                    CodTipoHallazgo = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THallazgos", x => new { x.CodHallazgo, x.CodAuditoria });
                    table.ForeignKey(
                        name: "FK_THallazgos_TAuditoria_CodAuditoria",
                        column: x => x.CodAuditoria,
                        principalTable: "TAuditoria",
                        principalColumn: "CodAuditoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TListaParticipantesComite",
                columns: table => new
                {
                    CodComite = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CodTipDocIden = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TListaParticipantesComite_Id", x => new { x.CodComite, x.CodPersona });
                    table.ForeignKey(
                        name: "fk_in_TComite_TListaParticipantesComite",
                        column: x => x.CodComite,
                        principalTable: "TComite",
                        principalColumn: "CodComite",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TCartillaDetalle",
                columns: table => new
                {
                    CodCartillaDet = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodCartilla = table.Column<string>(maxLength: 50, nullable: false),
                    CodCC = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCartillaDetalle", x => x.CodCartillaDet);
                    table.ForeignKey(
                        name: "FK_TCartillaDetalle_TControlCritico_CodCC",
                        column: x => x.CodCC,
                        principalTable: "TControlCritico",
                        principalColumn: "CodCC",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TCartillaDetalle_TCartilla_CodCartilla",
                        column: x => x.CodCartilla,
                        principalTable: "TCartilla",
                        principalColumn: "CodCartilla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TCriterio",
                columns: table => new
                {
                    CodCrit = table.Column<string>(maxLength: 50, nullable: false),
                    CodCC = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Criterio = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCriterio", x => new { x.CodCC, x.CodCrit });
                    table.ForeignKey(
                        name: "FK_TCriterio_TControlCritico_CodCC",
                        column: x => x.CodCC,
                        principalTable: "TControlCritico",
                        principalColumn: "CodCC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TCurso",
                columns: table => new
                {
                    CodCurso = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    RecurrenceID = table.Column<string>(nullable: true),
                    CodTemaCapacita = table.Column<string>(nullable: true),
                    CodTipoTema = table.Column<string>(nullable: true),
                    CodAreaCapacita = table.Column<string>(nullable: true),
                    CodEmpCapacita = table.Column<string>(nullable: true),
                    PuntajeTotal = table.Column<decimal>(nullable: true),
                    PorcAprobacion = table.Column<int>(nullable: false),
                    CodLugarCapacita = table.Column<string>(nullable: true),
                    CodSala = table.Column<string>(nullable: true),
                    Capacidad = table.Column<int>(nullable: true),
                    Vigencia = table.Column<int>(nullable: true),
                    CodVigenciaCapacita = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaFin = table.Column<DateTime>(nullable: false),
                    Online = table.Column<bool>(nullable: false),
                    Enlace = table.Column<string>(nullable: true),
                    Duracion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCurso", x => x.CodCurso);
                    table.ForeignKey(
                        name: "FK_TCurso_TCursoRules_RecurrenceID",
                        column: x => x.RecurrenceID,
                        principalTable: "TCursoRules",
                        principalColumn: "RecurrenceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TAfectadoComunidad",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodComuAfec = table.Column<string>(nullable: true),
                    CodMotivo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CodTipoAfectado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAfectadoComunidad", x => new { x.Correlativo, x.CodIncidente });
                    table.ForeignKey(
                        name: "FK_TAfectadoComunidad_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAfectadoMedioAmbiente",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodImpAmbiental = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CodTipoAfectado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAfectadoMedioAmbiente", x => new { x.Correlativo, x.CodIncidente });
                    table.ForeignKey(
                        name: "FK_TAfectadoMedioAmbiente_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAfectadoPropiedad",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipActivo = table.Column<string>(nullable: true),
                    CodActivo = table.Column<string>(nullable: true),
                    Operador = table.Column<string>(nullable: true),
                    Daño = table.Column<string>(nullable: true),
                    CodCosto = table.Column<string>(nullable: true),
                    Monto = table.Column<decimal>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    CodTipoAfectado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAfectadoPropiedad", x => new { x.Correlativo, x.CodIncidente });
                    table.ForeignKey(
                        name: "FK_TAfectadoPropiedad_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TDetalleAfectado",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    DesSuceso = table.Column<string>(nullable: true),
                    DesDanioLesImpacPerd = table.Column<string>(nullable: true),
                    AccInmediatas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDetalleAfectado", x => new { x.Correlativo, x.CodIncidente });
                    table.ForeignKey(
                        name: "FK_TDetalleAfectado_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TDiasPerdidosAfectado",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CodPersona = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(nullable: true),
                    CodTipAccidente = table.Column<string>(nullable: true),
                    PeridoAnio = table.Column<string>(nullable: true),
                    PeridoMes = table.Column<string>(nullable: true),
                    CantidadDias = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDiasPerdidosAfectado", x => new { x.Correlativo, x.CodIncidente, x.CodPersona });
                    table.ForeignKey(
                        name: "FK_TDiasPerdidosAfectado_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEquipoInvestigacion",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    NroEquipo = table.Column<int>(nullable: true),
                    CodTabla = table.Column<string>(nullable: true),
                    CodPersona = table.Column<string>(nullable: true),
                    AreaDes = table.Column<string>(nullable: true),
                    Lider = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEquipoInvestigacion", x => new { x.Correlativo, x.CodIncidente });
                    table.ForeignKey(
                        name: "FK_TEquipoInvestigacion_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIcam",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipoIcamfactOrg = table.Column<string>(nullable: true),
                    FactOrg = table.Column<string>(nullable: true),
                    CodTipoCondEntIcam = table.Column<string>(nullable: true),
                    CondEnt = table.Column<string>(nullable: true),
                    CodTipoAccEquipIcam = table.Column<string>(nullable: true),
                    AccEquip = table.Column<string>(nullable: true),
                    CodTipoDefenAusen = table.Column<string>(nullable: true),
                    DefenAusen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIcam", x => new { x.Correlativo, x.CodIncidente });
                    table.ForeignKey(
                        name: "FK_TIcam_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TIncidenteAnalisisCausa",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    CodAnalisis = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    CodCondicion = table.Column<string>(nullable: true),
                    CodCausa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIncidenteAnalisisCausa", x => new { x.CodIncidente, x.CodAnalisis });
                    table.ForeignKey(
                        name: "FK_TIncidenteAnalisisCausa_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TInvestigaAfectado",
                columns: table => new
                {
                    Correlativo = table.Column<long>(nullable: false),
                    CodIncidente = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(nullable: true),
                    CodTipoAfectado = table.Column<string>(nullable: true),
                    DocAfectado = table.Column<string>(nullable: true),
                    Empresa = table.Column<string>(nullable: true),
                    Cargo = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<DateTime>(nullable: true),
                    CodRegimen = table.Column<int>(nullable: true),
                    DiasDeTrabajo = table.Column<string>(nullable: true),
                    CodSisTrabajo = table.Column<string>(nullable: true),
                    PorcenDiasTrabajados = table.Column<string>(nullable: true),
                    HorasLaboradas = table.Column<string>(nullable: true),
                    CodGuardia = table.Column<string>(nullable: true),
                    CodExperiencia = table.Column<string>(nullable: true),
                    DesExperiencia = table.Column<string>(nullable: true),
                    CodTipoPersona = table.Column<int>(nullable: true),
                    CodZonasDeLesion = table.Column<int>(nullable: true),
                    ZonasDeLesion = table.Column<string>(nullable: true),
                    CodMecLesion = table.Column<string>(nullable: true),
                    CodNatLesion = table.Column<string>(nullable: true),
                    CodClasificaInci = table.Column<int>(nullable: true),
                    NroAtencionMedia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TInvestigaAfectado", x => new { x.CodIncidente, x.Correlativo });
                    table.ForeignKey(
                        name: "FK_TInvestigaAfectado_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TSecuenciaEvento",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(nullable: true),
                    Orden = table.Column<string>(nullable: true),
                    Evento = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSecuenciaEvento", x => new { x.CodIncidente, x.Correlativo });
                    table.ForeignKey(
                        name: "FK_TSecuenciaEvento_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TTestigoInvolucrado",
                columns: table => new
                {
                    CodIncidente = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    NroGrupo = table.Column<int>(nullable: true),
                    CodTabla = table.Column<string>(nullable: true),
                    CodPersona = table.Column<string>(nullable: true),
                    Manifestacion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTestigoInvolucrado", x => new { x.CodIncidente, x.Correlativo });
                    table.ForeignKey(
                        name: "FK_TTestigoInvolucrado_TIncidente_CodIncidente",
                        column: x => x.CodIncidente,
                        principalTable: "TIncidente",
                        principalColumn: "CodIncidente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TDetalleInspeccion",
                columns: table => new
                {
                    Correlativo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodInspeccion = table.Column<string>(nullable: true),
                    NroDetInspeccion = table.Column<int>(nullable: false),
                    CodTabla = table.Column<string>(nullable: true),
                    Lugar = table.Column<string>(nullable: true),
                    CodUbicacion = table.Column<string>(nullable: true),
                    CodAspectoObs = table.Column<string>(nullable: true),
                    CodActividadRel = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    CodNivelRiesgo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDetalleInspeccion", x => x.Correlativo);
                    table.ForeignKey(
                        name: "FK_1n_Inspeccion_Detalle",
                        column: x => x.CodInspeccion,
                        principalTable: "TInspeccion",
                        principalColumn: "CodInspeccion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TEquipoInspeccion",
                columns: table => new
                {
                    CodInspeccion = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Correlativo = table.Column<long>(nullable: true),
                    Lider = table.Column<string>(nullable: true),
                    NroEquipo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TInspeccion_EquipoInspeccion", x => new { x.CodInspeccion, x.CodPersona });
                    table.ForeignKey(
                        name: "FK_1n_Inspeccion_EquipoInspeccion",
                        column: x => x.CodInspeccion,
                        principalTable: "TInspeccion",
                        principalColumn: "CodInspeccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TInspeccionAnalisisCausa",
                columns: table => new
                {
                    Correlativo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodInspeccion = table.Column<string>(nullable: true),
                    CodAnalisis = table.Column<string>(nullable: true),
                    CodCausa = table.Column<string>(nullable: true),
                    CodCondicion = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TInspeccionAnalisisCausa", x => x.Correlativo);
                    table.ForeignKey(
                        name: "FK_1n_Inspeccion_Analisis_Causa",
                        column: x => x.CodInspeccion,
                        principalTable: "TInspeccion",
                        principalColumn: "CodInspeccion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TPersonaAtendida",
                columns: table => new
                {
                    CodInspeccion = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Correlativo = table.Column<long>(nullable: false),
                    CodTabla = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TInspeccion_PersonaAtendida", x => new { x.CodInspeccion, x.CodPersona });
                    table.ForeignKey(
                        name: "FK_1n_Inspeccion_Persona",
                        column: x => x.CodInspeccion,
                        principalTable: "TInspeccion",
                        principalColumn: "CodInspeccion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TJerarquiaPersona",
                columns: table => new
                {
                    CodPosicion = table.Column<int>(nullable: false),
                    CodPersona = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodElipse = table.Column<string>(nullable: true),
                    CodTipoPersona = table.Column<int>(nullable: false),
                    PosicionPrimaria = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TJerarquiaPersona", x => new { x.CodPosicion, x.CodPersona });
                    table.ForeignKey(
                        name: "FK_TJerarquiaPersona_TJerarquia_CodPosicion",
                        column: x => x.CodPosicion,
                        principalTable: "TJerarquia",
                        principalColumn: "CodPosicion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPersonaTolerancia",
                columns: table => new
                {
                    CodTolCero = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Correlativo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPersonaTolerancia_Id", x => new { x.CodTolCero, x.CodPersona });
                    table.ForeignKey(
                        name: "fk_in_TToleranciaCero_TPersonaTolerancia",
                        column: x => x.CodTolCero,
                        principalTable: "ToleranciaCero",
                        principalColumn: "CodTolCero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRegTolDetalle",
                columns: table => new
                {
                    CodRegla = table.Column<string>(maxLength: 20, nullable: false),
                    CodTolCero = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToleranciaRegla_Id", x => new { x.CodTolCero, x.CodRegla });
                    table.ForeignKey(
                        name: "fk_in_TToleranciaCero_ToleranciaRegla",
                        column: x => x.CodTolCero,
                        principalTable: "ToleranciaCero",
                        principalColumn: "CodTolCero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TToleranciaCeroAnalisisCausa",
                columns: table => new
                {
                    CodTolCero = table.Column<string>(nullable: false),
                    CodAnalisis = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    CodCondicion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TToleranciaCeroAnalisisCausa_Id", x => new { x.CodTolCero, x.CodAnalisis });
                    table.ForeignKey(
                        name: "fk_in_TToleranciaCero_TToleranciaCeroAnalisisCausa",
                        column: x => x.CodTolCero,
                        principalTable: "ToleranciaCero",
                        principalColumn: "CodTolCero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPersona",
                columns: table => new
                {
                    CodPersona = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTipoPersona = table.Column<string>(nullable: true),
                    CodProveedor = table.Column<string>(nullable: true),
                    CodPais = table.Column<string>(nullable: true),
                    CodRrhh = table.Column<string>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    ApellidoPaterno = table.Column<string>(nullable: true),
                    ApellidoMaterno = table.Column<string>(nullable: true),
                    Direccion1 = table.Column<string>(nullable: true),
                    Direccion2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    GrupoSanguineo = table.Column<string>(nullable: true),
                    Ocupacion = table.Column<string>(nullable: true),
                    Profesion = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: true),
                    EstadoCivil = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    Sueldo = table.Column<decimal>(nullable: true),
                    FechaCese = table.Column<DateTime>(nullable: true),
                    ObsPersona = table.Column<string>(nullable: true),
                    Turno = table.Column<string>(nullable: true),
                    Guardia = table.Column<string>(nullable: true),
                    Empresa = table.Column<string>(nullable: true),
                    CodCargo = table.Column<string>(nullable: true),
                    FlagPersistente = table.Column<string>(nullable: true),
                    NroDocumento = table.Column<string>(nullable: true),
                    CodTipDocIden = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPersona", x => x.CodPersona);
                    table.ForeignKey(
                        name: "FK_TPersona_TPais_CodPais",
                        column: x => x.CodPais,
                        principalTable: "TPais",
                        principalColumn: "CodPais",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TPersona_TProveedor_CodProveedor",
                        column: x => x.CodProveedor,
                        principalTable: "TProveedor",
                        principalColumn: "CodProveedor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TAgenda",
                columns: table => new
                {
                    CodReunion = table.Column<string>(nullable: false),
                    Correlativo = table.Column<int>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    DesAgenda = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAgenda_Id", x => new { x.CodReunion, x.Correlativo });
                    table.ForeignKey(
                        name: "fk_in_TReunion_TAgenda",
                        column: x => x.CodReunion,
                        principalTable: "TReunion",
                        principalColumn: "CodReunion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAsistentesReunion",
                columns: table => new
                {
                    CodReunion = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAsistentesReunion_Id", x => new { x.CodReunion, x.CodPersona });
                    table.ForeignKey(
                        name: "fk_in_TReunion_TAsistentesReunion",
                        column: x => x.CodReunion,
                        principalTable: "TReunion",
                        principalColumn: "CodReunion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAusentesReunion",
                columns: table => new
                {
                    CodReunion = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAusentesReunion_Id", x => new { x.CodReunion, x.CodPersona });
                    table.ForeignKey(
                        name: "fk_in_TReunion_TAusentesReunion",
                        column: x => x.CodReunion,
                        principalTable: "TReunion",
                        principalColumn: "CodReunion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TJustificadosReunion",
                columns: table => new
                {
                    CodReunion = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TJustificadosReunion_Id", x => new { x.CodReunion, x.CodPersona });
                    table.ForeignKey(
                        name: "fk_in_TReunion_TJustificadosReunion",
                        column: x => x.CodReunion,
                        principalTable: "TReunion",
                        principalColumn: "CodReunion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRolAcceso",
                columns: table => new
                {
                    CodRol = table.Column<int>(nullable: false),
                    CodAcceso = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRolAcesso_CodRol", x => new { x.CodRol, x.CodAcceso });
                    table.ForeignKey(
                        name: "FK_1n_Acceso_RolAcceso",
                        column: x => x.CodAcceso,
                        principalTable: "TAcceso",
                        principalColumn: "CodAcceso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_1n_Rol_RolAcceso",
                        column: x => x.CodRol,
                        principalTable: "TRol",
                        principalColumn: "CodRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEquipoSimulacro",
                columns: table => new
                {
                    CodSimulacro = table.Column<string>(maxLength: 20, nullable: false),
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 10, nullable: true),
                    CodPersona = table.Column<string>(maxLength: 20, nullable: true),
                    NroEquipo = table.Column<int>(nullable: false),
                    Lider = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSimulacro_Equipo", x => new { x.CodSimulacro, x.Correlativo });
                    table.ForeignKey(
                        name: "FK_1n_Simulacro_Equipo",
                        column: x => x.CodSimulacro,
                        principalTable: "TSimulacro",
                        principalColumn: "CodSimulacro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TObservacionSimulacro",
                columns: table => new
                {
                    CodSimulacro = table.Column<string>(maxLength: 20, nullable: false),
                    Correlativo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Hora = table.Column<string>(maxLength: 10, nullable: true),
                    Suceso = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSimulacro_Observacion", x => new { x.CodSimulacro, x.Correlativo });
                    table.ForeignKey(
                        name: "FK_1n_Simulacro_Observacion",
                        column: x => x.CodSimulacro,
                        principalTable: "TSimulacro",
                        principalColumn: "CodSimulacro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TRegEncuestaSimulacro",
                columns: table => new
                {
                    CodSimulacro = table.Column<string>(maxLength: 20, nullable: false),
                    CodPregunta = table.Column<string>(maxLength: 20, nullable: false),
                    CodRespuesta = table.Column<string>(maxLength: 20, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodTabla = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSimulacro_Encuesta", x => new { x.CodSimulacro, x.CodPregunta, x.CodRespuesta });
                    table.ForeignKey(
                        name: "FK_1n_Simulacro_Encuesta",
                        column: x => x.CodSimulacro,
                        principalTable: "TSimulacro",
                        principalColumn: "CodSimulacro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPlanTema",
                columns: table => new
                {
                    CodTemaCapacita = table.Column<string>(maxLength: 50, nullable: false),
                    CodReferencia = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Tipo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPlanTema", x => new { x.CodTemaCapacita, x.CodReferencia });
                    table.ForeignKey(
                        name: "FK_TPlanTema_TTemaCapacitacion_CodTemaCapacita",
                        column: x => x.CodTemaCapacita,
                        principalTable: "TTemaCapacitacion",
                        principalColumn: "CodTemaCapacita",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TTemaCapEspecifico",
                columns: table => new
                {
                    CodTemaCapacita = table.Column<string>(maxLength: 50, nullable: false),
                    CodPeligro = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodRiesgo = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTemaCapEspecifico", x => new { x.CodTemaCapacita, x.CodPeligro });
                    table.ForeignKey(
                        name: "FK_TTemaCapEspecifico_TTemaCapacitacion_CodTemaCapacita",
                        column: x => x.CodTemaCapacita,
                        principalTable: "TTemaCapacitacion",
                        principalColumn: "CodTemaCapacita",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TUsuarioRol",
                columns: table => new
                {
                    CodUsuario = table.Column<int>(nullable: false),
                    CodRol = table.Column<int>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUsuarioRol_CodUsuario_CodRol", x => new { x.CodRol, x.CodUsuario });
                    table.ForeignKey(
                        name: "FK_1n_Rol_UsuarioRol",
                        column: x => x.CodRol,
                        principalTable: "TRol",
                        principalColumn: "CodRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_1n_Usuario_UsuarioRol",
                        column: x => x.CodUsuario,
                        principalTable: "TUsuario",
                        principalColumn: "CodUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPersonaYoAseguro",
                columns: table => new
                {
                    CodYoAseguro = table.Column<string>(nullable: false),
                    CodPersona = table.Column<string>(nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYoAseguro_CodPersona", x => new { x.CodYoAseguro, x.CodPersona });
                    table.ForeignKey(
                        name: "FK_1n_YoAseguro_Persona",
                        column: x => x.CodYoAseguro,
                        principalTable: "TYoAseguro",
                        principalColumn: "CodYoAseguro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TExpositor",
                columns: table => new
                {
                    CodPersona = table.Column<string>(maxLength: 50, nullable: false),
                    CodCurso = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Tipo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TExpositor", x => new { x.CodPersona, x.CodCurso });
                    table.ForeignKey(
                        name: "FK_TExpositor_TCurso_CodCurso",
                        column: x => x.CodCurso,
                        principalTable: "TCurso",
                        principalColumn: "CodCurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TParticipantes",
                columns: table => new
                {
                    CodPersona = table.Column<string>(maxLength: 50, nullable: false),
                    CodCurso = table.Column<string>(maxLength: 50, nullable: false),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    Nota = table.Column<decimal>(nullable: true),
                    Evaluado = table.Column<bool>(nullable: false),
                    Intentos = table.Column<int>(nullable: false),
                    Tipo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TParticipantes", x => new { x.CodPersona, x.CodCurso });
                    table.ForeignKey(
                        name: "FK_TParticipantes_TCurso_CodCurso",
                        column: x => x.CodCurso,
                        principalTable: "TCurso",
                        principalColumn: "CodCurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPreguntas",
                columns: table => new
                {
                    CodPregunta = table.Column<int>(maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodCurso = table.Column<string>(maxLength: 50, nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Puntaje = table.Column<double>(nullable: false),
                    Respuesta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPreguntas", x => x.CodPregunta);
                    table.ForeignKey(
                        name: "FK_TPreguntas_TCurso_CodCurso",
                        column: x => x.CodCurso,
                        principalTable: "TCurso",
                        principalColumn: "CodCurso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TAlternativas",
                columns: table => new
                {
                    CodAlternativa = table.Column<int>(maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(nullable: true),
                    Creado = table.Column<DateTime>(nullable: false),
                    ModificadoPor = table.Column<string>(nullable: true),
                    Modificado = table.Column<DateTime>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CodPregunta = table.Column<int>(maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAlternativas", x => x.CodAlternativa);
                    table.ForeignKey(
                        name: "FK_TAlternativas_TPreguntas_CodPregunta",
                        column: x => x.CodPregunta,
                        principalTable: "TPreguntas",
                        principalColumn: "CodPregunta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAcceso_CodPadre",
                table: "TAcceso",
                column: "CodPadre");

            migrationBuilder.CreateIndex(
                name: "IX_TAfectadoComunidad_CodIncidente",
                table: "TAfectadoComunidad",
                column: "CodIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TAfectadoMedioAmbiente_CodIncidente",
                table: "TAfectadoMedioAmbiente",
                column: "CodIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TAfectadoPropiedad_CodIncidente",
                table: "TAfectadoPropiedad",
                column: "CodIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TAlternativas_CodPregunta",
                table: "TAlternativas",
                column: "CodPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_TAnalisisCausa_CodPadre",
                table: "TAnalisisCausa",
                column: "CodPadre");

            migrationBuilder.CreateIndex(
                name: "IX_TAuditoriaTregNoConfoObserva_CodNoConformidad",
                table: "TAuditoriaTregNoConfoObserva",
                column: "CodNoConformidad");

            migrationBuilder.CreateIndex(
                name: "IX_TCartillaDetalle_CodCC",
                table: "TCartillaDetalle",
                column: "CodCC");

            migrationBuilder.CreateIndex(
                name: "IX_TCartillaDetalle_CodCartilla",
                table: "TCartillaDetalle",
                column: "CodCartilla");

            migrationBuilder.CreateIndex(
                name: "IX_TCurso_RecurrenceID",
                table: "TCurso",
                column: "RecurrenceID");

            migrationBuilder.CreateIndex(
                name: "IX_TDetalleAfectado_CodIncidente",
                table: "TDetalleAfectado",
                column: "CodIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TDetalleInspeccion_CodInspeccion",
                table: "TDetalleInspeccion",
                column: "CodInspeccion");

            migrationBuilder.CreateIndex(
                name: "IX_TDiasPerdidosAfectado_CodIncidente",
                table: "TDiasPerdidosAfectado",
                column: "CodIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TEquipoAuditor_CodAuditoria",
                table: "TEquipoAuditor",
                column: "CodAuditoria");

            migrationBuilder.CreateIndex(
                name: "IX_TEquipoInvestigacion_CodIncidente",
                table: "TEquipoInvestigacion",
                column: "CodIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TExpositor_CodCurso",
                table: "TExpositor",
                column: "CodCurso");

            migrationBuilder.CreateIndex(
                name: "IX_TGestionRiesgo_CodGestionRiesgoPadre",
                table: "TGestionRiesgo",
                column: "CodGestionRiesgoPadre");

            migrationBuilder.CreateIndex(
                name: "IX_THallazgos_CodAuditoria",
                table: "THallazgos",
                column: "CodAuditoria");

            migrationBuilder.CreateIndex(
                name: "IX_THistorialAprobacion_CodAprobacion",
                table: "THistorialAprobacion",
                column: "CodAprobacion");

            migrationBuilder.CreateIndex(
                name: "IX_TIcam_CodIncidente",
                table: "TIcam",
                column: "CodIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TInspeccionAnalisisCausa_CodInspeccion",
                table: "TInspeccionAnalisisCausa",
                column: "CodInspeccion");

            migrationBuilder.CreateIndex(
                name: "IX_TJerarquia_CodPosicionPadre",
                table: "TJerarquia",
                column: "CodPosicionPadre");

            migrationBuilder.CreateIndex(
                name: "IX_TModulo_CodModuloPadre",
                table: "TModulo",
                column: "CodModuloPadre");

            migrationBuilder.CreateIndex(
                name: "IX_TParticipantes_CodCurso",
                table: "TParticipantes",
                column: "CodCurso");

            migrationBuilder.CreateIndex(
                name: "IX_TPersona_CodPais",
                table: "TPersona",
                column: "CodPais");

            migrationBuilder.CreateIndex(
                name: "IX_TPersona_CodProveedor",
                table: "TPersona",
                column: "CodProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_TPreguntas_CodCurso",
                table: "TPreguntas",
                column: "CodCurso");

            migrationBuilder.CreateIndex(
                name: "IX_TRolAcceso_CodAcceso",
                table: "TRolAcceso",
                column: "CodAcceso");

            migrationBuilder.CreateIndex(
                name: "IX_TTipoIncidente_CodPadreTipoIncidente",
                table: "TTipoIncidente",
                column: "CodPadreTipoIncidente");

            migrationBuilder.CreateIndex(
                name: "IX_TUbicacion_CodUbicacionPadre",
                table: "TUbicacion",
                column: "CodUbicacionPadre");

            migrationBuilder.CreateIndex(
                name: "IX_TUsuarioRol_CodUsuario",
                table: "TUsuarioRol",
                column: "CodUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAcreditacionCurso");

            migrationBuilder.DropTable(
                name: "TAfectadoComunidad");

            migrationBuilder.DropTable(
                name: "TAfectadoMedioAmbiente");

            migrationBuilder.DropTable(
                name: "TAfectadoPropiedad");

            migrationBuilder.DropTable(
                name: "TAgenda");

            migrationBuilder.DropTable(
                name: "TAlternativas");

            migrationBuilder.DropTable(
                name: "TAnalisisCausa");

            migrationBuilder.DropTable(
                name: "TAnalisisHallazgo");

            migrationBuilder.DropTable(
                name: "TAprobacionPlan");

            migrationBuilder.DropTable(
                name: "TAprobacionPlanHistorial");

            migrationBuilder.DropTable(
                name: "TAsistentesReunion");

            migrationBuilder.DropTable(
                name: "TAudCCCriterio");

            migrationBuilder.DropTable(
                name: "TAuditoriaAnalisisCausalidad");

            migrationBuilder.DropTable(
                name: "TAuditoriaTregNoConfoObserva");

            migrationBuilder.DropTable(
                name: "TAusentesReunion");

            migrationBuilder.DropTable(
                name: "TCargo");

            migrationBuilder.DropTable(
                name: "TCartillaDetalle");

            migrationBuilder.DropTable(
                name: "TCriterio");

            migrationBuilder.DropTable(
                name: "TDatosHallazgo");

            migrationBuilder.DropTable(
                name: "TDetalleAfectado");

            migrationBuilder.DropTable(
                name: "TDetalleInspeccion");

            migrationBuilder.DropTable(
                name: "TDiasPerdidosAfectado");

            migrationBuilder.DropTable(
                name: "TEquipoAuditor");

            migrationBuilder.DropTable(
                name: "TEquipoInspeccion");

            migrationBuilder.DropTable(
                name: "TEquipoInvestigacion");

            migrationBuilder.DropTable(
                name: "TEquipoSimulacro");

            migrationBuilder.DropTable(
                name: "TExpositor");

            migrationBuilder.DropTable(
                name: "TFile");

            migrationBuilder.DropTable(
                name: "TGestionRiesgo");

            migrationBuilder.DropTable(
                name: "THallazgos");

            migrationBuilder.DropTable(
                name: "THistorialAprobacion");

            migrationBuilder.DropTable(
                name: "TIcam");

            migrationBuilder.DropTable(
                name: "TIncidenteAnalisisCausa");

            migrationBuilder.DropTable(
                name: "TInspeccionAnalisisCausa");

            migrationBuilder.DropTable(
                name: "TInvestigaAfectado");

            migrationBuilder.DropTable(
                name: "TJerarquiaPersona");

            migrationBuilder.DropTable(
                name: "TJerarquiaResponsable");

            migrationBuilder.DropTable(
                name: "TJustificadosReunion");

            migrationBuilder.DropTable(
                name: "TLevantamientoPlan");

            migrationBuilder.DropTable(
                name: "TListaParticipantesComite");

            migrationBuilder.DropTable(
                name: "TMaestro");

            migrationBuilder.DropTable(
                name: "TModulo");

            migrationBuilder.DropTable(
                name: "TObservacionComportamientos");

            migrationBuilder.DropTable(
                name: "TObservacionCondiciones");

            migrationBuilder.DropTable(
                name: "TObservaciones");

            migrationBuilder.DropTable(
                name: "TObservacionIteracciones");

            migrationBuilder.DropTable(
                name: "TObservacionSimulacro");

            migrationBuilder.DropTable(
                name: "TObservacionTareas");

            migrationBuilder.DropTable(
                name: "TObservacionVerControlCritico");

            migrationBuilder.DropTable(
                name: "TObsISRegistroEncuestas");

            migrationBuilder.DropTable(
                name: "TObsTaComentarios");

            migrationBuilder.DropTable(
                name: "TObsTaEtapaTareas");

            migrationBuilder.DropTable(
                name: "TObsTaPersonaObservadas");

            migrationBuilder.DropTable(
                name: "TObsTaRegistroEncuestas");

            migrationBuilder.DropTable(
                name: "TObsVCCCierreIteraccion");

            migrationBuilder.DropTable(
                name: "TObsVCCHerramienta");

            migrationBuilder.DropTable(
                name: "TObsVCCRespuesta");

            migrationBuilder.DropTable(
                name: "TObsVCCVerCCEfectividad");

            migrationBuilder.DropTable(
                name: "TParticipantes");

            migrationBuilder.DropTable(
                name: "TPeligro");

            migrationBuilder.DropTable(
                name: "TPersona");

            migrationBuilder.DropTable(
                name: "TPersonaAtendida");

            migrationBuilder.DropTable(
                name: "TPersonaTolerancia");

            migrationBuilder.DropTable(
                name: "TPersonaYoAseguro");

            migrationBuilder.DropTable(
                name: "TPlanAnual");

            migrationBuilder.DropTable(
                name: "TPlanAnualGeneral");

            migrationBuilder.DropTable(
                name: "TPlanAnualVerConCri");

            migrationBuilder.DropTable(
                name: "TPlanTema");

            migrationBuilder.DropTable(
                name: "TProceso");

            migrationBuilder.DropTable(
                name: "TRegEncuestaSimulacro");

            migrationBuilder.DropTable(
                name: "TRegTolDetalle");

            migrationBuilder.DropTable(
                name: "TReportePF");

            migrationBuilder.DropTable(
                name: "TReportePFDetalle");

            migrationBuilder.DropTable(
                name: "TResponsable");

            migrationBuilder.DropTable(
                name: "TRespuestaParticipante");

            migrationBuilder.DropTable(
                name: "TRiesgo");

            migrationBuilder.DropTable(
                name: "TRolAcceso");

            migrationBuilder.DropTable(
                name: "TSecuenciaEvento");

            migrationBuilder.DropTable(
                name: "TTemaCapEspecifico");

            migrationBuilder.DropTable(
                name: "TTestigoInvolucrado");

            migrationBuilder.DropTable(
                name: "TTipoIncidente");

            migrationBuilder.DropTable(
                name: "TToleranciaCeroAnalisisCausa");

            migrationBuilder.DropTable(
                name: "TUbicacion");

            migrationBuilder.DropTable(
                name: "TUsuarioRol");

            migrationBuilder.DropTable(
                name: "TValidadorArchivo");

            migrationBuilder.DropTable(
                name: "TVerificacionControlCritico");

            migrationBuilder.DropTable(
                name: "TVerificacionControlCriticoCartilla");

            migrationBuilder.DropTable(
                name: "TVerificaciones");

            migrationBuilder.DropTable(
                name: "TVerificacionIPERC");

            migrationBuilder.DropTable(
                name: "TVerificacionPTAR");

            migrationBuilder.DropTable(
                name: "TYoAseguroLugar");

            migrationBuilder.DropTable(
                name: "TPreguntas");

            migrationBuilder.DropTable(
                name: "TAudCartilla");

            migrationBuilder.DropTable(
                name: "TCartilla");

            migrationBuilder.DropTable(
                name: "TControlCritico");

            migrationBuilder.DropTable(
                name: "TAuditoria");

            migrationBuilder.DropTable(
                name: "TAprobacion");

            migrationBuilder.DropTable(
                name: "TJerarquia");

            migrationBuilder.DropTable(
                name: "TReunion");

            migrationBuilder.DropTable(
                name: "TComite");

            migrationBuilder.DropTable(
                name: "TPais");

            migrationBuilder.DropTable(
                name: "TProveedor");

            migrationBuilder.DropTable(
                name: "TInspeccion");

            migrationBuilder.DropTable(
                name: "TYoAseguro");

            migrationBuilder.DropTable(
                name: "TSimulacro");

            migrationBuilder.DropTable(
                name: "TAccion");

            migrationBuilder.DropTable(
                name: "TAcceso");

            migrationBuilder.DropTable(
                name: "TTemaCapacitacion");

            migrationBuilder.DropTable(
                name: "TIncidente");

            migrationBuilder.DropTable(
                name: "ToleranciaCero");

            migrationBuilder.DropTable(
                name: "TRol");

            migrationBuilder.DropTable(
                name: "TUsuario");

            migrationBuilder.DropTable(
                name: "TCurso");

            migrationBuilder.DropTable(
                name: "TCursoRules");
        }
    }
}
