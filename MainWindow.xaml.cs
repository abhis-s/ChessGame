using Microsoft.Win32;
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

namespace ChessGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChessControl.BoardInstance = Board.standardBoard();
            UpdateTurnLabel();
            ChessControl.OnBoardChanged += UpdateTurnLabel;
        }

        private void UpdateTurnLabel()
        {
            TurnLabel.Text = $"Turn: {ChessControl.BoardInstance.OnTurn}";
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            ChessControl.BoardInstance = Board.standardBoard();
            UpdateTurnLabel();
        }

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "ChessBoards|*.xml"
            };

            if (dialog.ShowDialog() == true)
            {
                Board.SaveBoard(ChessControl.BoardInstance, dialog.FileName);
            }
        }

        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "ChessBoards|*.xml"
            };

            if (dialog.ShowDialog() == true)
            {
                Board loaded = Board.LoadBoard(dialog.FileName);
                ChessControl.BoardInstance = loaded;
                UpdateTurnLabel();
            }
        }
    }
}