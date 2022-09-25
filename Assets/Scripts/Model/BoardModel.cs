using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace JGM.Model
{
    public class BoardModel
    {
        private readonly int[,] grid;
        private readonly int rows;
        private readonly int columns;

        public BoardModel(int rows, int columns)
        {
            Debug.Assert(rows > 2 && columns > 2);
            this.rows = rows;
            this.columns = columns;
            grid = new int[rows, columns];
            ClearCells();
        }

        public void ClearCells()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    grid[i, j] = -1;
                }
            }
        }

        public void SetCell(Vector2Int cell, int value)
        {
            Debug.Assert(cell.x < grid.GetLength(0) && cell.y < grid.GetLength(1));
            grid[cell.x, cell.y] = value;
        }

        public int GetCell(Vector2Int cell)
        {
            Debug.Assert(cell.x < grid.GetLength(0) && cell.y < grid.GetLength(1));
            return grid[cell.x, cell.y];
        }
    }
}