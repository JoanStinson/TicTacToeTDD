using JGM.Controller;
using JGM.Model;
using UnityEngine;

namespace JGM.View
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Vector2Int boardSize = Vector2Int.one * 3;
        [SerializeField] private float cellSize = 80;
        [SerializeField][Range(0, 1)] private int startingPlayerTurn = 0;

        private BoardController boardController;

        private void Awake()
        {
            boardController = new BoardController(boardSize.x, boardSize.y, startingPlayerTurn);
        }

        private void OnGUI()
        {
            if (!boardController.IsGameRunning)
            {
                return;
            }

            for (int i = 0; i < boardSize.x; i++)
            {
                for (int j = 0; j < boardSize.y; j++)
                {
                    var buttonRect = new Rect(i * 100, j * 100, cellSize, cellSize);
                    var cell = new Vector2Int(i, j);
                    string cellValue = new TokenModel(boardController.GetCell(cell)).ToString();

                    if (GUI.Button(buttonRect, cellValue))
                    {
                        int playerId = boardController.GetPlayerTurn();
                        boardController.SetCell(cell, playerId);
                    }
                }
            }
        }
    }
}