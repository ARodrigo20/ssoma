
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using Hsec.Application.Incidentes.Models;
using Hsec.Application.Incidentes.Commands.CreateIncidente;
using Hsec.Application.Incidentes.Commands.DeleteIncidente;
using Hsec.Application.Incidentes.Commands.UpdateIncidente;
using Hsec.Application.Incidentes.Queries.GetAfectadosDescipcionesAcciones;
using Hsec.Application.Incidentes.Queries.GetBuscarInsidentes;
using Hsec.Application.Incidentes.Queries.GetCabeceraIncidente;
using Hsec.Application.Incidentes.Queries.GetCodigoPosicion;
using Hsec.Application.Incidentes.Queries.GetIncidente;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectado;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscar;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoBuscarMedioAmbiente;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoComunidad;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoComunidadBuscar;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoMedioAmbiente;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPersona;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPersonaBuscar;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPropiedad;
using Hsec.Application.Incidentes.Queries.GetIncidenteAfectadoPropiedadBuscar;
using Hsec.Application.Incidentes.Queries.GetIncidenteAnalisisCausalidad;
using Hsec.Application.Incidentes.Queries.GetIncidenteDetalle;
using Hsec.Application.Incidentes.Queries.GetIncidenteGeneral;
using Hsec.Application.Incidentes.Queries.GetIncidenteICAM;
using Hsec.Application.Incidentes.Queries.GetPortletMeses;
using Hsec.Application.Incidentes.Queries.GetPortletTipoMes;
using Hsec.Application.Incidentes.Queries.GetSeguimientoIncidente;
using Microsoft.AspNetCore.Mvc;
using Hsec.Application.PlanAccion.Commands.CreatePlanDeAccion;
using Hsec.Application.Files.Queries.GetFilesUpload;

namespace Hsec.WebApi.Controllers.Incidentes
{    
    public class IncidentesController : ApiController
    {
        [HttpPost("Create"), DisableRequestSizeLimit] // ok
        public async Task<ActionResult<string>> Create([FromForm] CreateIncidenteVM data)
        {
            var file = Request.Form.Files;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<IncidenteDto>(data.JSONIncidente);

            if (String.IsNullOrEmpty(data.JSONPlanAccion)) data.JSONPlanAccion = "[]";
            var planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanVM>>(data.JSONPlanAccion);

            return await Mediator.Send(new CreateIncidenteCommand() { data = obj, planAccion = planes, Files = file });
        }

        [HttpPost("{idIncidente}/update"), DisableRequestSizeLimit] // ok
        public async Task<ActionResult> Update([FromForm] UpdateIncidenteVM data, string idIncidente)
        {

            var file = Request.Form.Files;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<IncidenteDto>(data.JSONIncidente);
            if (obj.CodIncidente != idIncidente) return new BadRequestResult();

            if (String.IsNullOrEmpty(data.JSONPlanAccion)) data.JSONPlanAccion = "[]";
            var planes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlanVM>>(data.JSONPlanAccion);

            if (String.IsNullOrEmpty(data.JSONFile)) data.JSONFile = "[]";
            var updateFiles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FilesUploadOneVM>>(data.JSONFile);

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;
          
            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;

            

            await Mediator.Send(new UpdateIncidenteCommand() { data = obj, planAccion = planes, newFiles = file, updateFiles = updateFiles, rol = rol }); ;

            return new NoContentResult();
        }

        [HttpGet("{idIncidente}")] // ok
        public async Task<ActionResult<IncidenteDto>> Get(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("{idIncidente}/GetAfectadosDescipcionesAcciones")]
        public async Task<ActionResult<AfectadosDescipcionesAccionesDto>> GetAfectadoDA(string idIncidente)
        {
            return await Mediator.Send(new GetAfectadosDescipcionesAccionesQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("{idIncidente}/Afectado")]
        public async Task<ActionResult<AfectadosDescipcionesAccionesVM>> GetAfectado(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("{idIncidente}/Afectado/Comunidad")]
        public async Task<ActionResult<List<ComunidadAfectadoDto>>> GetAfectadoCo(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoComunidadQuery() { CodIncidente = idIncidente});
        }
        [HttpGet("{idIncidente}/Afectado/MedioAmbiente")]
        public async Task<ActionResult<List<MedioAmbienteAfectadoDto>>> GetAfectadoMA(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoMedioAmbienteQuery() { CodIncidente = idIncidente});
        }
        [HttpGet("{idIncidente}/Afectado/Persona")]
        public async Task<ActionResult<List<PersonaAfectadoDto>>> GetAfectadoPe(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoPersonaQuery() { CodIncidente = idIncidente});
        }
        [HttpGet("{idIncidente}/Afectado/Propiedad")]
        public async Task<ActionResult<List<PropiedadAfectadoDto>>> GetAfectadoPr(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoPropiedadQuery() { CodIncidente = idIncidente});
        }
        [HttpGet("{idIncidente}/AnalisisCausalidad")]
        public async Task<ActionResult< List<AnalisisCausalidadDto> >> GetAnalisisCausalidad(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAnalisisCausalidadQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("{idIncidente}/Detalle")]
        public async Task<ActionResult<DetalleDto>> GetDetalle(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteDetalleQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("{idIncidente}/General")]
        public async Task<ActionResult<DatosGeneralesDto>> GetGeneral(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteGeneralQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("{idIncidente}/ICAM")]
        public async Task<ActionResult<List<ICAMDto>>> GetICAM(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteICAMQuery() { CodIncidente = idIncidente });
        }
        
        [HttpGet("{idIncidente}/remove")] // ok
        public async Task<ActionResult> Remove(string idIncidente)
        {
            if (idIncidente == null) return new BadRequestResult();
            await Mediator.Send(new DeleteIncidenteCommand() { Codigo = idIncidente });
            return new NoContentResult();
        }
        [HttpPost("Buscar")] // ok
        public async Task<ActionResult<IncidentesVM>> Get(GetBuscarInsidentesQuery obj)
        {
            return await Mediator.Send(obj);
        }
        [HttpGet("Buscar/{idIncidente}/Afectados")]
        public async Task<ActionResult<AfectadosVM>> GetAfectados(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoBuscarQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("Buscar/{idIncidente}/Afectados/Personas")] // ok
        public async Task<ActionResult<AfectadosPersonaVM>> GetAfectadosPersonas(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoPersonaBuscaQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("Buscar/{idIncidente}/Afectados/Comunidad")] // ok
        public async Task<ActionResult<AfectadosComunidadVM>> GetAfectadosComunidad(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoComunidadBuscarQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("Buscar/{idIncidente}/Afectados/Propiedad")] // ok
        public async Task<ActionResult<AfectadosPropiedadVM>> GetAfectadosPropiedad(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoPropiedadBuscarQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("Buscar/{idIncidente}/Afectados/MedioAmbiente")] // ok
        public async Task<ActionResult<AfectadosMedioAmbienteVM>> GetAfectadosMedioAmbiente(string idIncidente)
        {
            return await Mediator.Send(new GetIncidenteAfectadoBuscarMedioAmbienteQuery() { CodIncidente = idIncidente });
        }
        [HttpGet("portlet/meses")] 
        public async Task<ActionResult<ICollection<Portlet>>> GetPortlet()
        {
            return await Mediator.Send(new GetPortletMesesQuery());
        }
        [HttpGet("portlet/tipo/mes")]
        public async Task<ActionResult<ICollection<PortletTipoIncidente>>> GetPortletTipoMes()
        {
            return await Mediator.Send(new GetPortletTipoMesQuery());
        }

        [HttpGet("codigoPosicion/{codigo}")]
        public async Task<ActionResult<IncidenteAprobacionVM>> GetCodigoPosicion(string codigo)
        {
            return await Mediator.Send(new GetCodigoPosicionQuery() { codigo = codigo });
        }



        [HttpGet("GetCabecera/{CodIncidente}")]
        public async Task<ActionResult<IncidenteCabeceraVM>> GetCabeceraIncidente(string CodIncidente)
        {
            return await Mediator.Send(new GetCabeceraIncidenteQuery() { CodIncidente = CodIncidente });
        }

        [HttpGet("seguimiento/{id_modulo}/{id_persona}/{id_anio}/{id_mes}")]
        public async Task<ActionResult<int>> seguimiento(string id_modulo,string id_persona,int id_anio,int id_mes)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(authHeader.Parameter) as JwtSecurityToken;

            var codPersona = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid").Value;
            var usuario = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
            var rol = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "role").Value;
            return await Mediator.Send(new GetSeguimientoIncidente() { 
                    modulo = id_modulo,
                    persona = id_persona,
                    anio = id_anio,
                    mes = id_mes,
                    usuario = usuario,
                    rol = rol
                });
        }

        [HttpPost("seguimiento")]
        public async Task<ActionResult< List<Tuple<string, int>> >> GetSeguimientoObseraciones(GetSeguimientoIncidentePersonas obj)
        {
            return await Mediator.Send(obj);
        }

        [HttpGet("Models")]
        public async Task<ActionResult<string>> GetVM(MODELS vm)
        {
            return "this is test and"; 
        }
    }
}
