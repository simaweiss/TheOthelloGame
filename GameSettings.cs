using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B19_Ex05_Othelo_SimaWeiss_309823011
{
   public class GameSettings
    {
        public eTypeGame TypeGame { get; set; }

        public byte BoardSize { get; set; } = 6;

        public bool IsOpponentPc()
        {
            return TypeGame == eTypeGame.AgainstComputer;
        }
    }
}
