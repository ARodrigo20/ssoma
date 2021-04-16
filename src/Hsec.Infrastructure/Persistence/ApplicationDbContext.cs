using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Common;
using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Entities.YoAseguro;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Entities.Inspecciones;
using Hsec.Domain.Entities.Verficaciones;
using Hsec.Domain.Entities.VerficacionesCc;
using Hsec.Domain.Entities.Simulacros;
using Hsec.Domain.Entities.PlanAccion;
using Hsec.Domain.Entities.Capacitaciones;
using Hsec.Domain.Entities.Auditoria;
using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }

        /* Modulo General*/
        public DbSet<TPais> TPais { get; set; }
        public DbSet<TPersona> TPersona { get; set; }
        public DbSet<TProveedor> TProveedor { get; set; }
        public DbSet<TJerarquia> TJerarquia { get; set; }
        public DbSet<TAcceso> TAcceso { get; set; }
        public DbSet<TUbicacion> TUbicacion { get; set; }
        public DbSet<TJerarquiaPersona> TJerarquiaPersona { get; set; }
        public DbSet<TJerarquiaResponsable> TJerarquiaResponsable { get; set; }
        public DbSet<TAnalisisCausa> TAnalisisCausa { get; set; }
        public DbSet<TUsuario> TUsuario { get; set; }
        public DbSet<TMaestro> TMaestro { get; set; }
        public DbSet<TRol> TRol { get; set; }
        public DbSet<TRolAcceso> TRolAcceso { get; set; }
        public DbSet<TUsuarioRol> TUsuarioRol { get; set; }
        public DbSet<TTipoIncidente> TTipoIncidente { get; set; }
        public DbSet<TGestionRiesgo> TGestionRiesgo { get; set; }
        public DbSet<TControlCritico> TControlCritico { get; set; }
        public DbSet<TCriterio> TCriterio { get; set; }
        public DbSet<TCartilla> TCartilla { get; set; }
        public DbSet<TCartillaDetalle> TCartillaDetalle { get; set; }
        public DbSet<TModulo> TModulo { get; set; }
        public DbSet<TCargo> TCargo { get; set; }
        public DbSet<TAprobacion> TAprobacion { get; set; }
        public DbSet<THistorialAprobacion> THistorialAprobacion { get; set; }
        public DbSet<TProceso> TProceso { get; set; }
        public DbSet<TAprobacionPlan> TAprobacionPlan { get; set; }
        public DbSet<TAprobacionPlanHistorial> TAprobacionPlanHistorial { get; set; }
        public DbSet<TPlanAnual> TPlanAnual { get; set; }
        public DbSet<TPlanAnualVerConCri> TPlanAnualVerConCri { get; set; }
        public DbSet<TPlanAnualGeneral> TPlanAnualGeneral { get; set; }

        /* Modulo Observaciones */
        public DbSet<TObservacion> TObservaciones { get; set; }
        public DbSet<TObservacionComportamiento> TObservacionComportamientos { get; set; }
        public DbSet<TObservacionCondicion> TObservacionCondiciones { get; set; }
        public DbSet<TObservacionIteraccion> TObservacionIteracciones { get; set; }
        public DbSet<TObservacionTarea> TObservacionTareas { get; set; }
        public DbSet<TObsISRegistroEncuesta> TObsISRegistroEncuestas { get; set; }
        public DbSet<TObsTaPersonaObservada> TObsTaPersonaObservadas { get; set; }
        public DbSet<TObsTaComentario> TObsTaComentarios { get; set; }
        public DbSet<TObsTaEtapaTarea> TObsTaEtapaTareas { get; set; }
        public DbSet<TObsTaRegistroEncuesta> TObsTaRegistroEncuestas { get; set; }
        public DbSet<TObservacionVerControlCritico> TObservacionVerControlCritico { get; set; }
        public DbSet<TObsVCCCierreIteraccion> TObsVCCCierreIteraccion { get; set; }
        public DbSet<TObsVCCHerramienta> TObsVCCHerramienta { get; set; }
        public DbSet<TObsVCCRespuesta> TObsVCCRespuesta { get; set; }
        public DbSet<TObsVCCVerCCEfectividad> TObsVCCVerCCEfectividad { get; set; }
        public DbSet<TReportePF> TReportePF { get; set; }
        public DbSet<TReportePFDetalle> TReportePFDetalle { get; set; }

        /* Modulo Verificaciones */
        public DbSet<TVerificaciones> TVerificaciones { get; set; }
        public DbSet<TVerificacionIPERC> TVerificacionIPERC { get; set; }
        public DbSet<TVerificacionPTAR> TVerificacionPTAR { get; set; }

        /* Modulo VerificacionesCc */
        public DbSet<TVerificacionControlCritico> TVerificacionControlCritico { get; set; }
        public DbSet<TVerificacionControlCriticoCartilla> TVerificacionControlCriticoCartilla { get; set; }
        
        /* Modulo Simulacros */
        public DbSet<TSimulacro> TSimulacro { get; set; }
        public DbSet<TObservacionSimulacro> TObservacionSimulacro { get; set; }
        public DbSet<TEquipoSimulacro> TEquipoSimulacro { get; set; }
        public DbSet<TRegEncuestaSimulacro> TRegEncuestaSimulacro { get; set; }

        /* Modulo Incidentes */
        public DbSet<TAfectadoComunidad> TAfectadoComunidad { get; set; }
        public DbSet<TAfectadoMedioAmbiente> TAfectadoMedioAmbiente { get; set; }
        public DbSet<TAfectadoPropiedad> TAfectadoPropiedad { get; set; }
        public DbSet<TDetalleAfectado> TDetalleAfectado { get; set; }
        public DbSet<TDiasPerdidosAfectado> TDiasPerdidosAfectado { get; set; }
        public DbSet<TEquipoInvestigacion> TEquipoInvestigacion { get; set; }
        public DbSet<TIcam> TIcam { get; set; }
        public DbSet<TIncidente> TIncidente { get; set; }
        public DbSet<TIncidenteAnalisisCausa> TIncidenteAnalisisCausa { get; set; }
        public DbSet<TInvestigaAfectado> TInvestigaAfectado { get; set; }
        public DbSet<TSecuenciaEvento> TSecuenciaEvento { get; set; }
        public DbSet<TTestigoInvolucrado> TTestigoInvolucrado { get; set; }

        /* Modulo Inspecciones */
        public DbSet<TInspeccion> TInspeccion { get; set; }
        public DbSet<TDetalleInspeccion> TDetalleInspeccion { get; set; }
        public DbSet<TEquipoInspeccion> TEquipoInspeccion { get; set; }
        public DbSet<TPersonaAtendida> TPersonaAtendida { get; set; }
        public DbSet<TInspeccionAnalisisCausa> TInspeccionAnalisisCausa { get; set; }

        /* Modulo YoAseguro*/
        public DbSet<TYoAseguro> TYoAseguro { get; set; }
        public DbSet<TYoAseguroLugar> TYoAseguroLugar { get; set; }
        public DbSet<TPersonaYoAseguro> TPersonaYoAseguro { get; set; }

        /* Planes de Accion */
        public DbSet<TAccion> TAccion { get; set; }
        public DbSet<TResponsable> TResponsable { get; set; }
        public DbSet<TFile> TFile { get; set; }
        public DbSet<TLevantamientoPlan> TLevantamientoPlan { get; set; }
        public DbSet<TValidadorArchivo> TValidadorArchivo { get; set; }

        /* Capacitaciones */
        public DbSet<TCurso> TCurso { get; set; }
        public DbSet<TExpositor> TExpositor { get; set; }
        public DbSet<TParticipantes> TParticipantes { get; set; }
        public DbSet<TPeligro> TPeligro { get; set; }
        public DbSet<TPlanTema> TPlanTema { get; set; }
        public DbSet<TRiesgo> TRiesgo { get; set; }
        public DbSet<TTemaCapacitacion> TTemaCapacitacion { get; set; }
        public DbSet<TTemaCapEspecifico> TTemaCapEspecifico { get; set; }
        public DbSet<TCursoRules> TCursoRules { get; set; }
        public DbSet<TPreguntas> TPreguntas { get; set; }
        public DbSet<TAlternativas> TAlternativas { get; set; }
        public DbSet<TRespuestaParticipante> TRespuestaParticipante { get; set; }
        public DbSet<TAcreditacionCurso> TAcreditacionCurso { get; set; }

        /* Auditoria */
        public DbSet<TAnalisisHallazgo> TAnalisisHallazgo { get; set; }
        public DbSet<TAuditoria> TAuditoria { get; set; }
        public DbSet<TAuditoriaAnalisisCausalidad> TAuditoriaAnalisisCausalidad { get; set; }
        public DbSet<TAuditoriaTregNoConfoObserva> TAuditoriaTregNoConfoObserva { get; set; }
        public DbSet<TDatosHallazgo> TDatosHallazgo { get; set; }
        public DbSet<TEquipoAuditor> TEquipoAuditor { get; set; }
        public DbSet<THallazgos> THallazgos { get; set; }
        public DbSet<TAudCartilla> TAudCartilla { get; set; }
        public DbSet<TAudCCCriterio> TAudCCCriterio { get; set; }

        /* Otros */
        public DbSet<TToleranciaCero> ToleranciaCero { get; set; }
        public DbSet<TRegTolDetalle> TRegTolDetalle { get; set; }
        public DbSet<TPersonaTolerancia> TPersonaTolerancia { get; set; }
        public DbSet<TToleranciaCeroAnalisisCausa> TToleranciaCeroAnalisisCausa { get; set; }
        public DbSet<TReunion> TReunion { get; set; }
        public DbSet<TAsistentesReunion> TAsistentesReunion { get; set; }
        public DbSet<TAusentesReunion> TAusentesReunion { get; set; }
        public DbSet<TJustificadosReunion> TJustificadosReunion { get; set; }
        public DbSet<TAgenda> TAgenda { get; set; }
        public DbSet<TComite> TComite { get; set; }
        public DbSet<TListaParticipantesComite> TListaParticipantesComite { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreadoPor = "admin";
                        entry.Entity.Creado = _dateTime.Now;
                        entry.Entity.Estado = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModificadoPor = "admin";
                        entry.Entity.Modificado = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
