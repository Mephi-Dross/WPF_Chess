using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_Chess.Tiles
{
    public abstract class King : IChessPiece
    {
        public PieceType PieceType
        {
            get { return PieceType.King; }
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

                if (Position.X - 1 >= 0 && Position.Y - 1 >= 0)
                {
                    Point moveUpLeft = new Point(Position.X - 1, Position.Y - 1);
                    possibleMoves.Add(new ChessMove((int)moveUpLeft.X, (int)moveUpLeft.Y, MoveDirection.UpLeft));
                }

                if (Position.Y - 1 >= 0)
                {
                    Point moveUp = new Point(Position.X, Position.Y - 1);
                    possibleMoves.Add(new ChessMove((int)moveUp.X, (int)moveUp.Y, MoveDirection.Up));
                }

                if (Position.X + 1 <= 8 && Position.Y - 1 >= 0)
                {
                    Point moveUpRight = new Point(Position.X + 1, Position.Y - 1);
                    possibleMoves.Add(new ChessMove((int)moveUpRight.X, (int)moveUpRight.Y, MoveDirection.UpRight));
                }

                if (Position.X - 1 >= 0)
                {
                    Point moveLeft = new Point(Position.X - 1, Position.Y);
                    possibleMoves.Add(new ChessMove((int)moveLeft.X, (int)moveLeft.Y, MoveDirection.Left));
                }

                if (Position.X + 1 <= 8)
                {
                    Point moveRight = new Point(Position.X + 1, Position.Y);
                    possibleMoves.Add(new ChessMove((int)moveRight.X, (int)moveRight.Y, MoveDirection.Right));
                }


                if (Position.X - 1 >= 0 && Position.Y + 1 <= 7)
                {
                    Point moveDownLeft = new Point(Position.X - 1, Position.Y + 1);
                    possibleMoves.Add(new ChessMove((int)moveDownLeft.X, (int)moveDownLeft.Y, MoveDirection.DownLeft));
                }

                if (Position.Y + 1 <= 7)
                {
                    Point moveDown = new Point(Position.X, Position.Y + 1);
                    possibleMoves.Add(new ChessMove((int)moveDown.X, (int)moveDown.Y, MoveDirection.Down));
                }

                if (Position.X + 1 <= 8 && Position.Y + 1 <= 7)
                {
                    Point moveDownRight = new Point(Position.X + 1, Position.Y + 1);
                    possibleMoves.Add(new ChessMove((int)moveDownRight.X, (int)moveDownRight.Y, MoveDirection.DownRight));
                }
            }

            return possibleMoves;
        }
    }

    public class WhiteKing : King
    {
        public WhiteKing(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.White; }
        }

        public override string ImageString
        {
            get { return "♔"; }
        }
    }

    public class BlackKing : King
    {
        public BlackKing(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.Black; }
        }

        public override string ImageString
        {
            get { return "♚"; }
        }
    }
}
