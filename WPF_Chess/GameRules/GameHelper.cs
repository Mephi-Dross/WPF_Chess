using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF_Chess.Tiles;
using System.Windows;

namespace WPF_Chess.GameRules
{
    public static class GameHelper
    {
        public static List<IChessPiece> ChessPieces;
        public static List<ChessMove> PlayedMoves;
        public static PieceColor CurrentPlayer;

        public static bool IsTileInCheck(IChessPiece king, Point targetPosition)
        {
            bool inCheck = false;

            foreach (var piece in ChessPieces.Where(cp => cp.PieceColor != king.PieceColor))
            {
                //Is position within the possible moves of the piece?
                ChessMove checkMove = piece.PossibleMoves.FirstOrDefault(pm => pm.NewPosition.X == targetPosition.X && pm.NewPosition.Y == targetPosition.Y);
                if (checkMove != null)
                {
                    //Remove all irrelevant moves.
                    List<ChessMove> moves = piece.PossibleMoves;
                    moves.RemoveAll(m => m.Direction != checkMove.Direction);

                    bool pathBlocked = false;
                    foreach (var move in moves)
                    {
                        //Check if path is obstructed
                        IChessPiece obstruction = GameHelper.ChessPieces.FirstOrDefault(p => p.Position.X == move.NewPosition.X && p.Position.Y == move.NewPosition.Y);
                        if (obstruction != null)
                        {
                            switch (move.Direction)
                            {
                                case MoveDirection.None:
                                    break;
                                case MoveDirection.UpLeft:
                                    if (move.NewPosition.X > checkMove.NewPosition.X && move.NewPosition.Y > checkMove.NewPosition.Y)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                                case MoveDirection.Up:
                                    if (move.NewPosition.Y > checkMove.NewPosition.Y)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                                case MoveDirection.UpRight:
                                    if (move.NewPosition.X < checkMove.NewPosition.X && move.NewPosition.Y > checkMove.NewPosition.Y)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                                case MoveDirection.Left:
                                    if (move.NewPosition.X > checkMove.NewPosition.X)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                                case MoveDirection.Right:
                                    if (move.NewPosition.X < checkMove.NewPosition.X)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                                case MoveDirection.DownLeft:
                                    if (move.NewPosition.X > checkMove.NewPosition.X && move.NewPosition.Y < checkMove.NewPosition.Y)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                                case MoveDirection.Down:
                                    if (move.NewPosition.Y < checkMove.NewPosition.Y)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                                case MoveDirection.DownRight:
                                    if (move.NewPosition.X < checkMove.NewPosition.X && move.NewPosition.Y < checkMove.NewPosition.Y)
                                    {
                                        if (obstruction != king)
                                        {
                                            pathBlocked = true;
                                        }
                                    }
                                    break;
                            }

                            if (pathBlocked)
                                break;
                        }
                    }

                    if (!pathBlocked)
                    {
                        inCheck = true;
                        break;
                    }
                }
            }


            return inCheck;
        }

        public static List<ChessMove> GetPlayableMoves(IChessPiece piece)
        {
            List<ChessMove> playableMoves = new List<ChessMove>();

            #region NormalMoves

            List<ChessMove> moves = piece.PossibleMoves.Where(p => p.SpecialMove == SpecialMoves.None).ToList();
            foreach (var move in piece.PossibleMoves.Where(p => p.SpecialMove == SpecialMoves.None))
            {
                IChessPiece obstruction = ChessPieces.FirstOrDefault(p => p.Position.X == move.NewPosition.X && p.Position.Y == move.NewPosition.Y);
                if (obstruction != null)
                {
                    if (obstruction.PieceColor == piece.PieceColor)
                    {
                        //Obstructed by friend
                        moves.RemoveAll(p => p.Direction == move.Direction);
                    }
                    else
                    {
                        //Obstructed by enemy
                        if (moves.Contains(move))
                            playableMoves.Add(move);

                        moves.RemoveAll(p => p.Direction == move.Direction);
                    }
                }
                else
                {
                    if(moves.Contains(move))
                        playableMoves.Add(move);
                }
            }

            #endregion

            #region SpecialMoves

            List<ChessMove> specialMoves = piece.PossibleMoves.Where(p => p.SpecialMove != SpecialMoves.None).ToList();
            foreach (var move in piece.PossibleMoves.Where(p => p.SpecialMove != SpecialMoves.None))
            {
                switch (piece.PieceType)
                {
                    case PieceType.King:
                        if (GameHelper.IsTileInCheck(piece, move.NewPosition))
                        {
                            specialMoves.Remove(move);
                        }
                        break;
                    case PieceType.Queen:
                        break;
                    case PieceType.Rook:
                        break;
                    case PieceType.Bishop:
                        break;
                    case PieceType.Knight:
                        break;
                    case PieceType.Pawn:
                        IChessPiece target = ChessPieces.FirstOrDefault(cp => cp.Position.X == move.NewPosition.X && cp.Position.Y == move.NewPosition.Y);
                        if (target == null)
                        {
                            specialMoves.Remove(move);
                        }
                        break;
                }

                IChessPiece obstruction = ChessPieces.FirstOrDefault(p => p.Position.X == move.NewPosition.X && p.Position.Y == move.NewPosition.Y);
                if (obstruction != null)
                {
                    if (obstruction.PieceColor == piece.PieceColor)
                    {
                        //Obstructed by friend
                        specialMoves.RemoveAll(p => p.Direction == move.Direction);
                    }
                    else
                    {
                        //Obstructed by enemy
                        if (specialMoves.Contains(move))
                            playableMoves.Add(move);

                        specialMoves.RemoveAll(p => p.Direction == move.Direction);
                    }
                }
                else
                {
                    if (specialMoves.Contains(move))
                        playableMoves.Add(move);
                }
            }



            #endregion

            return playableMoves;
        }
    }
}
