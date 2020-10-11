using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chassAnaliz
{
    public class Pawn : Figure
    {
        public Point Position { get; private set; }

        public bool IsWhite { get; private set; }

        public int Cast => -1;

        private int dir;

        bool first = true;
        public Pawn(int i, int j, bool b)
        {
            IsWhite = b;
            Position = new Point(i, j);
            if (IsWhite) dir = -1;
            else dir = 1;
        }

        public Pawn(int i, int j, bool b, bool f)
        {
            IsWhite = b;
            Position = new Point(i, j);
            if (IsWhite) dir = -1;
            else dir = 1;
            first = f;
        }

        public IEnumerable<Step> GetSteps(Map map)
        {
            return AllSteps(map).Where(x =>
            {
                if (!map.CheckCorrect(x.Position)) return false;
                if (Position.X != x.Position.X)
                {
                    if (map.map[x.Position.X, x.Position.Y] != null &&
                    map.map[x.Position.X, x.Position.Y].IsWhite != IsWhite)
                        return true;
                }
                else if (map.map[x.Position.X, x.Position.Y] == null &&
                    map.map[Position.X, Position.Y + dir] == null)
                        return true;
                return false;
            });
        }

        private IEnumerable<Step> AllSteps(Map map)
        {
            if (first)
            {
                yield return new Step(Position, Position.X,
         Position.Y + dir * 2);
                first = false;
            }
            yield return new Step(Position, Position.X,
                Position.Y + dir);
            if (Position.X != 7) yield return new Step(Position,
                 Position.X + 1, Position.Y + dir);
            if (Position.X != 0) yield return new Step(Position,
                 Position.X - 1, Position.Y + dir);
        }

        public Figure GetFigure()
        {
            return new Pawn(Position.X, Position.Y, IsWhite, first);
        }

        public void SetStep(Point pos)
        {
            first = false;
            Position = pos;
        }
    }
}
