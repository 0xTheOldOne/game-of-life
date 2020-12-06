using System;
using System.Windows.Forms;

namespace GameOfLife
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the game of life
        /// </summary>
        /// <see cref="https://fr.wikipedia.org/wiki/Jeu_de_la_vie"/>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormGame());
        }
    }
}
