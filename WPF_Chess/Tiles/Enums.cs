using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_Chess.Tiles
{
    public enum PieceType
    {
        King,
        Queen, 
        Rook,
        Bishop,
        Knight,
        Pawn
    }

    public enum PieceColor
    {
        Black,
        White
    }

    public enum MoveDirection
    {
        None,
        UpLeft,
        Up,
        UpRight,
        Left,
        Right,
        DownLeft,
        Down,
        DownRight
    }
}
