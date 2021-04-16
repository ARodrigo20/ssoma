using AutoMapper;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;


namespace Hsec.Application.ReportePeligroFatal.Queries.GetControlesQuery
{
    public class GetControlesQuery : IRequest<ControlesVM>
    {
        public string CodPosicion { get; set; }
        public string CodPeligro { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public class GetControlesQueryHandler : IRequestHandler<GetControlesQuery, ControlesVM>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetControlesQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ControlesVM> Handle(GetControlesQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var vm = new ControlesVM();
                    vm.List = new List<ControlDto>();

                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "pkg_TControl_Critico_ReporteDetalle";
                    cmd.Parameters.Add("@CodPosicion", System.Data.SqlDbType.VarChar, 20).Value = request.CodPosicion;
                    cmd.Parameters.Add("@CodPeligro", System.Data.SqlDbType.VarChar, 200).Value = request.CodPeligro;
                    cmd.Parameters.Add("@Anio", System.Data.SqlDbType.VarChar, 10).Value = request.Anio;
                    cmd.Parameters.Add("@Mes", System.Data.SqlDbType.VarChar, 5).Value = request.Mes;

                    //cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ControlDto row = new ControlDto();
                            row.CodReportePF = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            row.CodCC = reader.IsDBNull(1) ? null : reader.GetString(1);
                            row.DesCC = reader.IsDBNull(2) ? null : reader.GetString(2);
                            row.Anio = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            row.Mes = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                            row.Observacion = reader.IsDBNull(5) ? null : reader.GetString(5);
                            row.Efectividad = reader.IsDBNull(6) ? 0 : reader.GetDouble(6);

                            vm.List.Add(row);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();

                    conn.Close();

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