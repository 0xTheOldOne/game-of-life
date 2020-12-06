using System;

namespace GameOfLife
{
    public class Game
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Cell[,] CurrentCells { get; set; }
        public Cell[,] NextCells { get; set; }

        public Game(int width = 10, int height = 10)
        {
            Width = width;
            Height = height;

            CurrentCells = new Cell[Width, Height];
            NextCells = new Cell[Width, Height];

            CurrentCells.Initialize(Width, Height);
        }

        public void Line()
        {
            CurrentCells[0, 1].Alive = true;
            CurrentCells[1, 1].Alive = true;
            CurrentCells[2, 1].Alive = true;
        }

        public void Randomize(double probability = 5)
        {
            var random = new Random();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (random.Next(0, 100) <= probability)
                    {
                        CurrentCells[x, y].Alive = true;
                    }
                }
            }
        }

        public void UpdateGame()
        {
            NextCells.Initialize(Width, Height);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int minX = x == 0 ? x : (x - 1);
                    int maxX = x == (Width - 1) ? x : (x + 1);
                    int minY = y == 0 ? y : (y - 1);
                    int maxY = y == (Height - 1) ? y : (y + 1);
                    int neighbourCount = 0;

                    for (int j = minY; j <= maxY; j++)
                    {
                        for (int i = minX; i <= maxX; i++)
                        {
                            neighbourCount += CurrentCells[i, j].Alive ? 1 : 0;
                        }
                    }

                    NextCells[x, y].Update(neighbourCount);
                }
            }

            Array.Copy(NextCells, CurrentCells, CurrentCells.Length);
        }
    }
}
