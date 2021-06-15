using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B19_Ex05_Othelo_SimaWeiss_309823011
{
    public struct MatrixTile
    {
        private int m_CurrentValue;
        private bool m_Taken;
        private bool m_LegalMove;

        public bool Taken
        {
            get { return m_Taken; }
            set { m_Taken = value; }
        }

        public int CurrentValue
        {
            get { return m_CurrentValue; }
            set { m_CurrentValue = value; }
        }

        public bool LegalMove
        {
            get { return m_LegalMove; }
            set { m_LegalMove = value; }
        }
    }
}
