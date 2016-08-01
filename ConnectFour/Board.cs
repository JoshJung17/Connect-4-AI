using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Board : SimpleBoard
    {
        private Board[] _children = new Board[C.COLUMNS];
        private byte _missingChildren = C.COLUMNS;
        private int _wins = 0, _simulations = 0;
        private Board _parent;

        public Board()
        {
            MissingChildren = C.COLUMNS;
        }

        public void Reset()
        {
            SimulationCount = 0;
            WinCount = 0;
            _parent = null;
            _wins = 0; _simulations = 0; _children = new Board[C.COLUMNS];
            Heights = new byte[C.COLUMNS];
            Discs = new Byte[C.COLUMNS];
            Turn = false;
            FinalState = false;

            MissingChildren = C.COLUMNS;
            for (int i = 0; i < C.COLUMNS; ++i)
            {
                Heights[i] = 0;
                Discs[i] = 0;
                Children[i] = null;
            }
        }

        //changes the values of the given board (reuses node)
        public void MakeRandomChild(Board board)
        {
            board.SimulationCount = 0;
            board.WinCount = 0;
            board.MissingChildren = 0;
            board.Turn = Turn;
            board.Parent = this;
            board.FinalState = false;

            int k = C.rnd.Next(0, MissingChildren);
            int cnt = 0, idx = 0;
            for (int i = 0; i < C.COLUMNS; ++i)
            {
                if (Children[i] == null && Heights[i]<C.ROWS)
                {
                    if (cnt == k) idx = i;
                    ++cnt;
                }
                board.Heights[i] = Heights[i];
                board.Discs[i] = Discs[i];
                board.Children[i] = null;
            }
            board.Update(idx);
            if (!board.FinalState)
                for (int i = 0; i < C.COLUMNS; i++)
                {
                    if (board.Heights[i] < C.ROWS)
                        ++board.MissingChildren;
                }

            --MissingChildren;
            Children[idx] = board;
        }

        //creates a new node, sets appropriate values, then returns it
        public Board MakeRandomChild()
        {
            Board board = new Board();
            board.Parent = this;
            board.MissingChildren = 0;
            board.Turn = Turn;

            int k = C.rnd.Next(0, MissingChildren);
            int cnt = 0, idx = 0;
            for (int i = 0; i < C.COLUMNS; ++i)
            {
                if (Children[i] == null && Heights[i]<C.ROWS)
                {
                    if (cnt == k) idx = i;
                    ++cnt;
                }
                board.Heights[i] = Heights[i];
                board.Discs[i] = Discs[i];
                board.Children[i] = null;
            }
            board.Update(idx);
            if (!board.FinalState)
                for (int i = 0; i < C.COLUMNS; i++)
                {
                    if (board.Heights[i] < C.ROWS)
                        ++board.MissingChildren;
                }
            --MissingChildren;
            Children[idx] = board;
            return board;
        }

        public void CopyTo(MonteCarloBoard destBoard)
        {
            destBoard.Turn = Turn;
            destBoard.FinalState = FinalState;
            for (int i = 0; i < C.COLUMNS; ++i)
            {
                destBoard.Heights[i] = Heights[i];
                destBoard.Discs[i] = Discs[i];
            }
        }

        public float GetValue(int totalSimulations)
        {
            return (float)_wins / SimulationCount + (float)(Math.Sqrt(5 * Math.Log(totalSimulations) / SimulationCount));
        }
        
        public byte MissingChildren
        {
            get
            {
                return _missingChildren;
            }
            private set
            {
                _missingChildren = value;
            }
        }

        public Board[] Children
        {
            get
            {
                return _children;
            }
            private set
            {
                _children = value;
            }
        }

        public int WinCount
        {
            get
            {
                return _wins;
            }
            set
            {
                _wins = value;
            }
        }

        public int SimulationCount
        {
            get
            {
                return _simulations;
            }
            set
            {
                _simulations = value;
            }
        }

        public Board Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }
    }
}
