using ChessAnalis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chassAnaliz
{
    public class Step
    {
        public Point PreviousPosition;
        public Point Position;
        public int RandomNumber { get; set; }
        public Step(Point PrevPos, int NextX, int NextY)
        {
            PreviousPosition = PrevPos;
            Position = new Point(NextX, NextY);
            RandomNumber = Rand.GetRandom();
        }

        public Step(Point PrevPos, int NextX, int NextY, int n)
        {
            PreviousPosition = PrevPos;
            Position = new Point(NextX, NextY);
            RandomNumber = n;
        }
    }
}
