using UnityEngine;
using Debug = UnityEngine.Debug;

namespace JGM.Model
{
    public class BoardModel
    {
        public int Rows => rows;
        public int Columns => columns;

        private readonly int[,] grid;
        private readonly int rows;
        private readonly int columns;

        public BoardModel(int rows, int columns)
        {
            Debug.Assert(rows > 0 && columns > 0);
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

        public virtual void SetCell(Vector2Int coordinates, int value)
        {
            bool insideBounds = (coordinates.x < grid.GetLength(0) && coordinates.y < grid.GetLength(1));
            Debug.Assert(insideBounds);
            if (insideBounds)
            {
                grid[coordinates.x, coordinates.y] = value;
            }
        }

        public int GetCell(Vector2Int coordinates)
        {
            bool insideBounds = (coordinates.x < grid.GetLength(0) && coordinates.y < grid.GetLength(1));
            Debug.Assert(insideBounds);
            return insideBounds ? grid[coordinates.x, coordinates.y] : -1;
        }
    }
}