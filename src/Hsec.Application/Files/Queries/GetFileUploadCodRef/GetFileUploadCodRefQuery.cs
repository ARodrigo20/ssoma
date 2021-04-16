using AutoMapper;
using Hsec.Application.Common.Interfaces;
using Hsec.Application.Files.Queries.GetFilesUpload;
using Hsec.Domain.Entities.PlanAccion;
using MediatR;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hsec.Application.Files.Queries.GetFileUploadCodRef
{
    public class GetFileUploadCodRefQuery : IRequest<FileUploadAllVM>
    {
        public string? NroDocReferencia { get; set; }
        public string? NroSubDocReferencia { get; set; }
        public string? CodTablaRef { get; set; }
        public class GetFileUploadCodRefQueryHandler : IRequestHandler<GetFileUploadCodRefQuery, FileUploadAllVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetFileUploadCodRefQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FileUploadAllVM> Handle(GetFileUploadCodRefQuery request, CancellationToken cancellationToken)
            {
                var docRef = request.NroDocReferencia;
                var docSubRef = request.NroSubDocReferencia; //aca le mandas el codPersona en caso sea el codTabla "TCAP"
                var tablaRef = request.CodTablaRef;
                var codPersona = "";
                if (tablaRef == "TCAP") {
                    codPersona = docSubRef;
                    docSubRef = "";
                }
                FileUploadAllVM archivos = new FileUploadAllVM();
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "TFile_Get";
                cmd.Parameters.Add("@DocReferencia", System.Data.SqlDbType.VarChar, 20).Value = docRef;
                cmd.Parameters.Add("@DocSubReferencia", System.Data.SqlDbType.VarChar, 20).Value = docSubRef;
                cmd.Parameters.Add("@CodTabla", System.Data.SqlDbType.VarChar, 10).Value = tablaRef;
                SqlDataAdapter document = new SqlDataAdapter(cmd);
                conn.Close();
                DataTable dtTable = new DataTable();
                document.Fill(dtTable);
                foreach (DataRow dtRow in dtTable.Rows)
                {
                    archivos.data.Add(new FilesUploadOneVM()
                    {
                        CorrelativoArchivos = int.Parse(dtRow["CorrelativoArchivos"].ToString()),
                        Size = long.Parse(dtRow["Size"].ToString()),
                        Nombre = dtRow["Nombre"].ToString(),
                        Descripcion = dtRow["Descripcion"].ToString(),
                        TipoArchivo = dtRow["TipoArchivo"].ToString(),
                        CodTablaRef = dtRow["CodTablaRef"].ToString(),
                        NroDocReferencia = dtRow["NroDocReferencia"].ToString(),
                        NroSubDocReferencia = dtRow["NroSubDocReferencia"].ToString(),
                        Estado = true,
                    });
                }
                archivos.count = archivos.data.Count;

                if (tablaRef == "TCAP")
                {                   
                    var Revisado = _context.TValidadorArchivo.Where(r => r.NroDocReferencia == docRef && r.CodPersona== codPersona).ToList();

                    foreach (var item in archivos.data)
                    {
                        if (Revisado.Any(v => v.CodArchivo == item.CorrelativoArchivos)) item.Revisado = Revisado.First(r => r.CodArchivo == item.CorrelativoArchivos).EstadoAccion;
                        else item.Revisado = 0;
                    }                                                         
                }
                return archivos;
                
            }
        }
    }
}