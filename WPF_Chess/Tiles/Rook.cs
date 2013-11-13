using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_Chess.Tiles
{
    public abstract class Rook : IChessPiece
    {
        public PieceType PieceType
        {
            get { return PieceType.Rook; }
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
                    if (Position.Y - i >= 0 && Position.Y - i <= 7)
                    {
                        Point up = new Point(Position.X, Position.Y - i);
                        possibleMoves.Add(new ChessMove((int)up.X, (int)up.Y, MoveDirection.Up));
                    }

                    if (Position.X - i >= 1 && Position.X - i <= 8)
                    {
                        Point left = new Point(Position.X - i, Position.Y);
                        possibleMoves.Add(new ChessMove((int)left.X, (int)left.Y, MoveDirection.Left));
                    }

                    if (Position.X + i >= 1 && Position.X + i <= 8)
                    {
                        Point right = new Point(Position.X + i, Position.Y);
                        possibleMoves.Add(new ChessMove((int)right.X, (int)right.Y, MoveDirection.Right));
                    }

                    if (Position.Y + i >= 0 && Position.Y + i <= 7)
                    {
                        Point down = new Point(Position.X, Position.Y + i);
                        possibleMoves.Add(new ChessMove((int)down.X, (int)down.Y, MoveDirection.Down));
                    }
                }
            }

            return possibleMoves;
        }
    }

    public class WhiteRook : Rook
    {
        public WhiteRook(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.White; }
        }

        public override string ImageString
        {
            get { return "♖"; }
        }
    }

    public class BlackRook : Rook
    {
        public BlackRook(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.Black; }
        }

        public override string ImageString
        {
            get { return "♜"; }
        }
    }
}
