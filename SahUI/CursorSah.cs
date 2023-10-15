using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace SahUI
{
    public static class CursorSah
    {

        // foloseste 2 imagini de culori diferite pentru a determina a cui jucator este randul
        public static readonly Cursor CursorA = IncarcaCursor("Resurse/CursorA.cur");
        public static readonly Cursor CursorN = IncarcaCursor("Resurse/CursorN.cur");

        // clasa folosita pentru incarcarea imaginilor folosind un stream
        private static Cursor IncarcaCursor(string filePath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
