using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chassAnaliz
{
    public struct Map
    {
        public List<Figure> WhiteFigures;
        public List<Figure> BlackFigures;
        public Figure[,] map;
        public const int size = 8;

        public Map(string[] m)
        {
            map = new Figure[size, size];
            WhiteFigures = new List<Figure>();
            BlackFigures = new List<Figure>();
            GetMap(m);
        }

        public Map(Map m)
        {
            map = new Figure[size, size];
            WhiteFigures = new List<Figure>();
            BlackFigures = new List<Figure>();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (m.map[i, j] != null)
                    {
                        var f = m.map[i, j].GetFigure();
                        map[i, j] = f;
                        if (m.map[i, j].IsWhite) WhiteFigures.Add(f);
                        else BlackFigures.Add(f);
                    }
        }

        public bool EndFor(bool isWhite)
        {
            if (isWhite)
            {
                if (WhiteFigures.Where(x => x is King).Count() == 0)
                    return true;
                return false;
            }
            if (BlackFigures.Where(x => x is King).Count() == 0)
                return true;
            return false;
        }

        public bool CheckCorrect(Point pos)
        {
            if (pos.X < 0 || pos.X > 7) return false;
            if (pos.Y < 0 || pos.Y > 7) return false;
            return true;
        }

        private void GetMap(string[] m)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    switch (m[j][i])
                    {
                        case (' '):
                            map[i, j] = null;
                            break;
                        case ('p'):
                            map[i, j] = new Pawn(i, j, true);
                            WhiteFigures.Add(map[i, j]);
                            break;
                        case ('P'):
                            map[i, j] = new Pawn(i, j, false);
                            BlackFigures.Add(map[i, j]);
                            break;
                        case ('r'):
                            map[i, j] = new Rook(i, j, true);
                            WhiteFigures.Add(map[i, j]);
                            break;
                        case ('R'):
                            map[i, j] = new Rook(i, j, false);
                            BlackFigures.Add(map[i, j]);
                            break;
                        case ('k'):
                            map[i, j] = new King(i, j, true, true);
                            WhiteFigures.Add(map[i, j]);
                            break;
                        case ('K'):
                            map[i, j] = new King(i, j, false, true);
                            BlackFigures.Add(map[i, j]);
                            break;
                        case ('q'):
                            map[i, j] = new Queen(i, j, true);
                            WhiteFigures.Add(map[i, j]);
                            break;
                        case ('Q'):
                            map[i, j] = new Queen(i, j, false);
                            BlackFigures.Add(map[i, j]);
                            break;
                        case ('o'):
                            map[i, j] = new Officer(i, j, true);
                            WhiteFigures.Add(map[i, j]);
                            break;
                        case ('O'):
                            map[i, j] = new Officer(i, j, false);
                            BlackFigures.Add(map[i, j]);
                            break;
                        case ('h'):
                            map[i, j] = new Horse(i, j, true);
                            WhiteFigures.Add(map[i, j]);
                            break;
                        case ('H'):
                            map[i, j] = new Horse(i, j, false);
                            BlackFigures.Add(map[i, j]);
                            break;
                    }
                }
            }//заполнение карты
        }
    }
}
