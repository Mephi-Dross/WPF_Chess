using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_Chess.Tiles
{
    public class ChessMove
    {
        public ChessMove()
        {

        }

        public ChessMove(IChessPiece piece, Point oldPosition, Point newPosition, MoveDirection direction, SpecialMoves specialMove = SpecialMoves.None)
        {
            Piece = piece;
            OldPosition = oldPosition;
            NewPosition = newPosition;
            Direction = direction;
            SpecialMove = specialMove;
        }

        public IChessPiece Piece { get; set; }
        public Point OldPosition { get; set; }
        public Point NewPosition { get; set; }
        public MoveDirection Direction { get; set; }
        public SpecialMoves SpecialMove { get; set; }
    }
}
