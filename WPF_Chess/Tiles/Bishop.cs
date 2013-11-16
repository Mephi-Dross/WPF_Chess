using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_Chess.Tiles
{
    public abstract class Bishop : IChessPiece
    {
        public PieceType PieceType
        {
            get { return PieceType.Bishop; }
        }

        public abstract PieceColor PieceColor { get; }
        public abstract string ImageString { get; }

        public Point Position { get; set; }
        public bool IsThrown { get; set; }
        public bool IsHighlighted { get; set; }
        public bool HasBeenMoved { get; set; }

        public List<ChessMove> PossibleMoves
        {
            get { return GetPossibleMoves(); }
        }

        private List<ChessMove> GetPossibleMoves()
        {
            List<ChessMove> possibleMoves = new List<ChessMove>();

            if (Position != null)
            {
                //Get positions this tile can move to and add to list.
                for (int i = 1; i < 8; i++)
                {
                    if (Position.X - i >= 1 && Position.X - i <= 8 && Position.Y - i >= 0 && Position.Y - i <= 7)
                    {
                        Point UpLeft = new Point(Position.X - i, Position.Y - i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)UpLeft.X, (int)UpLeft.Y), MoveDirection.UpLeft));
                    }

                    if (Position.X + i >= 1 && Position.X + i <= 8 && Position.Y - i >= 0 && Position.Y - i <= 7)
                    {
                        Point UpRight = new Point(Position.X + i, Position.Y - i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)UpRight.X, (int)UpRight.Y), MoveDirection.UpRight));
                    }

                    if (Position.X - i >= 1 && Position.X - i <= 8 && Position.Y + i >= 0 && Position.Y + i <= 7)
                    {
                        Point diagonalDownLeft = new Point(Position.X - i, Position.Y + i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)diagonalDownLeft.X, (int)diagonalDownLeft.Y), MoveDirection.DownLeft));
                    }

                    if (Position.X + i >= 1 && Position.X + i <= 8 && Position.Y + i >= 0 && Position.Y + i <= 7)
                    {
                        Point diagonalDownRight = new Point(Position.X + i, Position.Y + i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)diagonalDownRight.X, (int)diagonalDownRight.Y), MoveDirection.DownRight));
                    }
                }
            }

            return possibleMoves;
        }
    }

    public class WhiteBishop : Bishop
    {
        public WhiteBishop(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.White; }
        }

        public override string ImageString
        {
            get { return "♗"; }
        }
    }

    public class BlackBishop : Bishop
    {
        public BlackBishop(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.Black; }
        }

        public override string ImageString
        {
            get { return "♝"; }
        }
    }
}
