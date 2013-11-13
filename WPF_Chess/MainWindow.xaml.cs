using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Chess.Tiles;
using System.Diagnostics;

namespace WPF_Chess
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            SetupPlayfield();
        }

        private void SetupPlayfield()
        {
            //Spawn all game tiles on their correct positions.

            #region CreateChessPieces

            ChessPieces = new List<IChessPiece>();

            ChessPieces.Add(new BlackRook(new Point(1, 0)));
            ChessPieces.Add(new BlackKnight(new Point(2, 0)));
            ChessPieces.Add(new BlackBishop(new Point(3, 0)));
            ChessPieces.Add(new BlackQueen(new Point(4, 0)));
            ChessPieces.Add(new BlackKing(new Point(5, 0)));
            ChessPieces.Add(new BlackBishop(new Point(6, 0)));
            ChessPieces.Add(new BlackKnight(new Point(7, 0)));
            ChessPieces.Add(new BlackRook(new Point(8, 0)));

            ChessPieces.Add(new BlackPawn(new Point(1, 1)));
            ChessPieces.Add(new BlackPawn(new Point(2, 1)));
            ChessPieces.Add(new BlackPawn(new Point(3, 1)));
            ChessPieces.Add(new BlackPawn(new Point(4, 1)));
            ChessPieces.Add(new BlackPawn(new Point(5, 1)));
            ChessPieces.Add(new BlackPawn(new Point(6, 1)));
            ChessPieces.Add(new BlackPawn(new Point(7, 1)));
            ChessPieces.Add(new BlackPawn(new Point(8, 1)));

            ChessPieces.Add(new WhitePawn(new Point(1, 6)));
            ChessPieces.Add(new WhitePawn(new Point(2, 6)));
            ChessPieces.Add(new WhitePawn(new Point(3, 6)));
            ChessPieces.Add(new WhitePawn(new Point(4, 6)));
            ChessPieces.Add(new WhitePawn(new Point(5, 6)));
            ChessPieces.Add(new WhitePawn(new Point(6, 6)));
            ChessPieces.Add(new WhitePawn(new Point(7, 6)));
            ChessPieces.Add(new WhitePawn(new Point(8, 6)));

            ChessPieces.Add(new WhiteRook(new Point(1, 7)));
            ChessPieces.Add(new WhiteKnight(new Point(2, 7)));
            ChessPieces.Add(new WhiteBishop(new Point(3, 7)));
            ChessPieces.Add(new WhiteQueen(new Point(4, 7)));
            ChessPieces.Add(new WhiteKing(new Point(5, 7)));
            ChessPieces.Add(new WhiteBishop(new Point(6, 7)));
            ChessPieces.Add(new WhiteKnight(new Point(7, 7)));
            ChessPieces.Add(new WhiteRook(new Point(8, 7)));

            #endregion

            foreach (var piece in ChessPieces)
            {
                Label lblPiece = new Label();
                lblPiece.Content = piece.ImageString;
                lblPiece.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                lblPiece.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                lblPiece.FontSize = 30;

                switch (piece.PieceColor)
                {
                    case PieceColor.Black:
                        lblPiece.Foreground = Brushes.Black;
                        break;
                    case PieceColor.White:
                        lblPiece.Foreground = Brushes.White;
                        break;
                    default:
                        break;
                }
                

                Grid.SetColumn(lblPiece, Convert.ToInt32(piece.Position.X));
                Grid.SetRow(lblPiece, Convert.ToInt32(piece.Position.Y));

                lblPiece.MouseLeftButtonUp += lblPiece_MouseLeftButtonUp;

                PieceLayer.Children.Add(lblPiece);
            }
        }

        #endregion

        #region Properties

        private List<IChessPiece> ChessPieces;
        private IChessPiece HighlightedChessPiece;

        #endregion

        #region Functions

        #region UserInput

        void lblPiece_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Highlight selected piece, then highlight all possible moves.
            
            Label lbl = sender as Label;
            int col = Grid.GetColumn(lbl);
            int row = Grid.GetRow(lbl);
            IChessPiece piece = ChessPieces.FirstOrDefault(p => p.Position.X == col && p.Position.Y == row);

            if (piece != null)
            {
                Debug.WriteLine("Clicked on {0}{1}.", piece.ImageString, piece.PieceType);

                if (!piece.IsHighlighted)
                {
                    if (HighlightedChessPiece == null)
                    {
                        HighlightPiece(piece);
                        HighlightMoves(piece);
                        piece.IsHighlighted = true;
                        HighlightedChessPiece = piece;
                    }
                }
                else
                {
                    RemoveHighlights(piece);
                }
            }
            
        }

        void highlight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            int col = Grid.GetColumn(rect);
            int row = Grid.GetRow(rect);
            
            if(HighlightedChessPiece != null)
            {
                MovePiece(HighlightedChessPiece, new Point(col, row));
            }
        }

        void highlightPiece_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (HighlightedChessPiece != null)
            {
                RemoveHighlights(HighlightedChessPiece);
            }
        }

        #endregion

        private void MovePiece(IChessPiece piece, Point targetPosition)
        {
            //Select piece from PieceLayer and move it to targetPosition.
            Label lblPiece = PieceLayer.Children.OfType<Label>().FirstOrDefault(lbl => Grid.GetColumn(lbl) == piece.Position.X && Grid.GetRow(lbl) == piece.Position.Y);

            IChessPiece targetPiece = ChessPieces.FirstOrDefault(p => p.Position.X == targetPosition.X && p.Position.Y == targetPosition.Y);
            if (targetPiece != null)
            {
                if (targetPiece.PieceColor != piece.PieceColor)
                {
                    Label lblTargetPiece = PieceLayer.Children.OfType<Label>().FirstOrDefault(lbl => Grid.GetColumn(lbl) == targetPiece.Position.X && Grid.GetRow(lbl) == targetPiece.Position.Y);
                    PieceLayer.Children.Remove(lblTargetPiece);
                    ChessPieces.Remove(targetPiece);
                }
            }

            piece.Position = targetPosition;
            piece.HasBeenMoved = true;

            Grid.SetColumn(lblPiece, Convert.ToInt32(piece.Position.X));
            Grid.SetRow(lblPiece, Convert.ToInt32(piece.Position.Y));

            RemoveHighlights(piece);
        }

        private void RemovePiece(IChessPiece piece)
        {
            //Removes target piece from the PieceLayer.
        }

        private void AddPiece(IChessPiece piece, Point position)
        {
            //Spawns a piece on the PieceLayer at position.
        }

        private void HighlightPiece(IChessPiece piece)
        {
            Rectangle highlight = new Rectangle();
            highlight.Fill = Brushes.RoyalBlue;
            highlight.Opacity = 0.3;
            highlight.MouseLeftButtonUp += highlightPiece_MouseLeftButtonUp;
            Grid.SetColumn(highlight, Convert.ToInt32(piece.Position.X));
            Grid.SetRow(highlight, Convert.ToInt32(piece.Position.Y));

            HighlightLayer.Children.Add(highlight);
        }

        private void HighlightMoves(IChessPiece piece)
        {
            List<ChessMove> moves = piece.PossibleMoves;

            foreach (var move in piece.PossibleMoves)
            {
                Rectangle highlight = new Rectangle();
                highlight.Fill = Brushes.RoyalBlue;
                highlight.Opacity = 0.3;
                highlight.MouseLeftButtonUp += highlight_MouseLeftButtonUp;
                Grid.SetColumn(highlight, Convert.ToInt32(move.X));
                Grid.SetRow(highlight, Convert.ToInt32(move.Y));

                //Do not highlight if field is obstructed by friendly piece, highlight red if obstructed by enemy piece.
                IChessPiece obstruction = ChessPieces.FirstOrDefault(p => p.Position.X == move.X && p.Position.Y == move.Y);

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
                        highlight.Fill = Brushes.Red;

                        if (moves.FirstOrDefault(m => m.X == move.X && m.Y == move.Y && m.Direction == move.Direction) != null)
                        {
                            if (piece.PieceType == PieceType.Pawn)
                            {
                                switch (piece.PieceColor)
                                {
                                    case PieceColor.Black:
                                        if (move.Direction != MoveDirection.Down)
                                        {
                                            HighlightLayer.Children.Add(highlight);
                                        }
                                        break;
                                    case PieceColor.White:
                                        if (move.Direction != MoveDirection.Up)
                                        {
                                            HighlightLayer.Children.Add(highlight);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                HighlightLayer.Children.Add(highlight);
                            }
                        }

                        moves.RemoveAll(p => p.Direction == move.Direction);
                    }
                }
                else
                {
                    if (moves.FirstOrDefault(m => m.X == move.X && m.Y == move.Y && m.Direction == move.Direction) != null)
                    {
                        if (piece.PieceType == PieceType.Pawn)
                        {
                            switch (piece.PieceColor)
                            {
                                case PieceColor.Black:
                                    if (move.Direction == MoveDirection.Down)
                                    {
                                            HighlightLayer.Children.Add(highlight);
                                    }
                                    break;
                                case PieceColor.White:
                                    if (move.Direction == MoveDirection.Up)
                                    {
                                        HighlightLayer.Children.Add(highlight);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            HighlightLayer.Children.Add(highlight);
                        }
                    }
                }
            }
        }

        private void RemoveHighlights(IChessPiece piece)
        {
            List<UIElement> items = new List<UIElement>();

            foreach (UIElement highlight in HighlightLayer.Children)
            {
                items.Add(highlight);
            }

            foreach (UIElement item in items)
            {
                HighlightLayer.Children.Remove(item);
            }

            piece.IsHighlighted = false;
            HighlightedChessPiece = null;
        }

        #endregion
    }
}
