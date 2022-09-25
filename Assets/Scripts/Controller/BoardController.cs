using JGM.Model;
using UnityEngine;

namespace JGM.Controller
{
    public class BoardController
    {
        private readonly BoardModel boardModel;

        public BoardController(int rows, int columns)
        {
            boardModel = new BoardModel(rows, columns);
        }

        public void SetCell(Vector2Int cell, int value)
        {
            boardModel.SetCell(cell, value);
        }

        public int GetPlayerTurn()
        {
            return 1;
        }

        public int GetCell(Vector2Int cell)
        {
            return boardModel.GetCell(cell);
        }
    }
}