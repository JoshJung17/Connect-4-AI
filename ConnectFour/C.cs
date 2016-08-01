using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class C
    {
        public const int COLUMNS = 8;
        public const int ROWS = 6; // must be less or equal to than 8
        public const float EPS = 0.0000001F;
        public const int THREADS = 4;

        public static int[,] dir = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 } };
        public static Random rnd = new Random();
    }
}
