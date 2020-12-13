using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreatePDF
{
    public class PlantillaParametros
    {
        public string Plantilla { get; set; }
        public List<IEnumerable<Parametro>> Parametros { get; set; }
    }
}
