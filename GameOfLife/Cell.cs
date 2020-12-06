using System;

namespace GameOfLife
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int NeighbourCount { get; set; }
        public bool Alive { get; set; }

        public void Update(int neighbourCount)
        {
            Alive = neighbourCount == 3 || (Alive && neighbourCount == 2);
            NeighbourCount = neighbourCount;
        }
    }

    public static class CellsExtensionMethods
    {
        public static void Initialize(this Cell[,] cells, int width, int height)
        {
            var table = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    table[x, y] = new Cell();
                }
            }

            Array.Copy(table, cells, table.Length);
        }
    }
}
