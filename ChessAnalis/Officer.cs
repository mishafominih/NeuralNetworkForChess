using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chassAnaliz
{
    public class Officer:Figure
    {
        public Point Position { get; private set; }

        public bool IsWhite { get; private set; }

        public int Cast => -3;

        public Officer(int i, int j, bool b)
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
            for(int x = -8; x < 8; x++)
                for (int y = -8; y < 8; y++)
                {
                    if(Math.Abs(x) == Math.Abs(y) && x != 0)
                    {
                        yield return new Step(Position,
                            Position.X + x, Position.Y + y);
                    }
                }
        }

        public Figure GetFigure()
        {
            return new Officer(Position.X, Position.Y, IsWhite);
        }

        public void SetStep(Point pos)
        {
            Position = pos;
        }
    }
}
