using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using Hsec.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.Capacitaciones.Participantes.Queries.GetFileReport
{
    public class GetReporteFileQuery : IRequest<byte[]>
    {
        public string CodPersona;
        public string CodCurso;
        public class GetReporteFileQueryHandler : IRequestHandler<GetReporteFileQuery, byte[]>
        {
            private readonly IApplicationDbContext _context;
            private readonly IReportService _reportService;

            public GetReporteFileQueryHandler(IApplicationDbContext context, IReportService reportService)
            {
                _context = context;
                _reportService = reportService;
            }

            public async Task<byte[]> Handle(GetReporteFileQuery request, CancellationToken cancellationToken)
            {              
                List<Tuple<string, string>> Params = new List<Tuple<string, string>>();
                Params.Add(new Tuple<string, string>("CodPersona", request.CodPersona));
                Params.Add(new Tuple<string, string>("CodCurso", request.CodCurso));
                //return _reportService.GetReport("/Capacitaciones/CertificadoParticipante", Params,ExportFormat.Image, "&rc:OutputFormat=JPEG");
                return await _reportService.GetReport2("/Capacitaciones/CertificadoParticipante", Params, ExportFormat.PDF, "");
            }
        }
    }

}
