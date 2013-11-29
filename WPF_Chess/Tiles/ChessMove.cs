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

        public override bool Equals(object obj)
        {
            if(!(obj is ChessMove))
                return false;

            if (((ChessMove)obj).Piece != this.Piece)
                return false;

            if (((ChessMove)obj).OldPosition != null)
            {
                if (this.OldPosition == null)
                    return false;
                if (((ChessMove)obj).OldPosition.X != this.OldPosition.X)
                    return false;
                if (((ChessMove)obj).OldPosition.Y != this.OldPosition.Y)
                    return false;
            }

            if (((ChessMove)obj).NewPosition != null)
            {
                if (this.NewPosition == null)
                    return false;
                if (((ChessMove)obj).NewPosition.X != this.NewPosition.X)
                    return false;
                if (((ChessMove)obj).NewPosition.Y != this.NewPosition.Y)
                    return false;
            }

            if (((ChessMove)obj).Direction != this.Direction)
                return false;

            if (((ChessMove)obj).SpecialMove != this.SpecialMove)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
