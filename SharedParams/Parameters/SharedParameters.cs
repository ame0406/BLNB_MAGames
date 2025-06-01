using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedParams.Parameters
{
    public class SharedParameters
    {
        //Important Enum --------------------------------------------------------------------

        public enum Status
        {
            Vente = 1,
            Garder = 2,
        }

        public enum SaleType
        {
			Switch = 1,
            Xbox360 = 2,
            Playstation3 = 3,
            Wii = 4,
		}

        public enum Marque
        {
			Nintendo = 1,
            Sony = 2,
            Microsoft = 3,
		}

        public enum Condition
        {
			CIB = 1,
            Loose = 2,
            Boxed = 3,
		}

        // -----------------------------------------------------------------------------------
		public enum ClosingModalChoice
        {
            LeftButton = 1,
            RightButton = 2,
            CloseX = 3
        }

        public enum TypeObj
        {
            Games = 1, 
            Hardware = 2,
            Other = 3
        }



	}
}
