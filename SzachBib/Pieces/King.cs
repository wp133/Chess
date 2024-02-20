using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachBib
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        public static readonly Direction[] dirs = new Direction[]
        {
            Direction.NW, Direction.NE, Direction.SW,Direction.SE, Direction.N, Direction.E, Direction.W, Direction.S
        };
        public King(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        private IEnumerable<Position> MovePositions(Position from, Board board)
        {
            foreach(Direction dir in dirs)
            {
                Position to = from + dir;
                if(!Board.IsInside(to))
                {
                    continue;
                }
                if (board.IsEmpty(to) || board[to].Color != Color)
                {
                    yield return to;
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach(Position to in  MovePositions(from, board))
            {
                yield return new NormalMove(from, to);
            }
        }
        public override bool CanCaptureOppKing(Position from, Board board)
        {
            return MovePositions(from, board).Any(to =>
            {
                Piece piece = board[to];
                return piece != null&& piece.Type == PieceType.King;
            });
        }
    }
}
