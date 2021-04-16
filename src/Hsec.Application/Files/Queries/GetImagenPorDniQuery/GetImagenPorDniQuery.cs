using AutoMapper;
using Hsec.Application.Common.Exceptions;
using Hsec.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Hsec.Application.Files.Queries.GetImagenPorDniQuery
{
    public class GetImagenPorDniQuery : IRequest<byte[]>
    {
        public string Dni { get; set; }

        public class GetImagenPorDniQueryHandler : IRequestHandler<GetImagenPorDniQuery, byte[]>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetImagenPorDniQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<byte[]> Handle(GetImagenPorDniQuery request, CancellationToken cancellationToken)
            {
                //var user = await _context.TFile.FirstOrDefaultAsync(a => a.CorrelativoArchivos == request.Dni);
                //if (user.Estado == true)
                //{
                //    byte[] imgbyte = user.ArchivoData;
                //    if (user == null)
                //    {
                //        throw new ExceptionGeneral(user.ToString());
                //    }
                //    return imgbyte;
                //}
                //else
                //{
                //    throw new ExceptionGeneral("EL ARCHIVO FUE ELIMINADO !");
                //}

                try
                {

                    FileStream stream;
                    // Streams the BLOB to the FileStream object.  
                    BinaryWriter writer;

                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "TPersona_Recuperar_Imagen";
                    cmd.Parameters.Add("@NroDocumento", System.Data.SqlDbType.VarChar, 20).Value = request.Dni;
                    //cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Size of the BLOB buffer.  
                    //int reader = 10000000;
                    // The BLOB byte[] buffer to be filled by GetBytes.  
                    byte[] outByte = new byte[10000000];

                    long retval;

                    long startIndex = 0;

                    if (reader.HasRows)
                    {
                        int correlativo = 0;
                        while (reader.Read())
                        {
                            outByte = (byte[])reader[0];
                        }
                    }
                    else
                    {
                        string filePath = AppDomain.CurrentDomain.BaseDirectory + "avatar.jpg";
                        outByte = File.ReadAllBytes(filePath);
                    }
                    reader.Close();

                    conn.Close();

                    return outByte;

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