using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_Chess.Tiles
{
    public abstract class Knight : IChessPiece
    {
        public PieceType PieceType
        {
            get { return PieceType.Knight; }
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

                if (Position.X - 1 >= 1 && Position.Y - 2 >= 0)
                {
                    Point moveUpLeft = new Point(Position.X - 1, Position.Y - 2);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveUpLeft.X, (int)moveUpLeft.Y), MoveDirection.UpLeft));
                }

                if (Position.X + 1 <= 8 && Position.Y - 2 >= 0)
                {
                    Point moveUpRight = new Point(Position.X + 1, Position.Y - 2);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveUpRight.X, (int)moveUpRight.Y), MoveDirection.UpRight));
                }

                if (Position.X - 2 >= 1 && Position.Y - 1 >= 0)
                {
                    Point moveLeftUp = new Point(Position.X - 2, Position.Y - 1);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveLeftUp.X, (int)moveLeftUp.Y), MoveDirection.Up));
                }

                if (Position.X - 2 >= 1 && Position.Y + 1 <= 7)
                {
                    Point moveLeftDown = new Point(Position.X - 2, Position.Y + 1);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveLeftDown.X, (int)moveLeftDown.Y), MoveDirection.Down));
                }

                if (Position.X + 2 <= 8 && Position.Y - 1 >= 0)
                {
                    Point moveRightUp = new Point(Position.X + 2, Position.Y - 1);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveRightUp.X, (int)moveRightUp.Y), MoveDirection.Left));
                }

                if (Position.X + 2 <= 8 && Position.Y + 1 <= 7)
                {
                    Point moveRightDown = new Point(Position.X + 2, Position.Y + 1);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveRightDown.X, (int)moveRightDown.Y), MoveDirection.Right));
                }

                if (Position.X - 1 >= 1 && Position.Y + 2 <= 7)
                {
                    Point moveDownLeft = new Point(Position.X - 1, Position.Y + 2);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveDownLeft.X, (int)moveDownLeft.Y), MoveDirection.DownLeft));
                }

                if (Position.X + 1 <= 8 && Position.Y + 2 <= 7)
                {
                    Point moveDownRight = new Point(Position.X + 1, Position.Y + 2);
                    possibleMoves.Add(new ChessMove(this, this.Position, new Point((int)moveDownRight.X, (int)moveDownRight.Y), MoveDirection.DownRight));
                }
            }

            return possibleMoves;
        }
    }

    public class WhiteKnight : Knight
    {
        public WhiteKnight(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.White; }
        }

        public override string ImageString
        {
            get { return "♘"; }
        }
    }

    public class BlackKnight : Knight
    {
        public BlackKnight(Point spawnPosition)
        {
            Position = spawnPosition;
        }

        public override PieceColor PieceColor
        {
            get { return PieceColor.Black; }
        }

        public override string ImageString
        {
            get { return "♞"; }
        }
    }
}
