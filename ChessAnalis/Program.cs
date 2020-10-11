using System;

namespace chassAnaliz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("я считаю, жди)");
            var res = Game.Update(true, new Map(new string[]
            {
                "   K    ",
                "r       ",
                "        ",
                "        ",
                "  k     ",
                "        ",
                "  q     ",
                "       R"
            }), 1);
            while (true)
            {
                Console.Clear();
                if (res == Result.win) Console.WriteLine("победа!!!");
                if (res == Result.neytral) Console.WriteLine("ничья");
                if (res == Result.lose) Console.WriteLine("проигрыш(((");
                Console.Read();
            }
        }
    }
}
