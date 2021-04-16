using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Hsec.Application.General.Aprobaciones.Queries.GetProceso;
using Hsec.Application.General.Aprobaciones.Queries.GetListProceso;
using Hsec.Application.General.Aprobaciones.Queries.GetHistorialAprobacion;
using Hsec.Application.General.Aprobaciones.Queries.GetAprobacion;
using Hsec.Application.General.Aprobaciones.Commands.UpdateProceso;
using Hsec.Application.General.Aprobaciones.Commands.DeleteProceso;
using Hsec.Application.General.Aprobaciones.Commands.CreateProceso;
using Hsec.Application.General.Aprobaciones.Commands.Aprobacion;
using Hsec.Application.General.Aprobaciones.Commands.InciarAprobacion;
using Hsec.Application.General.Aprobaciones.Queries.GetDocAprobacion;
using Hsec.Application.General.Aprobaciones.Commands.UpdateCadenaAprobacion;
using Hsec.Application.General.Aprobaciones.Commands.SendNotificacion;
using Hsec.Application.General.Aprobaciones.Queries.GetAprobacionesTabla;
using System;
using System.Collections.Generic;
using Hsec.Application.General.Aprobaciones.Queries.GetEstadoDoc;
using Hsec.Application.General.Aprobaciones.Queries.GetListadoAprobacion;
using Hsec.Application.General.Aprobaciones.Commands.AprobacionPlan;
using Hsec.Application.General.Aprobaciones.Commands.AprobacionPlanCreate;
using Hsec.Application.General.Aprobaciones.Commands.AprobacionPlanDelete;
using Hsec.Application.General.Aprobaciones.Queries.AprobacionPlanGetQuery;

namespace Hsec.WebApi.Controllers.General
{    
    public class AprobacionController : ApiController
    {

        [Route("GetEstadoDoc/{Codigo}/{Tabla}")]
        [HttpGet]
        public async Task<ActionResult<string>> GetEstadoDoc(string Codigo,string Tabla)
        {
            return await Mediator.Send(new GetEstadoDocQuery() { docReferencia= Codigo ,codTabla=Tabla});
        }

        [Route("GetEstadoListDoc")]
        [HttpPost]
        public async Task<ActionResult<List<Tuple<string, string>>>> GetEstadoListDoc(GetEstadoListDocQuery values)
        {
            return await Mediator.Send(values);
        }

        [HttpGet("GetAprobaciones/{docReferencia}")]
        public async Task<ActionResult<bool>> GetAprobaciones(string docReferencia)
        {
            return await Mediator.Send(new GetListadoAprobacionQuery() { DocReferencia = docReferencia });
            //return await Mediator.Send(values);
        }

        // [Route("Create")]
        // [HttpPost]        
        // public async Task<ActionResult<long>> Create(CreateModuloCommand command)
        // {
        //     return await Mediator.Send(command);
        // }

        // [Route("Update/{id}")]
        // [HttpPost]
        // public async Task<ActionResult<Unit>> Update(int id, UpdateModuloCommand command)
        // {
        //     command.CodModulo = id.ToString();
        //     return await Mediator.Send(command);
        // }

        // [Route("Code2Name/{codigo}")]
        // [HttpGet]
        // public async Task<ActionResult<string>> Get(string codigo)
        // {
        //     return await Mediator.Send(new Code2NameQuery() { codigo = codigo });
        // }
        [Route("IniciarAprobacion")]
        [HttpPost]
        public async Task<ActionResult<int>> IniciarAprobacion(IniciarAprobacionCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("Aprobacion")]
        [HttpPost]
        public async Task<ActionResult<Unit>> Aprobacion(AprobacionCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("CreateProceso")]
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateProceso(CreateProcesoCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("DeleteProceso/{codProceso}")]
        [HttpGet]
        public async Task<ActionResult<Unit>> DeleteProceso(string codProceso)
        {
            return await Mediator.Send(new DeleteProcesoCommand(){CodProceso=codProceso});
        }

        [Route("UpdateProceso/{codProceso}")]
        [HttpPost]
        public async Task<ActionResult<Unit>> UpdateProceso(string codProceso,UpdateProcesoCommand command)
        {
            if(!codProceso.Equals(command.Data.CodProceso)) throw new System.Exception("codProceso != command.codProceso");
            command.Data.CodProceso = codProceso;
            return await Mediator.Send(command);
        }

        [Route("GetAprobacion/{docReferencia}")]
        [HttpGet]
        public async Task<ActionResult<GetAprobacionVM>> GetAprobacion(string docReferencia)
        {
            return await Mediator.Send(new GetAprobacionQuery(){DocReferencia=docReferencia});
        }
        [Route("GetAprobacion/Usuario")] //POrtlet
        [HttpGet]
        public async Task<ActionResult<GetDocAprobacionVM>> GetDocAprobacionQuery()
        {
            return await Mediator.Send(new GetDocAprobacionQuery());
        }

        [Route("GetHistorialAprobacion/{docReferencia}")]
        [HttpGet]
        public async Task<ActionResult<GetHistorialAprobacionVM>> GetHistorialAprobacion(string docReferencia)
        {
            return await Mediator.Send(new GetHistorialAprobacionQuery(){DocReferencia = docReferencia});
        }

        [Route("GetListProceso")]
        [HttpGet]
        public async Task<ActionResult<ListaProcesoVM>> GetListProceso()
        {
            return await Mediator.Send(new GetListProcesoQuery());
        }

        [Route("GetProceso/{codProceso}")]
        [HttpGet]
        public async Task<ActionResult<GetProcesoVM>> GetProceso(string codProceso)
        {
            return await Mediator.Send( new GetProcesoQuery(){CodProceso=codProceso} );
        }

        [Route("UpdateCadenaAprobacion")]
        [HttpPost]
        public async Task<Unit> UpdateCadenaAprobacion(UpdateCadenaAprobacionCommand val)
        {
            return await Mediator.Send(val);
        }

        [Route("SendNotificacionAprobador")]
        [HttpPost]
        public async Task<Unit> SendNotificacionAprobador(SendNotificacionCommand val)
        {
            return await Mediator.Send(val);
        }

        [Route("GetAprobacionesTabla/{codTabla}")]
        [HttpGet]
        public async Task<List<Tuple<string,string>>> GetAprobacionesTabla(string codTabla)
        {
            return await Mediator.Send(new GetAprobacionesTablaQuery() { CodTabla = codTabla });
        }

        [Route("AprobacionPlan")]
        [HttpPost]
        public async Task<ActionResult<Unit>> AprobacionPlan(AprobacionPlanCommand command)
        {
            return await Mediator.Send(command);
        }

        [Route("CreateAprobacionPlan")]
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateAprobacionPlan(AprobacionPlanCreateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("DeleteAprobacionPlan/{codAccion}")]
        public async Task<ActionResult<Unit>> DeleteAprobacionPlan(int codAccion)
        {
            return await Mediator.Send(new AprobacionPlanDeleteCommand() { CodAccion = codAccion });
            //return await Mediator.Send(values);
        }

        [HttpGet("GetAprobacionPlan/{codAccion}")]
        public async Task<ActionResult<AprobacionPlanVM>> GetAprobacionPlan(int codAccion)
        {
            return await Mediator.Send(new AprobacionPlanGetQuery() { CodAccion = codAccion });
            //return await Mediator.Send(values);
        }
    }
}