using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class SimpleBoard
    {
        private byte[] _heights = new byte[C.COLUMNS];
        private byte[] _discs = new byte[C.COLUMNS];
        private bool _turn;
        private bool _finalState = false;

        private bool CheckWin(int row, int col)
        {
            for (int i = 0; i < 4; ++i)
            {
                //cancel the double-counting of the position considered
                int cnt = -1;
                int r = row, c = col;
                while (c >= 0 && c < C.COLUMNS && r >= 0 && r < Heights[c] && ((Discs[c] & (1 << r)) == 0 ^ Turn))
                {
                    r += C.dir[i, 0]; c += C.dir[i, 1];
                    ++cnt;
                }
                r = row; c = col;
                while (c >= 0 && c < C.COLUMNS && r >= 0 && r < Heights[c] && ((Discs[c] & (1 << r)) == 0 ^ Turn))
                {
                    r -= C.dir[i, 0]; c -= C.dir[i, 1];
                    ++cnt;
                }
                if (cnt >= 4) 
                    return true;
            }
            return false;
        }

        //places disc at column, checks for win
        public void Update(int col)
        {
            int row = Heights[col];
            ++Heights[col];
            if (Turn) Discs[col] += (byte)(1 << row);
            if (CheckWin(row, col))
                FinalState = true;
            Turn = !Turn;
        }

        public byte[] Heights
        {
            get
            {
                return _heights;
            }
            set
            {
                _heights = value;
            }
        }

        public byte[] Discs
        {
            get
            {
                return _discs;
            }
            set
            {
                _discs = value;
            }
        }

        public bool Turn
        {
            get
            {
                return _turn;
            }
            set
            {
                _turn = value;
            }
        }

        public bool FinalState
        {
            get
            {
                return _finalState;
            }
            set
            {
                _finalState = value;
            }
        }
    }
}
