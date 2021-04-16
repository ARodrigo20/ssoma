namespace Hsec.Application.Auditoria.Queries.GetBuscarHallazgosAuditoria
{
  public class HallazgoDto
  {
    public string TipoHallazgo { get; set; }
    public string CodHallazgo { get; set; }
    public string TipoNoconformidad { get; set; }
    public string CierreNoConformidad { get; set; }
    public string ObsDocAuditoria { get; set; }
    public string ObsDescripcion { get; set; }
    public string OpoDocAuditoria { get; set; }
    public string OpoDescripcion { get; set; }
    public string ReqDocAuditoria { get; set; }
    public string ReqDescripcion { get; set; }
  }
}