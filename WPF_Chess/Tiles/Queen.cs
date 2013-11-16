using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_Chess.Tiles
{
    public abstract class Queen : IChessPiece
    {
        public PieceType PieceType
        {
            get { return PieceType.Queen; }
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
                    if (Position.X - i >= 1 && Position.Y - i >= 0)
                    {
                        Point moveUpLeft = new Point(Position.X - i, Position.Y - i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveUpLeft.X, (int)moveUpLeft.Y), MoveDirection.UpLeft));
                    }

                    if (Position.Y - i >= 0)
                    {
                        Point moveUp = new Point(Position.X, Position.Y - i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveUp.X, (int)moveUp.Y), MoveDirection.Up));
                    }

                    if (Position.X + i <= 8 && Position.Y - i >= 0)
                    {
                        Point moveUpRight = new Point(Position.X + i, Position.Y - i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveUpRight.X, (int)moveUpRight.Y), MoveDirection.UpRight));
                    }

                    if (Position.X - i >= 1)
                    {
                        Point moveLeft = new Point(Position.X - i, Position.Y);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveLeft.X, (int)moveLeft.Y), MoveDirection.Left));
                    }

                    if (Position.X + i <= 8)
                    {
                        Point moveRight = new Point(Position.X + i, Position.Y);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveRight.X, (int)moveRight.Y), MoveDirection.Right));
                    }

                    if (Position.X - i >= 1 && Position.Y + i <= 7)
                    {
                        Point moveDownLeft = new Point(Position.X - i, Position.Y + i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveDownLeft.X, (int)moveDownLeft.Y), MoveDirection.DownLeft));
                    }

                    if (Position.Y + i <= 7)
                    {
                        Point moveDown = new Point(Position.X, Position.Y + i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveDown.X, (int)moveDown.Y), MoveDirection.Down));
                    }

                    if (Position.X + i <= 8 && Position.Y + i <= 7)
                    {
                        Point moveDownRight = new Point(Position.X + i, Position.Y + i);
                        possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveDownRight.X, (int)moveDownRight.Y), MoveDirection.DownRight));
                    }
                }
            }

            return possibleMoves;
        }
    }

    public class WhiteQueen : Queen
    {
        public WhiteQueen(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.White; }
        }

        public override string ImageString
        {
            get { return "♕"; }
        }
    }

    public class BlackQueen : Queen
    {
        public  BlackQueen(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.Black; }
        }

        public override string ImageString
        {       
            get { return "♛"; }
        }
    }
}
