using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreatePDF
{
    public class PlantillaParametros
    {
        public string Plantilla { get; set; }
        public IEnumerable<Parametro> Parametros { get; set; }
        public List<IEnumerable<Parametro>> Comparecientes { get; set; }
    }
}
