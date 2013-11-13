using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_Chess.Tiles
{
    public interface IChessPiece
    {
        PieceType PieceType { get; }
        PieceColor PieceColor { get; }
        string ImageString { get; }
        /// <summary>
        /// Position in format X=Column/Y=Row
        /// </summary>
        Point Position { get; set; }
        bool IsThrown { get; set; }
        bool IsHighlighted { get; set; }
        bool HasBeenMoved { get; set; }
        List<ChessMove> PossibleMoves { get; }
    }
}
