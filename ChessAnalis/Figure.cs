using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chassAnaliz
{
    public interface Figure
    {
        Point Position { get; }
        bool IsWhite { get; }
        IEnumerable<Step> GetSteps(Map map);
        int Cast { get; }
        Figure GetFigure();
        void SetStep(Point pos);
    }
}
