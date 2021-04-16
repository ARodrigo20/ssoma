using Hsec.Domain.Entities.Observaciones;
using Hsec.Domain.Entities.YoAseguro;
using Hsec.Domain.Entities.General;
using Hsec.Domain.Entities.Incidentes;
using Hsec.Domain.Entities.Inspecciones;
using Hsec.Domain.Entities.Simulacros;
using Hsec.Domain.Entities.Verficaciones;
using Hsec.Domain.Entities.VerficacionesCc;
using Hsec.Domain.Entities.PlanAccion;
using Hsec.Domain.Entities.Capacitaciones;
using Hsec.Domain.Entities.Auditoria;
using Hsec.Domain.Entities.Otros;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        /*Modulo General*/
        DbSet<TPais> TPais { get; set; }
        DbSet<TPersona> TPersona { get; set; }
        DbSet<TProveedor> TProveedor { get; set; }
        DbSet<TAcceso> TAcceso { get; set; } //por cada dbset es un tabla
        DbSet<TJerarquia> TJerarquia { get; set; } //por cada dbset es un tabla
        DbSet<TUbicacion> TUbicacion { get; set; } //por cada dbset es un tabla
        DbSet<TJerarquiaPersona> TJerarquiaPersona { get; set; } //por cada dbset es un tabla
        DbSet<TJerarquiaResponsable> TJerarquiaResponsable { get; set; }
        DbSet<TAnalisisCausa> TAnalisisCausa { get; set; }
        DbSet<TUsuario> TUsuario { get; set; }
        DbSet<TMaestro> TMaestro { get; set; }
        DbSet<TRol> TRol { get; set; }
        DbSet<TRolAcceso> TRolAcceso { get; set; }
        DbSet<TUsuarioRol> TUsuarioRol { get; set; }
        DbSet<TTipoIncidente> TTipoIncidente { get; set; }
        DbSet<TGestionRiesgo> TGestionRiesgo { get; set; }
        DbSet<TControlCritico> TControlCritico { get; set; }
        DbSet<TCriterio> TCriterio { get; set; }
        DbSet<TCartilla> TCartilla { get; set; }
        DbSet<TCartillaDetalle> TCartillaDetalle { get; set; }
        DbSet<TModulo> TModulo { get; set; }
        DbSet<TCargo> TCargo { get; set; }
        DbSet<TAprobacion> TAprobacion { get; set; }
        DbSet<THistorialAprobacion> THistorialAprobacion { get; set; }
        DbSet<TProceso> TProceso { get; set; }
        DbSet<TAprobacionPlan> TAprobacionPlan { get; set; }
        DbSet<TAprobacionPlanHistorial> TAprobacionPlanHistorial { get; set; }
        DbSet<TPlanAnual> TPlanAnual { get; set; }
        DbSet<TPlanAnualVerConCri> TPlanAnualVerConCri { get; set; }
        DbSet<TPlanAnualGeneral> TPlanAnualGeneral { get; set; }

        /* Modulo Observaciones */

        DbSet<TObservacion> TObservaciones { get; set; }
        DbSet<TObservacionComportamiento> TObservacionComportamientos { get; set; }
        DbSet<TObservacionCondicion> TObservacionCondiciones { get; set; }
        DbSet<TObservacionIteraccion> TObservacionIteracciones { get; set; }
        DbSet<TObservacionTarea> TObservacionTareas { get; set; }
        DbSet<TObsISRegistroEncuesta> TObsISRegistroEncuestas { get; set; }
        DbSet<TObsTaPersonaObservada> TObsTaPersonaObservadas { get; set; }
        DbSet<TObsTaComentario> TObsTaComentarios { get; set; }
        DbSet<TObsTaEtapaTarea> TObsTaEtapaTareas { get; set; }
        DbSet<TObsTaRegistroEncuesta> TObsTaRegistroEncuestas { get; set; }
        DbSet<TObservacionVerControlCritico> TObservacionVerControlCritico { get; set; }
        DbSet<TObsVCCCierreIteraccion> TObsVCCCierreIteraccion { get; set; }
        DbSet<TObsVCCHerramienta> TObsVCCHerramienta { get; set; }
        DbSet<TObsVCCRespuesta> TObsVCCRespuesta { get; set; }
        DbSet<TObsVCCVerCCEfectividad> TObsVCCVerCCEfectividad { get; set; }
        DbSet<TReportePF> TReportePF { get; set; }
        DbSet<TReportePFDetalle> TReportePFDetalle { get; set; }

        /* Modulo Verificaciones */
        DbSet<TVerificaciones> TVerificaciones { get; set; }
        DbSet<TVerificacionIPERC> TVerificacionIPERC { get; set; }
        DbSet<TVerificacionPTAR> TVerificacionPTAR { get; set; }

        /* Modulo Verificaciones Control Critico */
        DbSet<TVerificacionControlCritico> TVerificacionControlCritico { get; set; }
        DbSet<TVerificacionControlCriticoCartilla> TVerificacionControlCriticoCartilla { get; set; }
        
        /* Modulo Simulacros */
        DbSet<TSimulacro> TSimulacro { get; set; }
        DbSet<TObservacionSimulacro> TObservacionSimulacro { get; set; }
        DbSet<TEquipoSimulacro> TEquipoSimulacro { get; set; }
        DbSet<TRegEncuestaSimulacro> TRegEncuestaSimulacro { get; set; }

        /* Modulo Incidentes */
        DbSet<TAfectadoComunidad> TAfectadoComunidad { get; set; }
        DbSet<TAfectadoMedioAmbiente> TAfectadoMedioAmbiente { get; set; }
        DbSet<TAfectadoPropiedad> TAfectadoPropiedad { get; set; }
        DbSet<TDetalleAfectado> TDetalleAfectado { get; set; }
        DbSet<TDiasPerdidosAfectado> TDiasPerdidosAfectado { get; set; }
        DbSet<TEquipoInvestigacion> TEquipoInvestigacion { get; set; }
        DbSet<TIcam> TIcam { get; set; }
        DbSet<TIncidente> TIncidente { get; set; }
        DbSet<TIncidenteAnalisisCausa> TIncidenteAnalisisCausa { get; set; }
        DbSet<TInvestigaAfectado> TInvestigaAfectado { get; set; }
        DbSet<TSecuenciaEvento> TSecuenciaEvento { get; set; }
        DbSet<TTestigoInvolucrado> TTestigoInvolucrado { get; set; }


        /* Inspecciones */
        DbSet<TInspeccion> TInspeccion { get; set; }
        DbSet<TDetalleInspeccion> TDetalleInspeccion { get; set; }
        DbSet<TEquipoInspeccion> TEquipoInspeccion { get; set; }
        DbSet<TPersonaAtendida> TPersonaAtendida { get; set; }
        DbSet<TInspeccionAnalisisCausa> TInspeccionAnalisisCausa { get; set; }

        /* Modulo Yo Aseguro */
        DbSet<TYoAseguro> TYoAseguro { get; set; }
        DbSet<TYoAseguroLugar> TYoAseguroLugar { get; set; }
        DbSet<TPersonaYoAseguro> TPersonaYoAseguro { get; set; }

        /* Planes de Accion */
        DbSet<TAccion> TAccion { get; set; }
        DbSet<TResponsable> TResponsable { get; set; }
        DbSet<TFile> TFile { get; set; }
        DbSet<TLevantamientoPlan> TLevantamientoPlan { get; set; }
        DbSet<TValidadorArchivo> TValidadorArchivo { get; set; }

        /* Capacitaciones */
        DbSet<TCurso> TCurso { get; set; }
        DbSet<TExpositor> TExpositor { get; set; }
        DbSet<TParticipantes> TParticipantes { get; set; }
        DbSet<TPeligro> TPeligro { get; set; }
        DbSet<TPlanTema> TPlanTema { get; set; }
        DbSet<TRiesgo> TRiesgo { get; set; }
        DbSet<TTemaCapacitacion> TTemaCapacitacion { get; set; }
        DbSet<TTemaCapEspecifico> TTemaCapEspecifico { get; set; }
        DbSet<TCursoRules> TCursoRules { get; set; }
        DbSet<TPreguntas> TPreguntas { get; set; }
        DbSet<TAlternativas> TAlternativas { get; set; }
        DbSet<TRespuestaParticipante> TRespuestaParticipante { get; set; }
        DbSet<TAcreditacionCurso> TAcreditacionCurso { get; set; }

        /*Auditoira*/
        DbSet<TAnalisisHallazgo> TAnalisisHallazgo { get; set; }
        DbSet<TAuditoria> TAuditoria { get; set; }
        DbSet<TAuditoriaAnalisisCausalidad> TAuditoriaAnalisisCausalidad { get; set; }
        DbSet<TAuditoriaTregNoConfoObserva> TAuditoriaTregNoConfoObserva { get; set; }
        DbSet<TDatosHallazgo> TDatosHallazgo { get; set; }
        DbSet<TEquipoAuditor> TEquipoAuditor { get; set; }
        DbSet<THallazgos> THallazgos { get; set; }
        DbSet<TAudCartilla> TAudCartilla { get; set; }
        DbSet<TAudCCCriterio> TAudCCCriterio { get; set; }

        /* Otros */
        DbSet<TToleranciaCero> ToleranciaCero { get; set; }
        DbSet<TRegTolDetalle> TRegTolDetalle { get; set; }
        DbSet<TPersonaTolerancia> TPersonaTolerancia { get; set; }
        DbSet<TToleranciaCeroAnalisisCausa> TToleranciaCeroAnalisisCausa { get; set; }
        DbSet<TReunion> TReunion { get; set; }
        DbSet<TAsistentesReunion> TAsistentesReunion { get; set; }
        DbSet<TAusentesReunion> TAusentesReunion { get; set; }
        DbSet<TJustificadosReunion> TJustificadosReunion { get; set; }
        DbSet<TAgenda> TAgenda { get; set; }
        DbSet<TComite> TComite { get; set; }
        DbSet<TListaParticipantesComite> TListaParticipantesComite { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        public Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }
    }
}
