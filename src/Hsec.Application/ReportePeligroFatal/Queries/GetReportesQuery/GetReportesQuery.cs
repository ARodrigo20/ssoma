using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hsec.Application.ReportePeligroFatal.Queries.GetReportesQuery
{
    public class GetReportesQuery : IRequest<ReportesVM>
    {
        public string Posicion { get; set; }
        public string CodPeligro { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public int Efectivo { get; set; }
        public int Pagina { get; set; }
        public int PaginaTamanio { get; set; }

        public class GetReportesQueryHandler : IRequestHandler<GetReportesQuery, ReportesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetReportesQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ReportesVM> Handle(GetReportesQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var vm = new ReportesVM();
                    vm.List = new List<ReporteDto>();

                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "pkg_TControl_Critico_BuscarReportePF";
                    cmd.Parameters.Add("@CodPosicion", System.Data.SqlDbType.VarChar, 20).Value = request.Posicion;
                    cmd.Parameters.Add("@CodPeligro", System.Data.SqlDbType.VarChar, 200).Value = request.CodPeligro;
                    cmd.Parameters.Add("@Anio", System.Data.SqlDbType.VarChar, 10).Value = request.Anio;
                    cmd.Parameters.Add("@Mes", System.Data.SqlDbType.VarChar, 5).Value = request.Mes;
                    if(request.Efectivo == 0)
                    {
                        cmd.Parameters.Add("@Efectivo", System.Data.SqlDbType.Bit).Value = 0;
                    }
                    else if (request.Efectivo == 1)
                    {
                        cmd.Parameters.Add("@Efectivo", System.Data.SqlDbType.Bit).Value = 1;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Efectivo", System.Data.SqlDbType.Bit).Value = null;
                    }

                    //cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        int correlativo = 0;
                        while (reader.Read())
                        {
                            ReporteDto row = new ReporteDto();
                            row.Correlativo = correlativo;
                            row.Posicion = reader.GetString(0);
                            row.DesPosicion = reader.GetString(1);
                            row.CodPeligro = reader.IsDBNull(2) ? null : reader.GetString(2);
                            row.Anio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            row.Mes = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);

                            vm.List.Add(row);
                            correlativo++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();

                    conn.Close();

                    vm.Count = vm.List.Count();

                    return vm;

                }
                catch (Exception e)
                {
                    Exception ee = e;
                    throw e;
                }
            }
        }
    }
}


