using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Reunion.Models;
using Hsec.Domain.Entities.Otros;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hsec.Application.General.Personas.Queries.GetPersona;

namespace Hsec.Application.Reunion.Queries.ReunionGetByCod
{
    public class ReunionGetByCodQuery : IRequest<ReunionGetDto>
    {
        public string CodReunion { get; set; }

        public class ReunionGetByCodQueryHandler : IRequestHandler<ReunionGetByCodQuery, ReunionGetDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public ReunionGetByCodQueryHandler(IApplicationDbContext context, IMapper mapper, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task<ReunionGetDto> Handle(ReunionGetByCodQuery request, CancellationToken cancellationToken)
            {
                var _reunion = _context.TReunion.Include(p => p.ReunionAsistentes).Include(c => c.ReunionAusentes).Include(r => r.ReunionJustificados).Include(a => a.ReunionAgendas).Where(u => u.CodReunion == request.CodReunion)
                    .FirstOrDefault();

                var reunion = new ReunionGetDto();

                reunion.CodReunion = _reunion.CodReunion;
                reunion.Reunion = _reunion.Reunion;
                reunion.Lugar = _reunion.Lugar;
                reunion.Fecha = _reunion.Fecha;
                reunion.Hora = _reunion.Hora;
                reunion.Acuerdos = _reunion.Acuerdos;
                reunion.Comentarios = _reunion.Comentarios;
                reunion.Otros = _reunion.Otros;
                reunion.CodPerFacilitador = _reunion.CodPerFacilitador;

                List<string> ListPersonAsistentes = _reunion.ReunionAsistentes.Where(p => p.Estado == true).Select(t => t.CodPersona).ToList();

                foreach (var item in ListPersonAsistentes)
                {
                    //var temp = Base64Encode(item);
                    //Console.OutputEncoding = Encoding.Unicode;
                    //var prueba5 = System.Net.WebUtility.UrlEncode(item);
                    //var prueba4 = System.Web.HttpUtility.UrlEncode(item, Encoding.Unicode);
                    //var prueba3 = System.Uri.EscapeUriString(item);
                    //var prueba2 = WebUtility.UrlEncode(item);
                    //var pruebadeff = "0 46722513";
                    //var prueba = pruebadeff.Replace("&nbsp", "funciona");

                    //var pers = await _generalService.GetPersonas(item);
                    var pers = await _mediator.Send(new GetPersonaQuery() { CodPersona = item });
                    //var convert = JsonConvert.DeserializeObject<PersonaDto>(pers);
                    reunion.ReunionAsistentes.Add(pers);
                }

                List<string> ListPersonAusentes = _reunion.ReunionAusentes.Where(p => p.Estado == true).Select(t => t.CodPersona).ToList();

                foreach (var item in ListPersonAusentes)
                {
                    //var pers = await _generalService.GetPersonas(item);
                    var pers = await _mediator.Send(new GetPersonaQuery() { CodPersona = item });

                    //var convert = JsonConvert.DeserializeObject<PersonaDto>(pers);
                    reunion.ReunionAusentes.Add(pers);
                }

                List<string> ListPersonJustificados = _reunion.ReunionJustificados.Where(p => p.Estado == true).Select(t => t.CodPersona).ToList();

                foreach (var item in ListPersonJustificados)
                {
                    //var pers = await _generalService.GetPersonas(item);
                    var pers = await _mediator.Send(new GetPersonaQuery() { CodPersona = item });

                    //var convert = JsonConvert.DeserializeObject<PersonaDto>(pers);
                    reunion.ReunionJustificados.Add(pers);
                }

                _reunion.ReunionAgendas = _reunion.ReunionAgendas.Where(p => p.Estado == true).ToList();

                foreach (var item in _reunion.ReunionAgendas)
                {
                    var agenda = _mapper.Map<TAgenda, TAgendaDto>(item);
                    reunion.ReunionAgendas.Add(agenda);
                }

                return reunion;
            }
            public static string Base64Encode(string str)
            {
                var tb = System.Text.Encoding.UTF8.GetBytes(str);
                return System.Convert.ToBase64String(tb);
            }
            public static string Base64Decode(string base64Data)
            {
                var b = System.Convert.FromBase64String(base64Data);
                return System.Text.Encoding.UTF8.GetString(b);
            }
        }
    }
}
