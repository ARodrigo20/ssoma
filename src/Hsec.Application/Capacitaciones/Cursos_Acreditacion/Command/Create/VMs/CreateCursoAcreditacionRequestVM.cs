using AutoMapper;
using Hsec.Application.Common.Mappings;
using Hsec.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Application.Capacitaciones.Cursos_Acreditacion.Command.Create.VMs
{
    public class CreateCursoAcreditacionRequestVM
    {
        public string? codCurso { get; set; }
        public string? codPersona { get; set; }
        public string? codStiker { get; set; }
        public string? candado { get; set; } // CodTarjeta       
        public DateTime? fechaStiker { get; set; }
        public DateTime? fechaTarjeta { get; set; }
    }
}