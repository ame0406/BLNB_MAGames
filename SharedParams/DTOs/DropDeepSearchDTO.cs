using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedParams.DTOs
{
    public class DropDeepSearchDTO
    {
        public string strSearch {  get; set; } = string.Empty;
        public bool _haveGames { get; set; } = false;
        public bool _haveHardware { get; set; } = false;
        public bool _haveOther { get; set; } = false;
    }
}
