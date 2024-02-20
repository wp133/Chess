using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachBib
{
    internal class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        private readonly Direction forward;
        public Pawn(Player color) 
        {
            Color = color;
            if (color == Player.White)
            {
                forward = Direction.N;
            }
            else forward = Direction.S;
        }
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        private static bool CanMoveTo(Position pos, Board board)
        {
            return Board.IsInside(pos) && board.IsEmpty(pos);
        }
        private bool CanCaptureAt(Position pos, Board board)
        {
            if (board.IsEmpty(pos) || !Board.IsInside(pos))
            {
                return false;
            }
            //return true;
            return board[pos].Color != Color;
        }
        private IEnumerable<Move>ForwardMoves(Position from, Board board)
        {
            Position oneMovepos = from + forward;
            if(CanMoveTo(oneMovepos, board))
            {
                yield return new NormalMove(from, oneMovepos);
                Position twoMovespos = oneMovepos + forward;
                if(!HasMoved && CanMoveTo(twoMovespos, board))
                {
                    yield return new NormalMove(from, twoMovespos);
                }
            }
        }
        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
            foreach (Direction dir in new Direction[] {Direction.W, Direction.E})
            {
                Position to = from + dir + forward;
                if(Board.IsInside(to)&&CanCaptureAt(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }
        public override bool CanCaptureOppKing(Position from, Board board)
        {
            //if(Board.IsInside(from + Direction.NW) && Board.IsInside(from + Direction.NE) && Board.IsInside(from + Direction.SW) && Board.IsInside(from + Direction.SE))
                return DiagonalMoves(from,board).Any(move =>
                {
                    Piece piece = board[move.TP];
                    return piece != null && piece.Type == PieceType.King;
                });
        }
    }
}
