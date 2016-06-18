using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWashFiap.Model
{
    public class AgendamentoModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Veiculo { get; set; }
        public string Placa { get; set; }
        public DateTime Data { get; set; }
    }
}
