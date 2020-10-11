using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chassAnaliz
{
    public class Rook : Figure
    {
        public Point Position { get;private set; }

        public bool IsWhite { get; private set; }

        public int Cast { get => -5; }

        public bool first = true;

        public Rook(int i, int j, bool b)
        {
            IsWhite = b;
            Position = new Point(i, j);
        }

        public Rook(int i, int j, bool b, bool f)
        {
            first = f;
            IsWhite = b;
            Position = new Point(i, j);
        }

        public IEnumerable<Step> GetSteps(Map map)
        {
            for (int i = 1; i < 8; i++)
            {
                int posX = Position.X + i;
                if (!map.CheckCorrect(new Point(posX, Position.Y)))
                {
                    break;
                }
                if (map.map[posX, Position.Y] != null)
                {
                    if (map.WhiteFigures.Contains(map.map[posX, Position.Y]))
                    {
                        if (IsWhite) break;
                        yield return new Step(Position, posX, Position.Y);
                        break;
                    }
                    if (IsWhite)
                        yield return new Step(Position, posX, Position.Y);
                    break;
                }
                yield return new Step(Position, posX, Position.Y);
            }
            for (int i =-1; i > -8; i--)
            {
                int posX = Position.X + i;
                if (!map.CheckCorrect(new Point(posX, Position.Y)))
                {
                    break;
                }
                if (map.map[posX, Position.Y] != null)
                {
                    if (map.WhiteFigures.Contains(map.map[posX, Position.Y]))
                    {
                        if (IsWhite) break;
                        yield return new Step(Position, posX, Position.Y);
                        break;
                    }
                    if (IsWhite)
                        yield return new Step(Position, posX, Position.Y);
                    break;
                }
                yield return new Step(Position, posX, Position.Y);
            }
            for (int i = 1; i < 8; i++)
            {
                int posY = Position.Y + i;
                if (!map.CheckCorrect(new Point(Position.X, posY)))
                {
                    break;
                }
                if (map.map[Position.X, posY] != null)
                {
                    if (map.WhiteFigures.Contains(map.map[Position.X, posY]))
                    {
                        if (IsWhite) break;
                        yield return new Step(Position, Position.X, posY);
                        break;
                    }
                    if (IsWhite)
                        yield return new Step(Position, Position.X, posY);
                    break;
                }
                yield return new Step(Position, Position.X, posY);
            }
            for (int i = -1; i > -8; i--)
            {
                int posY = Position.Y + i;
                if (!map.CheckCorrect(new Point(Position.X, posY)))
                {
                    break;
                }
                if (map.map[Position.X, posY] != null)
                {
                    if (map.WhiteFigures.Contains(map.map[Position.X, posY]))
                    {
                        if (IsWhite) break;
                        yield return new Step(Position, Position.X, posY);
                        break;
                    }
                    if (IsWhite)
                        yield return new Step(Position, Position.X, posY);
                    break;
                }
                yield return new Step(Position, Position.X, posY);
            }
        }
        
        public Figure GetFigure()
        {
            return new Rook(Position.X, Position.Y, IsWhite, first);
        }

        public void SetStep(Point pos)
        {
            first = false;
            Position = pos;
        }
    }
}
