using JGM.Controller;
using UnityEngine;

namespace JGM.View
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Vector2Int boardSize = Vector2Int.one * 3;
        [SerializeField] private float cellSize = 80;
        
        private BoardController boardController;

        public void Awake()
        {
            boardController = new BoardController(boardSize.x, boardSize.y);
        }

        private void OnGUI()
        {
            for (int i = 0; i < boardSize.x; i++)
            {
                for (int j = 0; j < boardSize.y; j++)
                {
                    var buttonRect = new Rect(i * 100, j * 100, cellSize, cellSize);
                    var cell = new Vector2Int(i, j);
                    string cellValue = boardController.GetCell(cell).ToString();

                    if (GUI.Button(buttonRect, cellValue))
                    {
                        int value = boardController.GetPlayerTurn();
                        boardController.SetCell(cell, value);
                    }
                }
            }
        }
    }
}