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
using WPF_Chess.GameRules;

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
            GameHelper.ChessPieces = new List<IChessPiece>();
            GameHelper.PlayedMoves = new List<ChessMove>();
            GameHelper.CurrentPlayer = PieceColor.White;
            HighlightedChessPiece = null;

            #region CreateChessPieces
            //Spawn all game tiles on their correct positions.

            AddPiece(new BlackRook(new Point(1, 0)));
            AddPiece(new BlackKnight(new Point(2, 0)));
            AddPiece(new BlackBishop(new Point(3, 0)));
            AddPiece(new BlackQueen(new Point(4, 0)));
            AddPiece(new BlackKing(new Point(5, 0)));
            AddPiece(new BlackBishop(new Point(6, 0)));
            AddPiece(new BlackKnight(new Point(7, 0)));
            AddPiece(new BlackRook(new Point(8, 0)));

            AddPiece(new BlackPawn(new Point(1, 1)));
            AddPiece(new BlackPawn(new Point(2, 1)));
            AddPiece(new BlackPawn(new Point(3, 1)));
            AddPiece(new BlackPawn(new Point(4, 1)));
            AddPiece(new BlackPawn(new Point(5, 1)));
            AddPiece(new BlackPawn(new Point(6, 1)));
            AddPiece(new BlackPawn(new Point(7, 1)));
            AddPiece(new BlackPawn(new Point(8, 1)));

            AddPiece(new WhitePawn(new Point(1, 6)));
            AddPiece(new WhitePawn(new Point(2, 6)));
            AddPiece(new WhitePawn(new Point(3, 6)));
            AddPiece(new WhitePawn(new Point(4, 6)));
            AddPiece(new WhitePawn(new Point(5, 6)));
            AddPiece(new WhitePawn(new Point(6, 6)));
            AddPiece(new WhitePawn(new Point(7, 6)));
            AddPiece(new WhitePawn(new Point(8, 6)));

            AddPiece(new WhiteRook(new Point(1, 7)));
            AddPiece(new WhiteKnight(new Point(2, 7)));
            AddPiece(new WhiteBishop(new Point(3, 7)));
            AddPiece(new WhiteQueen(new Point(4, 7)));
            AddPiece(new WhiteKing(new Point(5, 7)));
            AddPiece(new WhiteBishop(new Point(6, 7)));
            AddPiece(new WhiteKnight(new Point(7, 7)));
            AddPiece(new WhiteRook(new Point(8, 7)));

            #endregion
        }

        #endregion

        #region Properties

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
            IChessPiece piece = GameHelper.ChessPieces.FirstOrDefault(p => p.Position.X == col && p.Position.Y == row);

            if (piece != null)
            {
                Debug.WriteLine("Clicked on {0}{1}.", piece.ImageString, piece.PieceType);

                if (piece.PieceColor == GameHelper.CurrentPlayer)
                {
                    if (!piece.IsHighlighted)
                    {
                        if (HighlightedChessPiece == null)
                        {
                            HighlightPiece(piece);
                            HighlightMoves(piece);
                            //HighlightSpecialMoves(piece);

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

            IChessPiece targetPiece = GameHelper.ChessPieces.FirstOrDefault(p => p.Position.X == targetPosition.X && p.Position.Y == targetPosition.Y);
            if (targetPiece != null)
            {
                if (targetPiece.PieceColor != piece.PieceColor)
                {
                    RemovePiece(targetPiece);
                }
            }

            GameHelper.PlayedMoves.Add(new ChessMove(piece, piece.Position, targetPosition, MoveDirection.None));

            piece.Position = targetPosition;
            piece.HasBeenMoved = true;

            Grid.SetColumn(lblPiece, Convert.ToInt32(piece.Position.X));
            Grid.SetRow(lblPiece, Convert.ToInt32(piece.Position.Y));

            RemoveHighlights(piece);

            switch (piece.PieceColor)
            {
                case PieceColor.Black:
                    GameHelper.CurrentPlayer = PieceColor.White;
                    break;
                case PieceColor.White:
                    GameHelper.CurrentPlayer = PieceColor.Black;
                    break;
            }
        }

        private void RemovePiece(IChessPiece piece)
        {
            //Removes target piece from the PieceLayer.
            GameHelper.ChessPieces.Remove(piece);

            Label lblTargetPiece = PieceLayer.Children.OfType<Label>().FirstOrDefault(lbl => Grid.GetColumn(lbl) == piece.Position.X && Grid.GetRow(lbl) == piece.Position.Y);
            PieceLayer.Children.Remove(lblTargetPiece);

        }

        private void AddPiece(IChessPiece piece)
        {
            //Spawns a piece on the PieceLayer at position.
            GameHelper.ChessPieces.Add(piece);

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

        #region Visualization

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
            foreach (var move in GameHelper.GetPlayableMoves(piece))
            {
                Rectangle highlight = new Rectangle();
                highlight.Fill = Brushes.RoyalBlue;
                highlight.Opacity = 0.3;
                highlight.MouseLeftButtonUp += highlight_MouseLeftButtonUp;
                Grid.SetColumn(highlight, Convert.ToInt32(move.NewPosition.X));
                Grid.SetRow(highlight, Convert.ToInt32(move.NewPosition.Y));

                //Highlight red if obstructed by enemy piece.
                IChessPiece obstruction = GameHelper.ChessPieces.FirstOrDefault(p => p.Position.X == move.NewPosition.X && p.Position.Y == move.NewPosition.Y);
                if (obstruction != null)
                    highlight.Fill = Brushes.Red;

                HighlightLayer.Children.Add(highlight);
                
            }
        }

        private void HighlightSpecialMoves(IChessPiece piece)
        {
            List<ChessMove> specialMoves = piece.PossibleMoves.Where(p => p.SpecialMove != SpecialMoves.None).ToList();

            foreach (var move in GameHelper.GetPlayableMoves(piece))
            {
                Rectangle highlight = new Rectangle();
                highlight.Fill = Brushes.RoyalBlue;
                highlight.Opacity = 0.3;
                highlight.MouseLeftButtonUp += highlight_MouseLeftButtonUp;
                Grid.SetColumn(highlight, Convert.ToInt32(move.NewPosition.X));
                Grid.SetRow(highlight, Convert.ToInt32(move.NewPosition.Y));

                //Highlight red if obstructed by enemy piece.
                IChessPiece obstruction = GameHelper.ChessPieces.FirstOrDefault(p => p.Position.X == move.NewPosition.X && p.Position.Y == move.NewPosition.Y);
                if (obstruction != null)
                    highlight.Fill = Brushes.Red;

                HighlightLayer.Children.Add(highlight);
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

        #endregion
    }
}
