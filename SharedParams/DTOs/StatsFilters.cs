using SharedParams.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedParams.DTOs
{
    public class Filters
    {
        public bool ToMaya { get; set; } = false;
        public bool IncludeToBoth { get; set; } = false;
        public DateTime? dateDebut { get; set; } = null;
        public DateTime? dateFin { get; set; } = null;
        public int status { get; set; } = (int)SharedParameters.Status.Vente;

	}
}
