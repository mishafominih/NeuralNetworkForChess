using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chassAnaliz
{
    public class Game
    {
        const int countStep = 7;
        public static Result Update(bool isWhiteHod, Map map, int count)
        {
            if (count > countStep) return Result.neytral;
            if (map.EndFor(true)) return Result.win;
            if (map.EndFor(false)) return Result.lose;
            //DrowMap(map, true);
            if (isWhiteHod)
            {
                bool isNeytral = false;
                var arr = map.WhiteFigures
                    .SelectMany(x => x.GetSteps(map))
                    .Select(x =>
                    {
                        if (map.map[x.Position.X, x.Position.Y] != null)
                            x.RandomNumber = map.map[x.Position.X, x.Position.Y].Cast;
                        return x;
                    })
                    .OrderBy(x => x.RandomNumber);
                foreach (var step in arr)
                {
                    var newMap = new Map(map);

                    var oldFigure = SetStep(step, newMap);

                    if (oldFigure != null)
                        newMap.BlackFigures.Remove(oldFigure);
                    var result = Update(!isWhiteHod, newMap, count + 1);
                    if (result == Result.lose)
                        return Result.win;
                    if (result == Result.neytral)
                        isNeytral = true;
                }
                return isNeytral ? Result.neytral : Result.lose;
            }
            else
            {
                var arr1 = map.BlackFigures
                    .SelectMany(x => x.GetSteps(map))
                    .Select(x =>
                    {
                        if (map.map[x.Position.X, x.Position.Y] != null)
                            x.RandomNumber = map.map[x.Position.X, x.Position.Y].Cast;
                        return x;
                    })
                    .OrderBy(x => x.RandomNumber);
                bool isNeytral = false;
                foreach (var step in arr1)
                {
                    var newMap = new Map(map);

                    var oldFigure = SetStep(step, newMap);

                    if (oldFigure != null)
                        newMap.WhiteFigures.Remove(oldFigure);
                    var result = Update(!isWhiteHod, newMap, count + 1);
                    if (result == Result.lose)
                        return Result.win;
                    if (result == Result.neytral)
                        isNeytral = true;
                }
                return isNeytral ? Result.neytral : Result.lose;
            }
        }

        private static Figure SetStep(Step step, Map newMap)
        {
            var f = newMap.map[step.PreviousPosition.X,
                                    step.PreviousPosition.Y];
            newMap.map[step.PreviousPosition.X,
                step.PreviousPosition.Y] = null;
            var oldFigure = newMap.map[step.Position.X,
                step.Position.Y];
            newMap.map[step.Position.X,
                step.Position.Y] = f;
            f.SetStep(step.Position);
            return oldFigure;
        }

        private static void DrowMap(Map map, bool clear)
        {
            if(clear) Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                Console.Write("+");
            }
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.Write("+");
                for (var j = 0; j < 8; j++)
                {
                    if (map.map[j, i] == null)
                        Console.Write(" ");
                    if (map.map[j, i] is Pawn)
                        if (map.map[j, i].IsWhite) Console.Write("p");
                        else Console.Write("P");
                    if (map.map[j, i] is Officer)
                        if (map.map[j, i].IsWhite) Console.Write("o");
                        else Console.Write("O");
                    if (map.map[j, i] is Rook)
                        if (map.map[j, i].IsWhite) Console.Write("r");
                        else Console.Write("R");
                    if (map.map[j, i] is Horse)
                        if (map.map[j, i].IsWhite) Console.Write("h");
                        else Console.Write("H");
                    if (map.map[j, i] is Queen)
                        if (map.map[j, i].IsWhite) Console.Write("q");
                        else Console.Write("Q");
                    if (map.map[j, i] is King)
                        if (map.map[j, i].IsWhite) Console.Write("k");
                        else Console.Write("K");
                }
                Console.Write("+");
                Console.WriteLine();
            }
            for (int i = 0; i < 10; i++)
            {
                Console.Write("+");
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
