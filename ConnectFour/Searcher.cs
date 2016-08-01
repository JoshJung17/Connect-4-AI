using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Searcher
    {
        List<Board> _emptyBoards = new List<Board>();
        Board _rootBoard;
        MonteCarloBoard[] _monteCarloBoards = new MonteCarloBoard[C.THREADS];
        int _treeSize;
        int _treeDepth;

        public Searcher()
        {
            RootBoard = new Board();
            for (int i = 0; i < C.THREADS; ++i)
            {
                MonteCarloBoards[i] = new MonteCarloBoard();
            }
            TreeSize = 1;
            TreeDepth = 1;
        }

        public int GetMove()
        {
            int idx = 0;
            int best = 0;

            for (int i = 0; i < C.COLUMNS; ++i)
            {
                if (RootBoard.Children[i] == null) continue;
                if (RootBoard.Children[i].SimulationCount > best)
                {
                    best = RootBoard.Children[i].SimulationCount;
                    idx = i;
                }
            }

            return idx;
        }

        public void Update(int col)
        {
            EmptyBoards.Add(RootBoard);
            for (int i = 0; i < C.COLUMNS; ++i)
            {
                if (i == col) continue;
                if (RootBoard.Children[i] != null)
                    EmptyBoards.Add(RootBoard.Children[i]);
                RootBoard.Children[i] = null;
            }
            
            RootBoard = RootBoard.Children[col];
            RootBoard.Parent.Children[col] = null;
            RootBoard.Parent = null;
            TreeDepth = -1;
        }

        public void SearchOnce()
        {
            
            Board board = Select(RootBoard);
            byte winner=0;
            if (board.FinalState) { if (board.Turn) winner = C.THREADS; }
            else
            {
                board = Expand(board);
                Parallel.For(0, C.THREADS, i =>
                {
                    board.CopyTo(MonteCarloBoards[i]);
                    if (MonteCarloBoards[i].RunSimulation()) ++winner;
                });
            }
            BackPropagate(board, winner);
        }

        //recursively goes down tree then returns a node
        //a node is returned if it is missing a child
        private Board Select(Board parentBoard)
        {
            if (parentBoard.MissingChildren > 0 || parentBoard.FinalState) return parentBoard;
            float best=0, val=0;
            int totalSimulations = parentBoard.SimulationCount-1;
            List<int> bestIdx = new List<int>();

            for (int i = 0; i < C.COLUMNS; ++i)
            {
                if (parentBoard.Children[i] != null)
                {
                    val = parentBoard.Children[i].GetValue(totalSimulations);
                    if (val > best)
                    {
                        best = val;
                        bestIdx.Clear();
                        bestIdx.Add(i);
                    }
                    else if (Math.Abs(val - best) < C.EPS)
                    {
                        bestIdx.Add(i);
                    }
                }
            }
            if (bestIdx.Count == 0) return parentBoard;
            int k = C.rnd.Next(0, bestIdx.Count);
            return Select(parentBoard.Children[bestIdx[k]]);
        }

        //return a random unvisited child
        private Board Expand(Board parentBoard)
        {
            if (parentBoard.FinalState) return parentBoard;
            Board board;
            if (EmptyBoards.Count > 0)
            {
                if (EmptyBoards.Count == 1)
                   board = null;
                board = EmptyBoards[EmptyBoards.Count - 1];
                EmptyBoards.RemoveAt(EmptyBoards.Count - 1);
                AddEmptyBoards(board);
                parentBoard.MakeRandomChild(board);
                return board;
            }
            ++TreeSize;
            board = parentBoard.MakeRandomChild();
            return board;
        }

        private void BackPropagate(Board board, byte winner)
        {
            int cnt = 0;
            while (board != null)
            {
                ++cnt;
                board.SimulationCount += C.THREADS;
                if (board.Turn)
                    board.WinCount += winner;
                else
                    board.WinCount += C.THREADS - winner;
                board = board.Parent;
                
            }
            TreeDepth = Math.Max(TreeDepth, cnt);
        }

        private void AddEmptyBoards(Board board)
        {
            for (int i = 0; i < C.COLUMNS; ++i)
            {
                if (board.Children[i] != null)
                    EmptyBoards.Add(board.Children[i]);
            }
        }

        public List<Board> EmptyBoards
        {
            get
            {
                return _emptyBoards;
            }
        }

        public Board RootBoard
        {
            get
            {
                return _rootBoard;
            }
            private set
            {
                _rootBoard = value;
            }
        }

        private MonteCarloBoard[] MonteCarloBoards
        {
            get
            {
                return _monteCarloBoards;
            }
            set
            {
                _monteCarloBoards = value;
            }
        }

        public int TreeSize
        {
            get
            {
                return _treeSize;
            }
            set
            {
                _treeSize = value;
            }
        }

        public int TreeDepth
        {
            get
            {
                return _treeDepth;
            }
            set
            {
                _treeDepth = value;
            }
        }

    }
}
