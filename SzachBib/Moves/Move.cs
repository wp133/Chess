using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachBib
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Position FP { get; }
        public abstract Position TP { get; }
        public abstract Rep Back { get; set; }
        public abstract void Execute(Board board);
        
        public virtual bool IsLegal(Board board)
        {
            Player player = board[FP].Color;
            Board boardcopy = board.Copy();
            Execute(boardcopy);
            return !boardcopy.IsInCheck(player);
        }
    }
}
