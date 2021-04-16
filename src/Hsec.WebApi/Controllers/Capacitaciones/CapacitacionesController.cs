using Hsec.Application.Common.EntityDTO;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Create.VMs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Delete;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update;
using Hsec.Application.Capacitaciones.CursoProgramacion.Command.Update.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacion.Queries.VMs;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Create.DTOs;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Delete;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Update;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Command.Update.VMs;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries;
using Hsec.Application.Capacitaciones.CursoProgramacionRule.Queries.VMs;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Command.Create;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Command.Create.VMs;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries;
using Hsec.Application.Capacitaciones.Cursos_Acreditacion.Queries.VMs;
using Hsec.Application.Capacitaciones.CursosFuturos.Queries;
using Hsec.Application.Capacitaciones.CursosFuturos.Queries.VMs;
using Hsec.Application.Capacitaciones.Expositores.Command.Delete;
using Hsec.Application.Capacitaciones.Expositores.Command.Update;
using Hsec.Application.Capacitaciones.Expositores.Command.Update.VMs;
using Hsec.Application.Capacitaciones.Inscripcion_Personas.Command.Create;
using Hsec.Application.Capacitaciones.Inscripcion_Personas.Command.Create.VMs;
using Hsec.Application.Capacitaciones.Inscripcion_Personas.Command.Delete;
using Hsec.Application.Capacitaciones.Inscripcion_Personas.Command.Delete.VMs;
using Hsec.Application.Capacitaciones.Participantes.Command.Update;
using Hsec.Application.Capacitaciones.Participantes.Command.Update.VMs;
using Hsec.Application.Capacitaciones.Participantes.Queries.GetFileReport;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.CreateCurso;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.CreateCurso.VMs;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.ExtraerPopUp;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Command.ExtraerPopUp.VMs;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries;
using Hsec.Application.Capacitaciones.PerfilCapacitacionPosicion.Queries.VMs;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Create;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Create.VMs;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Delete;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Update;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Command.Update.VMs;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetAll;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetAll.VMs;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetCod;
using Hsec.Application.Capacitaciones.PlanTemaCapacitacion.Queries.GetCod.VMs;
using Hsec.Application.Capacitaciones.Preguntas.Command.Update;
using Hsec.Application.Capacitaciones.Preguntas.Command.Update.VMs;
using Hsec.Application.Capacitaciones.Preguntas.Queries.Get;
using Hsec.Application.Capacitaciones.Preguntas.Queries.Get.VMs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.DTOs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Disponibles.Queries.VMs;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries;
using Hsec.Application.Capacitaciones.Preguntas_Cursos_Inscritos.Queries.VMs;
using Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries;
using Hsec.Application.Capacitaciones.Preguntas_DashBoard.Queries.VMs;
using Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command;
using Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Command.VM;
using Hsec.Application.Capacitaciones.Preguntas_Evaluacion.Queries;
using Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries;
using Hsec.Application.Capacitaciones.Preguntas_Mis_Capacitaciones.Queries.VMs;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Create.VMs;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Delete;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Command.Update.VMs;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetFiltrado.VMs;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetPeligroRiesgo;
using Hsec.Application.Capacitaciones.TemasCapacitacion.Queries.GetPeligroRiesgo.VMs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hsec.WebApi.Controllers.Capacitaciones
{
    public class CapacitacionesController : ApiController
    {
        [Route("Tema/Create")]
        [HttpPost]
        public async Task<Unit> CreateTemaCapacitacion(CreateTemaCapacitacionRequestVM request)
        {
            return await Mediator.Send(new CreateTemaCapacitacionCommand()
            {
                CreateVM = request
            });
        }

        [Route("Tema/Filtrado")] //Corregido
        [HttpPost]
        public async Task<GetFiltradoResponseCabVM> GetTemaCapacitacionFiltrado(GetFiltradoRequestVM temaCapacitacionVM)
        {
            return await Mediator.Send(new GetFiltradoQuery() { temaCapacitacionVM = temaCapacitacionVM });
        }        
        
        [Route("Tema/Update")] // YA SE CORRIGIO DICHO SERVICIO !! YA PUEDE ACTUALIZAR Y AGREGAR PELIGROS Y RIESGOS !!
        [HttpPost]
        public async Task<Unit> UpdateTemaCapacitacion(UpdateTemaCapacitacionRequestVM request)
        {
            return await Mediator.Send(new UpdateTemaCapacitacionCommand() {
                VM = request
            });
        }

        [Route("PlanTema/Create")]
        [HttpPost]
        public async Task<Unit> CreatePlanTemaCapacitacion(CreatePlanTemaRequestVM request)
        {
            return await Mediator.Send(new CreatePlanTemaCommand()
            {                
                VM = request
            });
        }

        [Route("PlanTema/Delete/{codReferencia}/{codTemaCapacita}")]
        [HttpGet]
        public async Task<Unit> DeletePlanTemaCapacitacion(string codReferencia, string codTemaCapacita)
        {
            return await Mediator.Send(new DeletePlanTemaCommand()
            {
                codReferencia = codReferencia,
                codTemaCapacita = codTemaCapacita
                
            });
        }

        [Route("PlanTema/Update")]
        [HttpPost]
        public async Task<Unit> UpdatePlanTemaCapacitacion(UpdatePlanTemaRequestVM request)
        {
            return await Mediator.Send(new UpdatePlanTemaCommand()
            {
                VM = request
            });
        }

        [Route("PlanTema/GetAll")]
        [HttpGet]
        public async Task<GetPlanTemaAllVM> GetAllPlanTemaCapacitacion()
        {
            return await Mediator.Send(new GetPlanTemaAllQuery());
        }

        [Route("PlanTema/Get/{codReferencia}/{codTemaCapacita}")]
        [HttpGet]
        public async Task<GetPlanTemaVM> GetPlanTemaCapacitacion(string codReferencia, string codTemaCapacita)
        {
            return await Mediator.Send(new GetPlanTemaQuery(){ 
            codReferencia = codReferencia,
            codTemaCapacita = codTemaCapacita            
            });
        }

        [Route("PerfilCapacitacionPosicionPopPup/Extraer")]
        [HttpPost]
        public async Task<IList<ExtraerPopUpCursoPosicionResponseVM>> ExtraerPlanTemaCapacitacionPopUp(ExtraerPopUpCursoPosicionRequestVM request)
        {
            return await Mediator.Send(new ExtraerPopUpCursoPosicionCommand()
            {
                VM = request
            });
        }

        [Route("PerfilCapacitacionPosicion/Extraer")]
        [HttpPost]
        public async Task<IList<GetCursoPosicionResponseVM>> PerfilPosicionExtraer(IList<string> codsRef)
        {
            return await Mediator.Send(new GetCursoPosicionQuery()
            {
                codElipses = codsRef
            });
        }        

        [Route("PerfilCapacitacionPosicion/Curso/Create")]
        [HttpPost]
        public async Task<Unit> CreatePerfilPosicion(IList<CreateCursoPosicionVM> request)
        {
            return await Mediator.Send(new CreateCursoPosicionCommand()
            {
                VM = request
            });
        }

        [Route("PerfilCapacitacionPosicionYPersona/Curso/Delete/{codTemaCapacita}")]
        [HttpGet]
        public async Task<Unit> DeleteTemaCapacitacion(string codTemaCapacita)
        {
            return await Mediator.Send(new DeleteTemaCapacitacionCommand() {                                
                codTemaCapacita = codTemaCapacita
            });
        }

        [Route("PerfilCapacitacionPersona/CursoYCursoPosicion/Extraer/{codPersona}")]
        [HttpGet]
        public async Task<GetCursoYCursoPosicionVM> CursoYCursoPosicionCapacitacion(string codPersona)
        {
            return await Mediator.Send(new GetCursoYCursoPosicionQuery() { codPersona = codPersona });
        }

        [Route("PeligroRiesgo/Extraer")]
        [HttpGet]
        public async Task<IList<GetPeligroRiesgoVM>> PeligroRiesgoExtraer()
        {
            return await Mediator.Send(new GetPeligroRiesgoQuery());
        }

        [Route("Expositores/Update")]
        [HttpPost]
        public async Task<Unit> UpdateExpositor(UpdateExpositorVM request)
        {
            return await Mediator.Send(new UpdateExpositorCommand()
            {
                VM = request
            });
        }

        [Route("Expositores/Delete")]
        [HttpPost]
        public async Task<Unit> DeleteExpositor(DeleteExpositorCommand request)
        {
            return await Mediator.Send(new DeleteExpositorCommand()
            {
                codTemaCapacita = request.codTemaCapacita,
                codPersona = request.codPersona
            });
        }
               
        [Route("CursoProgramacion/ExtraerAll")]
        [HttpGet]
        public async Task<GetCursoProgramacionVM> CursoProgramacionExtraerAll()
        {
            return await Mediator.Send(new GetCursoProgramacionQuery());
        }
        
        [Route("CursoProgramacion/Create")] //codCurso // CAMPO ENLACE AÑADIDO
        [HttpPost]
        public async Task<CodCursoDTO> CreateCursoProgramacion(CreateCursoProgramacionDto request)
        {
            return await Mediator.Send(new CreateCursoProgramacionCommand()
            {
                VM = request
            });   
        }

        [Route("CursoProgramacion/Update")] // CAMPO ENLACE AÑADIDO
        [HttpPost]
        public async Task<Unit> UpdateCursoProgramacion(UpdateCursoProgramacionDto request)
        {
            return await Mediator.Send(new UpdateCursoProgramacionCommand()
            {
                DTO = request
            });
        }

        [Route("CursoProgramacion/Delete/{codTemaCapacita}")]
        [HttpGet]
        public async Task<Unit> DeleteCursoProgramacion(string codTemaCapacita)
        {
            return await Mediator.Send(new DeleteCursoProgramacionCommand()
            {
                codTemaCapacita = codTemaCapacita
            });
        }

        [Route("CursoProgramacionRule/Update")] //Arreglado
        [HttpPost]
        public async Task<Unit> UpdateCursoProgramacionRule(UpdateCursoProgramacionRuleVM request)
        {
            return await Mediator.Send(new UpdateCursoProgramacionRuleCommand()
            {
                VM = request
            });
        }

        [Route("CursoProgramacionRule/Delete/{codCursoRule}")]
        [HttpGet]
        public async Task<Unit> DeleteCursoProgramacionRule(string codCursoRule)
        {
            return await Mediator.Send(new DeleteCursoProgramacionRuleCommand()
            {
                recurrenceID = codCursoRule
            });
        }

        [Route("CursoProgramacionRule/ExtraerAll")] //Arreglado
        [HttpGet]
        public async Task<GetCursoProgramacionRuleResponseAllVM> ExtraerAllCursoProgramacionRule()
        {
            return await Mediator.Send(new GetCursoProgramacionRuleAllQuery());
        }

        [Route("CursoProgramacionRule/Filtro")]
        [HttpGet]
        public async Task<GetCursoProgramacionRuleResponseAllVM> FiltroExtraerCursoProgramacionRule(GetCursoProgramacionRuleFiltroRequestVM request)
        {
            return await Mediator.Send(new GetCursoProgramacionRulleFiltroQuery()
            {
                VM = request
            });
        }

        [Route("Participantes/Update")]
        [HttpPost]
        public async Task<Unit> ParticipantesUpdateCursoProgramacion(UpdateParticipantesAllVM request)
        {
            return await Mediator.Send(new UpdateParticipantesCommand()
            {
                VM = request
            });
        }

        [Route("CursoProgramacion/ExtraerPorFechas")]
        [HttpPost]
        public async Task<GetCursoProgramacionFechasVM> ExtraerPorFechasCursoProgramacion(GetCursoProgramacionFechasQuery request)
        {
            return await Mediator.Send(new GetCursoProgramacionFechasQuery()
            {
                fechaInicio = request.fechaInicio,
                fechaFin = request.fechaFin
            });
        }

        [Route("CursoProgramacionRule/Extraer/{recurrenceID}")] // Arreglado
        [HttpGet]
        public async Task<GetCursoProgramacionRuleVM> ExtraerCodCursoCursoProgramacionRule(string recurrenceID)
        {
            return await Mediator.Send(new GetCursoProgramacionRuleIDQuery()
            {
                recurrenceID = recurrenceID
            });
        }

        [Route("CursoProgramacion/Extraer/{codCurso}")]
        [HttpGet]
        public async Task<GetCursoProgramacionIDDto> ExtraerCodCursoCursoProgramacion(string codCurso)
        {
            return await Mediator.Send(new GetCursoProgramacionIDQuery()
            {
                codCurso = codCurso
            });
        }

        [Route("CursoProgramacion/ExtraerPorFechasPart")] //revisar
        [HttpPost]
        public async Task<GetCursoProgramacionFechasPartVM> ExtraerPorFechasCursoPartProgramacion(GetCursoProgramacionFechasPartQuery request)
        {
            return await Mediator.Send(new GetCursoProgramacionFechasPartQuery()
            {
                fechaInicio = request.fechaInicio,
                fechaFin = request.fechaFin
            });
        }

        [Route("CursoProgramacionRule/Create")] //Arreglado
        [HttpPost]
        public async Task<Unit> CreateCursoProgramacionRule(CreateCursoProgramacionRuleDto request)
        {
            return await Mediator.Send(new CreateCursoProgramacionRuleCommand()
            {
                VM = request
            });
        }

        // NUEVO MODULO APARTIR DE AQUÍ (EXAMENES VIRTUALES)

        //[Route("Pregunta/Create")]
        //[HttpPost]
        //public async Task<Unit> CreateCursoPregunta(CreatePreguntaVM request)
        //{
        //    return await Mediator.Send(new CreatePreguntaCommand()
        //    {
        //        VM = request
        //    });
        //}
        
        //[Route("Pregunta/Delete/{codPregunta}")] //SOLO EN EL CASO DE QUERER ELIMINAR UNA PREGUNTA ESPECIFICA ... TMB SE ELIMINAN SUS ALTERNATIVAS RELACIONADAS !!
        //[HttpGet]
        //public async Task<Unit> DeleteCursoPregunta(string codPregunta)
        //{
        //    return await Mediator.Send(new DeletePreguntaCommand()
        //    {
        //        codPregunta = codPregunta
        //    });
        //}

        [Route("Pregunta/GetAll/{codCurso}")] //traer todas las preguntas para un curso con rpta y sin rpta y tambien el numero de respuestas!!
        [HttpGet]
        public async Task<GetPreguntaVM> GetCursoPreguntaAll(string codCurso)
        {
            return await Mediator.Send(new GetPreguntaAllQuery()
            {
                codCurso = codCurso
            }    
                );
        }

        [Route("Pregunta/GetConRpta/{codCurso}")] //traer todas las preguntas para un curso (con rpta)
        [HttpGet]
        public async Task<GetPreguntaVM> GetCursoPreguntaConRpta(string codCurso)
        {
            return await Mediator.Send(new GetPreguntaConRptaQuery()
            {
                codCurso = codCurso
            });
        }

        [Route("Pregunta/GetSinRpta/{codCurso}")] //traer todas las preguntas para un curso (sin rpta)
        [HttpGet]
        public async Task<GetPreguntaVM> GetCursoPreguntaSinRpta(string codCurso)
        {
            return await Mediator.Send(new GetPreguntaSinRptaQuery()
            {
                codCurso = codCurso
            });
        }

        [Route("Pregunta/CreateUpdateDelete")]
        [HttpPost]
        public async Task<Unit> CreateUpdateDeleteCursoPregunta(UpdatePreguntaVM VM)
        {
            return await Mediator.Send(new UpdatePreguntaCommand()
            {
                VM = VM
            });
        }
        // hasta aca

        
        [Route("Dashboard/GetEstadoCursos/{codPersona}")] //LISTO!! // duracion
        [HttpGet]
        public async Task<GetDashboardVM> GetDashboardEstadoCursos(string codPersona)
        {
            return await Mediator.Send(new GetDashboardQuery()
            {
                codPersona = codPersona
            });
        }

        [Route("GetMisCapacitaciones/GetEstadoCursos/{codPersona}")] //LISTO!!
        [HttpGet]
        public async Task<GetMisCapacitacionesVM> GetMisCapacitacionesEstadoCursos(string codPersona)
        {
            return await Mediator.Send(new GetMisCapacitacionesQuery()
            {
                codPersona = codPersona
            });
        }

        [Route("GetMisCursosDisponibles/GetEstadoCursos/{codPersona}/{codTemaCapacita}")] // DOS PARAMETROS !! //LISTO!! // duracion // CAMPO ENLACE AÑADIDO
        [HttpGet]
        public async Task<GetCursosDisponiblesVM> GetMisCursosDisponibles(string codPersona, string codTemaCapacita)
        {
            return await Mediator.Send(new GetCursosDisponiblesQuery()
            {
                codPersona = codPersona,
                codTema = codTemaCapacita                
            });
        }

        [Route("GetMisCursosInscritos/GetEstadoCursos/{codPersona}")] //LISTO!! //duracion
        [HttpGet]
        public async Task<GetPreguntasCursosInscritosVM> GetMisCursosInscritos(string codPersona)
        {
            return await Mediator.Send(new GetPreguntasCursosInscritosQuery()
            {
                codPersona = codPersona
            });
        }

        [Route("Inscripcion/CreateUpdate")]  // YA FUNCIONA !!
        [HttpPost]
        public async Task<Unit> InscripcionCreate(CreateInscripcionVM VM)
        {
            return await Mediator.Send(new CreateInscripcionCommand()
            {
                codPersona = VM.codPersona,
                codCurso = VM.codCurso
            });
        }

        [Route("Inscripcion/Delete")]   // YA FUNCIONA !!
        [HttpPost]
        public async Task<Unit> InscripcionDelete(DeleteInscripcionVM VM)
        {
            return await Mediator.Send(new DeleteInscripcionCommand()
            {
                codPersona = VM.codPersona,
                codCurso = VM.codCurso
            });
        }

        [Route("CursosFuturos/Get/{codTemaCapacita}")] // YA FUNCIONA !!
        [HttpGet]
        public async Task<GetCursosFuturosVM> CursosFuturosGet(string codTemaCapacita)
        {
            return await Mediator.Send(new GetCursosFuturosQuery()
            {
                codTemaCapacita = codTemaCapacita
            });
        }

        [Route("Cursos/FiltroGeneral")] //Filtro para Gerson!
        [HttpPost]
        public async Task<GetCursoProgramacionFiltroGenVM> CursosFuturosGet(GetCursoProgramacionFiltroGenReqVM modelVM)
        {
            return await Mediator.Send(new GetCursoProgramacionFiltroGenQuery()
            {
                model = modelVM
            });
        }

        [Route("Cursos/EvaluacionPreguntasExamen")]
        [HttpPost]
        public async Task<PreguntasEvaluacionRespVM> EvaluacionPreguntasExamen(PreguntasEvaluacionVM modelVM)
        {
            return await Mediator.Send(new PreguntasEvaluacionCommand()
            {
                VM = modelVM
            });
        }
        [Route("Cursos/EvaluacionRespuestaExamen")]
        [HttpPost]
        public async Task<PreguntasEvaluacionRespVM> EvaluacionRespuestaExamen(PreguntaEvaluacionVM modelVM)
        {
            return await Mediator.Send(new UpdateNotaPreguntaCommand()
            {
                VM = modelVM
            });
        }

        [Route("CursosAcreditacion/FiltroAcreditacion")] //Segundo Filtro para Gerson!
        [HttpPost]
        public async Task<GetCursoAcreditacionRespVM> FiltroAcreditacion(GetCursoAcreditacionReqVM modelVM)
        {
            return await Mediator.Send(new GetCursoAcreditacionQuery()
            {
                VM = modelVM
            });
        }

        [Route("CursosAcreditacion/CreateUpdateTarjeta")]
        [HttpPost]
        public async Task<CreateCursoAcreditacionVM> CreateTarjeta(CreateCursoAcreditacionRequestVM modelVM)
        {
            return await Mediator.Send(new CreateCursoAcreditacionTarjetaCommand()
            {
                VM = modelVM
            });
        }

        [Route("CursosAcreditacion/CreateUpdateStiker")]
        [HttpPost]
        public async Task<CreateCursoAcreditacionVM> CreateStiker(CreateCursoAcreditacionRequestVM modelVM)
        {
            return await Mediator.Send(new CreateCursoAcreditacionStikerCommand()
            {
                VM = modelVM
            });
        }

        //Aumento de Servicio Adicional de Cursos Disponibles Sin Filtros (para Manu) // CAMPO ENLACE AÑADIDO

        [Route("GetMisCursosDisponibles/GetEstadoCursos/SinFiltro/{pagina}/{paginaTamanio}")] // DOS PARAMETROS !! //LISTO!! // duracion
        [HttpGet]
        public async Task<CollectionResponse<GetCursosDisponiblesDTO>> GetMisCursosDisponiblesSinFiltros(int pagina, int paginaTamanio)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var CodPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            return await Mediator.Send(new GetCursosDisponiblesSinFiltroQuery() { codPersona = CodPersona ,Pagina= pagina ,PaginaTamanio= paginaTamanio });
        }

        [Route("GetRespuestaParticipante/{CodPersona}/{CodCurso}")] // DOS PARAMETROS !! //LISTO!! // duracion
        [HttpGet]
        public async Task<IList<PreguntaRespuestaVM>> GetRespuestaParticipante(string CodPersona, string CodCurso)
        {            
            return await Mediator.Send(new GetRespuestaParticipanteQuery() { codCurso = CodCurso , codPersona = CodPersona});
        }

        [AllowAnonymous]
        [HttpGet("Participante/Certificado/{CodPersona}/{CodCurso}")]
        public async Task<ActionResult> GetCertificado(string CodPersona,string CodCurso)
        {
            var file = await Mediator.Send(new GetReporteFileQuery { CodPersona = CodPersona, CodCurso = CodCurso });

            var fileContent = new FileContentResult(file, System.Net.Mime.MediaTypeNames.Application.Octet);
            fileContent.FileDownloadName = "Certificado de Curso HSEC.pdf";
            return fileContent;
        }
    }
}