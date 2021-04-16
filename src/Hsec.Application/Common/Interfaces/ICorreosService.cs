using System.Threading.Tasks;

namespace Hsec.Application.Common.Interfaces
{
    public interface ICorreosService
    {
        public Task NotificarAprobador(string emails, string DocReferencia,string CodTabla);
        public Task NotificarRechazo(string emails, string DocReferencia, string CodTabla);
        public Task NotificarDocAprobado(string emails, string DocReferencia, string CodTabla);
        public Task NotificarModificar(string emails, string DocReferencia, string CodTabla);
    }
}
