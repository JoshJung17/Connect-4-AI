using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class MonteCarloBoard : SimpleBoard
    {
        public bool RunSimulation()
        {
            List<int> moves = new List<int>();
            Random rnd = new Random();
            while (!FinalState)
            {
                for (int i = 0; i < C.COLUMNS; ++i)
                {
                    if (Heights[i] < C.ROWS) moves.Add(i);
                }
                if (moves.Count == 0)
                    return rnd.Next(0, 2) == 0 ? true : false;
                Update(moves[rnd.Next(0, moves.Count)]);
                moves.Clear();
            }
            return Turn;
        }
    }
}
