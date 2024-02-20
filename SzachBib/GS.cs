using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachBib
{
    public class GS
    {
        public Board B { get; }
        public Player CurrentPlayer { get; private set; }
        public Result R { get; private set; } = null;
        public static Rep CopycatRep { get; set; }
        public GS(Player p, Board b) 
        { 
            CurrentPlayer = p; 
            B = b;
        }
        public IEnumerable<Move> MovesForPiece(Position pos)
        {
            if(B.IsEmpty(pos) || B[pos].Color != CurrentPlayer)
                return Enumerable.Empty<Move>();
            Piece piece = B[pos];
            //return piece.GetMoves(pos, B);
            IEnumerable<Move> movecandidates = piece.GetMoves(pos, B);
            return movecandidates.Where(move => move.IsLegal(B));
        }
        public void MakeMove(Move move)
        {
            CopycatRep = move.Back;
            move.Execute(B);
            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver();
        }
        public void MakeReverse(Move move)
        {
            CopycatRep = move.Back;
            move.Execute(B);
            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver();
        }

        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = B.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = B[pos];
                return piece.GetMoves(pos, B);
            });
            return moveCandidates.Where(move => move.IsLegal(B));
        }
        private void CheckForGameOver()
        {
            if(!AllLegalMovesFor(CurrentPlayer).Any())
            {
                if (B.IsInCheck(CurrentPlayer))
                    R = Result.Win(CurrentPlayer.Opponent());
                else R = Result.Draw(EndReason.Stalemate);
            }
        }
        public bool IsGameOver()
        {
            return R != null;
        }
    }
}
