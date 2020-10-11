using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chassAnaliz
{
    public class Horse : Figure
    {
        public Point Position { get; private set; }

        public bool IsWhite { get; private set; }

        public int Cast => -3;

        public Horse(int i, int j, bool b)
        {
            IsWhite = b;
            Position = new Point(i, j);
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
            yield return new Step(Position, Position.X + 1, Position.Y + 2);
            yield return new Step(Position, Position.X - 1, Position.Y + 2);
            yield return new Step(Position, Position.X + 2, Position.Y - 1);
            yield return new Step(Position, Position.X + 2, Position.Y + 1);
            yield return new Step(Position, Position.X - 2, Position.Y - 1);
            yield return new Step(Position, Position.X - 2, Position.Y + 1);
            yield return new Step(Position, Position.X - 1, Position.Y - 2);
            yield return new Step(Position, Position.X + 1, Position.Y - 2);
        }

        public Figure GetFigure()
        {
            return new Horse(Position.X, Position.Y, IsWhite);
        }

        public void SetStep(Point pos)
        {
            Position = pos;
        }
    }
}
