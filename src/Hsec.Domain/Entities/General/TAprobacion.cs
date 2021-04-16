using Hsec.Domain.Common;
using System.Collections.Generic;

namespace Hsec.Domain.Entities.General
{
  public class TAprobacion : AuditableEntity
  {
    public TAprobacion()
        {
            Historial = new HashSet<THistorialAprobacion>();
        }
    public int CodAprobacion { get; set; }
    public string DocReferencia { get; set; }
    public string ProcesoAprobacion { get; set; }
    public int Version { get; set; }
    public string CadenaAprobacion { get; set; }
    public string EstadoDoc { get; set; }
    public string CodResponsable { get; set; }
    public string CodJerResponsable { get; set; }
    public string CodTabla { get; set; }
    public ICollection<THistorialAprobacion> Historial { get; set; }
        
    public void EstadoIncial(string docReferencia,string codTabla,string codResponsable,string estado)
    {
      this.CodTabla = codTabla;
      this.DocReferencia = docReferencia;
      this.CodResponsable = codResponsable;
      this.EstadoDoc = estado;
    }
    public void AddVersion(int version = 1){
      this.Version = version;
    }
    public void AddProceso(string procesoAprobacion,string cadenaAprobacion){
      this.ProcesoAprobacion = procesoAprobacion;
      this.CadenaAprobacion = cadenaAprobacion;
    }
    public void AddReponsable(string CodResponsable,string CodJerResponsable){
      this.CodJerResponsable = CodJerResponsable;
      this.CodResponsable = CodResponsable;
    }
  }
}