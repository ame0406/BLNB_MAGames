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
            Échange = 3,
            Terminer = 4,
            NonFonctionnel = 5,
		}

        public enum SaleType
        {
			Xbox360 = 1,
            PS3 = 2,
            Switc = 3,
            NES = 4,
            SNES = 5,
            N64 = 6,
            Gamecube = 7,
            Gameboy = 8,
            GameboyColor = 9,
            GameboyAdvance = 10,
            DS = 11,
            DS3 = 12,
            Wii = 13,
            WiiU = 14,
            PS1 = 15,
            PS2 = 16,
            PS4 = 17,
            PS5 = 18,
            PSP = 19,
            PSVita = 20,
            XBOX = 21,
            XboxOne = 22,
            XboxSeriesX = 23,
            PC = 24,
            Sega = 25,
            Coleco = 26,
            Atari = 27,
            DVD = 28,
            Skylanders = 29,
            Figurine = 30,
            CartePokemon = 31,
            Lego = 32,
            Pop = 33,
            Supplies = 34,
		}

        public enum Marque
        {
			Microsoft = 1,
			Nintendo = 2,
            Sony = 3,
		}

        public enum Condition
        {
			CIB = 1,
            Boxed = 2,
            Loose = 3,
            CD_Manuel = 4,
            Boxe_Manuel = 5,
            Boxe = 6,
            Manuel = 7,
            Sealed = 8,
            CIB_ = 9,
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
