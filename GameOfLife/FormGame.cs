using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class FormGame : Form
    {
        private readonly string title;

        public Game CurrentGame { get; set; }

        public FormGame()
        {
            InitializeComponent();

            AssemblyName asmName = Assembly.GetExecutingAssembly().GetName();
            title = $"{asmName.Name} - {asmName.Version}";

            void drawGame()
            {
                panelGame.Refresh();
                System.Threading.Thread.Sleep(500);
            }

            Load += (s, e) =>
            {
                Text = title;
                nudGeneration.Value = 10;
                nudWidth.Value = 100;
                nudHeight.Value = 100;
                nudProbability.Value = 5;
            };

            nudWidth.ValueChanged += (s, e) =>
            {
                CurrentGame = new Game((int)nudWidth.Value, (int)nudHeight.Value);
            };

            nudHeight.ValueChanged += (s, e) =>
            {
                CurrentGame = new Game((int)nudWidth.Value, (int)nudHeight.Value);
            };

            buttonRandomize.Click += (s, e) =>
            {
                CurrentGame = new Game((int)nudWidth.Value, (int)nudHeight.Value);
                CurrentGame.Randomize((int)nudProbability.Value);
                drawGame();
            };

            buttonRun.Click += (s, e) =>
            {
                panelControls.Enabled = !buttonRun.Enabled;

                if (CurrentGame == null)
                {
                    CurrentGame = new Game((int)nudWidth.Value, (int)nudHeight.Value);
                    CurrentGame.Line();
                    drawGame();
                }

                for (int i = 0; i < nudGeneration.Value; i++)
                {
                    Text = $"{title} - Generation #{i + 1}";
                    CurrentGame.UpdateGame();
                    drawGame();
                }

                panelControls.Enabled = !buttonRun.Enabled;
            };

            panelGame.Paint += (s, e) =>
            {
                int cellSize = 4;

                if (CurrentGame != null)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;

                    using (var backColorBrush = new SolidBrush(panelGame.BackColor))
                    {
                        for (int y = 0; y < CurrentGame.Height; y++)
                        {
                            for (int x = 0; x < CurrentGame.Width; x++)
                            {
                                Brush brush = backColorBrush;

                                if (CurrentGame.CurrentCells[x, y].Alive)
                                {
                                    brush = Brushes.Lime;
                                }

                                e.Graphics.FillRectangle(brush, new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize));
                            }
                        }
                    }
                }
            };
        }
    }
}
