using JGM.Controller;
using JGM.Model;
using UnityEngine;

namespace JGM.View
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private int boardRows = 3;
        [SerializeField] private int boardColumns = 3;
        [SerializeField] private float cellSize = 80;
        [SerializeField][Range(0, 1)] private int startingPlayerTurn = 0;

        private const int boardRenderSize = 300;
        private const int cellsSpacing = 100;
        private const int cellsPadding = 10;

        private static readonly int screenWidthInHalf = Screen.width / 2;
        private static readonly int screenHeightInHalf = Screen.height / 2;

        private BoardController boardController;

        private void Awake()
        {
            boardController = new BoardController(boardRows, boardColumns, startingPlayerTurn);
        }

        private void OnGUI()
        {
            DrawTitle();
            DrawBoard();
            DrawPlayAgainButton();
        }

        private void DrawTitle()
        {
            GUI.Label(new Rect(screenWidthInHalf - 160, screenHeightInHalf - 225, 320, 300), "<size=50><b>TIC TAC TOE</b></size>");
        }

        private void DrawBoard()
        {
            GUI.BeginGroup(new Rect(screenWidthInHalf - (boardRenderSize / 2), screenHeightInHalf - (boardRenderSize / 2), boardRenderSize, boardRenderSize));
            GUI.Box(new Rect(0, 0, boardRenderSize, boardRenderSize), string.Empty);

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    var buttonRect = new Rect((i * cellsSpacing) + cellsPadding, (j * cellsSpacing) + cellsPadding, cellSize, cellSize);
                    var coordinates = new Vector2Int(i, j);
                    string cellValue = new TokenModel(boardController.GetCell(coordinates)).ToString();

                    if (GUI.Button(buttonRect, cellValue))
                    {
                        if (!boardController.GameIsPlaying)
                        {
                            continue;
                        }

                        int playerId = boardController.GetPlayerTurn();
                        boardController.SetCell(coordinates, playerId);
                    }
                }
            }

            GUI.EndGroup();
        }

        private void DrawPlayAgainButton()
        {
            if (!boardController.GameIsPlaying)
            {
                var playAgainButtonRect = new Rect(screenWidthInHalf - 50, screenHeightInHalf + 175, 100, 25);
                if (GUI.Button(playAgainButtonRect, "<b>PLAY AGAIN</b>"))
                {
                    boardController.Restart();
                }
            }
        }
    }
}