using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_Chess.Tiles
{
    public class ChessMove
    {
        public ChessMove(int x, int y, MoveDirection direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public MoveDirection Direction { get; set; }
    }
}
