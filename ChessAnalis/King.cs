using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chassAnaliz
{
    public class King : Figure
    {
        public Point Position { get; private set; }

        public bool IsWhite { get; private set; }

        private bool first = true;

        public int Cast => -10;

        public King(int i, int j, bool b, bool f)
        {
            IsWhite = b;
            Position = new Point(i, j);
            first = f;
        }

        public IEnumerable<Step> GetSteps(Map map)
        {
            return AllSteps(map).Where(x =>
            {
                if (!map.CheckCorrect(x.Position)) return false;
                if (map.map[x.Position.X, x.Position.Y] != null
                && map.map[x.Position.X, x.Position.Y].IsWhite == IsWhite) return false;
                return true;
            });
        }

        private IEnumerable<Step> AllSteps(Map map)
        {
            yield return new Step(Position, Position.X + 1, Position.Y + 1);
            yield return new Step(Position, Position.X + 1, Position.Y);
            yield return new Step(Position, Position.X + 1, Position.Y - 1);
            yield return new Step(Position, Position.X, Position.Y + 1);
            yield return new Step(Position, Position.X, Position.Y - 1);
            yield return new Step(Position, Position.X - 1, Position.Y + 1);
            yield return new Step(Position, Position.X - 1, Position.Y);
            yield return new Step(Position, Position.X - 1, Position.Y - 1);
        }

        public Figure GetFigure()
        {
            return new King(Position.X, Position.Y, IsWhite, first);
        }

        public void SetStep(Point pos)
        {
            first = false;
            Position = pos;
        }
    }
}
