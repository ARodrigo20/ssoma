using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Commands.CreatePersona;
using Hsec.Application.General.Personas.Queries.GetBuscarPersonas;
using Hsec.Application.General.Personas.Queries.GetCode2Name;
using Hsec.Application.General.Personas.Queries.GetCode2NameOne;
using Hsec.Application.General.Personas.Queries.GetPersona;
using Hsec.Application.General.Personas.Queries.GetPersonaByNomApeNro;
using Hsec.Application.General.Personas.Queries.GetPersonaByNomApeNroAll;
using Hsec.Application.General.Personas.Queries.GetPersonaCodigoQuery;
using Hsec.Application.General.Personas.Queries.GetPersonasQuery;
using Hsec.Application.General.Personas.Queries.GetEmpresaPersonas;
using Hsec.Application.General.Personas.Queries.GetAprobadorInmediato;
using Microsoft.AspNetCore.Mvc;

namespace Hsec.WebApi.Controllers.General
{    
    public class PersonasController : ApiController
    {
        [HttpPost("Buscar")]
        public async Task<ActionResult<PersonasVM>> Get(GetBuscarPersonasQuery busqueda)
        {
            return await Mediator.Send(busqueda);
        }
        
        [HttpGet("{IdPersona}")]
        public async Task<ActionResult<PersonaVM>> Get(string IdPersona)
        {
            return await Mediator.Send(new GetPersonaQuery() { CodPersona = IdPersona});
        }

        // [HttpGet("PersonalEntendido/{IdPersona}")]
        // public async Task<ActionResult<string>> Create(string IdPersona)
        // {
        //     return await Mediator.Send(new GetPersonaQuery() { CodPersona = IdPersona});
        // }

        [HttpGet("NomApeNro/{text}")]
        public async Task<ActionResult<PersonaByVM>> GetBy(string text)
        {
            return await Mediator.Send(new GetPersonaByNomApeNroQuery() { text = text });
        }

        [HttpGet("GetCodigo/{NroDocumento}")]
        public async Task<ActionResult<string>> GetCodigo(string NroDocumento)
        {
            return await Mediator.Send(new GetPersonaCodigoQuery() { nroDocumento = NroDocumento });
        }

        [HttpGet("NomApeNroAll/{text}")]
        public async Task<ActionResult<ListPersonasVM>> GetByAll(string text)
        {
            return await Mediator.Send(new GetPersonaByNomApeNroAllQuery() { text = text });
        }

        //[HttpGet("BuscarStaticData")]
        //public async Task<ActionResult<StaticaDataPersonasVM>> Get()
        //{
        //    return await Mediator.Send(new GetBuscarStaticaDataPersonasQuery());
        //}

        [HttpPost("Code2Name")]
        public async Task<ActionResult< List<string> >> Get(List<string> Code)
        {
            return await Mediator.Send(new GetCode2NameQuery() { data = Code });
        }
        [HttpPost("GetCodPersonaName")]
        public async Task<ActionResult<List<Tuple<string,string>>>> GetPersonas(List<string> CodPers)
        {
            return await Mediator.Send(new GetCodPersonaNameQuery() { data = CodPers });
        }
        [HttpPost("GetPersonasCap")]
        public async Task<ActionResult<List<PersonaMinDTO>>> GetPersonasCap(List<string> CodPers)
        {
            return await Mediator.Send(new GetPersonasNameQuery() { data = CodPers });
        }
        [HttpGet("Code2NameOne/{Code}")]
        public async Task<ActionResult< string>> GetOne(string Code)
        {
            return await Mediator.Send(new GetCode2NameOneQuery() { code = Code });
        }

        [HttpPost("ExpositorExterno/Create")]
        public async Task<ActionResult<string>> Create(CreatePersonaExpositorCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("GetPersonasPorDni")]
        public async Task<ActionResult<List<PersonaVM>>> GetPersonas(ICollection<string> dnis)
        {
            return await Mediator.Send(new GetPersonasQuery() { dnisPersona = dnis });
        }

        [HttpGet("GetEmpresaPersonas")]
        public async Task<ActionResult<List<EmpresaVM>>> GetEmpresaPersonas()
        {
            return await Mediator.Send(new GetEmpresaPersonasQuery());
        }

        [HttpGet("AprobadorInmediato/{Code}")]
        public async Task<ActionResult<PersonaVM>> GetAprobadoresInmediatos(string Code)
        {
            return await Mediator.Send(new GetAprobadorInmediatoQuery() { codPersona = Code });
        }

    }
}
