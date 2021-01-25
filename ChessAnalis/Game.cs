using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chassAnaliz
{
    public class Game
    {
        const int countStep = 7;
        Map map;
        bool isWhiteHod = true;

        public Game()
        {
            var start = new string[]
            {
                "RHOQKOHR",
                "PPPPPPPP",
                "        ",
                "        ",
                "        ",
                "        ",
                "pppppppp",
                "rhoqkohr"
            };
            map = new Map(start);
        }

        public void Update(List<double> output)
        {
            if (map.EndFor(isWhiteHod)) return;
            var step = CorrectStep(output, isWhiteHod? map.WhiteFigures : map.BlackFigures);
            if (step == null) return;
            if (isWhiteHod)
            {
                SetStep(step, map, map.BlackFigures);
            }
            else
            {
                SetStep(step, map, map.WhiteFigures);
            }
            isWhiteHod = !isWhiteHod;
        }

        public int GetCountFigure(bool isWhite)
        {
            return isWhite ? map.WhiteFigures.Count() : map.BlackFigures.Count;
        }

        public List<double> GetInfo(bool isWhite)
        {
            var res = GetInfoFigures(isWhite ? map.WhiteFigures : map.BlackFigures);
            res.AddRange(GetInfoFigures(isWhite ? map.BlackFigures : map.WhiteFigures));
            return res;
        }

        private List<double> GetInfoFigures(List<Figure> figures)
        {
            var res = new List<double>();
            AddInfo(8, figures.Where(x => x is Pawn), res);
            AddInfo(2, figures.Where(x => x is Horse), res);
            AddInfo(2, figures.Where(x => x is Officer), res);
            AddInfo(2, figures.Where(x => x is Rook), res);
            AddInfo(1, figures.Where(x => x is Queen), res);
            AddInfo(1, figures.Where(x => x is King), res);
            return res;
        }

        private static void AddInfo(int count, IEnumerable<Figure> figures, List<double> res)
        {
            if (figures == null)
            {
                res.AddRange(Enumerable.Range(0, count).Select(x => -1.0));
                return;
            }
            var list = figures.ToList();
            for (int i = 0; i < count; i++)
            {
                if (list.Count > 0)
                {
                    res.Add(list[0].Position.X / 7.0);
                    res.Add(list[0].Position.Y / 7.0);
                    res.Add(list[0].Cast / 5.0);
                    list.RemoveAt(0);
                }
                else
                {
                    res.Add(-1);
                    res.Add(-1);
                    res.Add(-1);
                }
            }
        }

        private void SetStep(Step step, Map newMap, List<Figure> figures)
        {
            var f = newMap.map[step.PreviousPosition.X,
                                    step.PreviousPosition.Y];
            newMap.map[step.PreviousPosition.X,
                step.PreviousPosition.Y] = null;//подняли фигуру, которую двигаем

            var oldFigure = newMap.map[step.Position.X, step.Position.Y];//запоминаем срубленную фигуру
            if(oldFigure != null) 
                figures.Remove(oldFigure);

            newMap.map[step.Position.X,
                step.Position.Y] = f;//ставим
            f.SetStep(step.Position);
        }

        public void DrowMap(bool clear)
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

        private Step CorrectStep(List<double> step, List<Figure> figures)
        {
            var Px = aroundKoordinate(step[0]);
            var Py = aroundKoordinate(step[1]);
            var x = aroundKoordinate(step[2]);
            var y = aroundKoordinate(step[3]);
            for(int i = 0; i < 8; i++)
                foreach(var p in GetVariants(new Point(Px, Py), i))
                {
                    var figure = map.map[p.X, p.Y];
                    if (figure != null && figures.Contains(figure))
                        for (int j = 0; j < 8; j++)
                            foreach (var nP in GetVariants(new Point(x, y), j))
                            {
                                var steps = figure.GetSteps(map).ToList();
                                Step res = new Step(p, nP.X, nP.Y);
                                if (steps.Contains(res))
                                    return res;
                            }
                }
            return null;
        }

        private int aroundKoordinate(double k)
        {
            if (k < 0) return 0;
            return (int)Math.Round(k * 7, 0);
        }

        private List<Point> GetVariants(Point point, int lenght)
        {
            if (lenght == 0) return new List<Point> { point };
            var res = new List<Point>();
            for(int i = -lenght; i <= lenght; i++)
            {
                for(int j = -lenght; j <= lenght; j++)
                {
                    res.Add(new Point(point.X - i, point.Y - j));
                }
            }
            var removed = new List<Point>();
            for(int i = lenght - 1; i >= 0; i--)
            {
                foreach (var p in GetVariants(point, i))
                    removed.Add(p);
            }
            foreach(var p in removed)
            {
                res.Remove(p);
            }
            return res.Where(x => map.CheckCorrect(x)).ToList();
        }

    }
}
