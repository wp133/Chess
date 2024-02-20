using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SzachBib;

namespace Plansza
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whitesrc = new()
        {
            { PieceType.Pawn, LoadImage("Syf/PawnW.png") },
            { PieceType.Bishop, LoadImage("Syf/BishopW.png") },
            { PieceType.Knight, LoadImage("Syf/KnightW.png") },
            { PieceType.Rook, LoadImage("Syf/RookW.png") },
            { PieceType.Queen, LoadImage("Syf/QueenW.png") },
            { PieceType.King, LoadImage("Syf/KingW.png") }
        };
        private static readonly Dictionary<PieceType, ImageSource> blacksrc = new()
        {
            { PieceType.Pawn, LoadImage("Syf/PawnB.png") },
            { PieceType.Bishop, LoadImage("Syf/BishopB.png") },
            { PieceType.Knight, LoadImage("Syf/KnightB.png") },
            { PieceType.Rook, LoadImage("Syf/RookB.png") },
            { PieceType.Queen, LoadImage("Syf/QueenB.png") },
            { PieceType.King, LoadImage("Syf/KingB.png") }
        }; 
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }
       
        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.Black => blacksrc[type],
                Player.White => whitesrc[type],
                _ => null
            };
        }
        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }
            return GetImage(piece.Color, piece.Type);
        }
    }
}
