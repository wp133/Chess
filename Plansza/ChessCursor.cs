using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Plansza
{
    public static class ChessCursor
    {
        public static readonly Cursor WhiteC = LoadCursor("Syf/CursorW.cur");
        public static readonly Cursor BlackC = LoadCursor("Syf/CursorB.cur");
        private static Cursor LoadCursor (string filePath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
            return new Cursor (stream, true);
        }
    }
}
