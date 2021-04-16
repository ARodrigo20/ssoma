using Hsec.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hsec.Domain.Entities.Otros
{
    public class TComite : AuditableEntity
    {
        public string CodComite { get; set; }

        public DateTime Fecha { get; set; }

        public string Hora { get; set; }

        public string CodTipoComite { get; set; }

        public string CodCategoria { get; set; }

        public string CodPosicionGer { get; set; }

        public string CodPosicionSup { get; set; }

        public string Lugar { get; set; }

        public string DetalleApertura { get; set; }

        public string CodSecretario { get; set; }

        public string ResumenSalud { get; set; }

        public string ResumenSeguridad { get; set; }

        public string ResumenMedioAmbiente { get; set; }

        public string ResumenComunidad { get; set; }

        public DateTime FechaCierre { get; set; }

        public string HoraCierre { get; set; }

        public ICollection<TListaParticipantesComite> ListaParticipantes { get; set; }

        public TComite()
        {
            ListaParticipantes = new HashSet<TListaParticipantesComite>();
        }
    }
}
