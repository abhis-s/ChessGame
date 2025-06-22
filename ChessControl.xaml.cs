using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using ChessGame.Pieces;

namespace ChessGame
{
    /// <summary>
    /// Interaction logic for ChessControl.xaml
    /// </summary>
    public partial class ChessControl : UserControl
    {
        private Board board;
        Button[,] buttons;
        private Coordinate selectedField = null;
        public event Action OnBoardChanged;

        public Board BoardInstance
        {
            get => board;
            set
            {
                board = value;
                drawPieces();
            }
        }
        public ChessControl()
        {
            InitializeComponent();
            UIElementCollection chlds = theGrid.Children;
            buttons = new Button[8, 8];
            foreach (UIElement item in chlds)
            {
                if (item is Button b)
                    buttons[Grid.GetColumn(b) - 1, Grid.GetRow(b) - 1] = b;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int x = Grid.GetColumn(sender as UIElement) - 1;
            int y = Grid.GetRow(sender as UIElement) - 1;

            Coordinate clicked = new Coordinate(x, 7 - y);
            drawBackground();

            System.Diagnostics.Debug.WriteLine(String.Format("x: {0}, y: {1}", x, y));
            System.Diagnostics.Debug.WriteLine(clicked);

            if (selectedField == null)
            {
                Piece piece = board.fields[clicked.X, clicked.Y];
                if (piece != null && piece.Color == board.OnTurn)
                {
                    selectedField = clicked;

                    buttons[selectedField.X, 7 - selectedField.Y].Background = Brushes.LightGreen; // selected = green

                    Coordinate[] moves = piece.PossibleMoves(board, clicked);
                    markFields(moves);
                }
            }
            else
            {
                if (board.Move(selectedField, clicked)) {
                    drawPieces();
                    OnBoardChanged?.Invoke();
                }
                selectedField = null;
            }
        }

        private void drawPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button btn = buttons[i, j];
                    btn.Content = null;

                    Piece piece = board.fields[i, 7 - j];
                    if (piece == null)
                        continue;
                    bool isWhite = piece.Color == ChessColor.White;

                    switch (piece)
                    {
                        case Pawn:
                            btn.Content = isWhite ? "♙" : "♟";
                            break;
                        case Rook:
                            btn.Content = isWhite ? "♖" : "♜";
                            break;
                        case Knight:
                            btn.Content = isWhite ? "♘" : "♞";
                            break;
                        case Bishop:
                            btn.Content = isWhite ? "♗" : "♝";
                            break;
                        case Queen:
                            btn.Content = isWhite ? "♕" : "♛";
                            break;
                        case King:
                            btn.Content = isWhite ? "♔" : "♚";
                            break;
                        default:
                            break;
                    }

                    btn.FontFamily = new FontFamily("Segoe UI Symbol");
                    btn.FontSize = 60;
                }
            }
        }
        private void drawBackground()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button btn = buttons[i, j];
                    if ((i + j) % 2 == 0)
                        btn.Background = Brushes.LightGray;
                    else
                        btn.Background = Brushes.DarkGray;
                }
            }
        }
        private void markFields(Coordinate[] marked)
        {
            foreach (Coordinate c in marked)
            {
                if (!c.IsValid()) continue;

                Piece target = board.fields[c.X, c.Y];
                if (target != null && target.Color != board.fields[selectedField.X, selectedField.Y].Color)
                {
                    buttons[c.X, 7 - c.Y].Background = Brushes.IndianRed; // enemy = red
                }
                else
                {
                    buttons[c.X, 7 - c.Y].Background = Brushes.LightBlue;
                }
            }
        }
    }
}
