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
using SzachBib;

namespace Plansza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position, Move> moveCache = new Dictionary<Position, Move>();
        private GS gamestate;
        private Position selectedPos = null;
        public MainWindow()
        {
            InitializeComponent();
            Init();
            gamestate = new GS(Player.White, Board.Initial());
            DrawBoard(gamestate.B);
            SetCursor(gamestate.CurrentPlayer);
        }
        private void Init()
        {
            for (int i = 0; i < 8; i++) 
            {
                for(int j = 0; j < 8; j++)
                {
                    Image image = new Image();
                    pieceImages[i, j] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[i,j] = highlight;
                    HighlightsGrid.Children.Add(highlight); 

                }
            }
        }
        private void DrawBoard(Board board)
        {
            for(int i = 0;i < 8;i++)
            {
                for(int k = 0;k < 8;k++)
                {
                    Piece piece = board[i, k];
                    pieceImages[i, k].Source = Images.GetImage(piece);
                }
            }
        }
        private void BoardGrid_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSqaurePosition(point);
            if(selectedPos == null) 
            {
                OneFromPositionSelected(pos);
            }
            else
            {
                OneToPositionSelected(pos);
            }
        }
        private Position ToSqaurePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position(row, col);
        }
        private void OneFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gamestate.MovesForPiece(pos);
            if(moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }
        private void OneToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHighlights();
            if(moveCache.TryGetValue(pos, out Move move))
            {
                HandleMove(move);
            }
        }
        private void HandleMove(Move move)
        {
            gamestate.MakeMove(move);
            DrawBoard(gamestate.B);
            SetCursor(gamestate.CurrentPlayer);
        }
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach (Move move in moves)
            {
                moveCache[move.TP] = move;
            }
        }
        private void ShowHighlights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);
            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);    
            }
        }
        private void HideHighlights()
        {
            //Color color = Color.FromArgb(150, 125, 255, 125);
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }

        private void SetCursor(Player player)
        {
            if (player == Player.White)
                Cursor = ChessCursor.WhiteC;
            else Cursor = ChessCursor.BlackC;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Z)
            { 
                Move NN = new NormalMove(GS.CopycatRep.p1, GS.CopycatRep.p2);
                gamestate.MakeMove(NN);
                DrawBoard(gamestate.B);
                SetCursor(gamestate.CurrentPlayer);
            }
        }
    }
}
