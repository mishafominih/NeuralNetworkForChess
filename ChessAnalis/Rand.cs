using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAnalis
{
    public static class Rand
    {
        static Random r = new Random();

        public static int GetRandom()
        {
            return r.Next(10, 1000);
        }
    }
}
