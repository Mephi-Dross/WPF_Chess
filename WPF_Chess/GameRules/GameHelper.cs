using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF_Chess.Tiles;

namespace WPF_Chess.GameRules
{
    public static class GameHelper
    {
        public static List<IChessPiece> ChessPieces;
        public static List<ChessMove> PlayedMoves;
        public static PieceColor CurrentPlayer;

        public static bool IsTileObstructed(ChessMove move)
        {

            return false;
        }
    }
}
