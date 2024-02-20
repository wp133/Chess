using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachBib
{
    public class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;
        public override Position FP { get; }
        public override Position TP { get; }
        public override Rep Back { get; set; }
        public NormalMove(Position from, Position to)
        {
            Rep Afd = new Rep(false, to, from);
            Back = Afd;
            FP = from;
            TP = to;
        }
        public override void Execute(Board board)
        {
            //throw new NotImplementedException();
            Piece piece = board[FP];


            if (piece.HasMoved == true)
            {
                Back.p1 = TP;
                Back.p2 = FP;
                Back.HasM = true;
            }
            piece.HasMoved = true;
            board[TP] = piece;
            board[FP] = null;
        }
    }
}
