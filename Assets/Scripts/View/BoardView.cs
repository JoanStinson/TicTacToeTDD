using JGM.Controller;
using JGM.Model;
using System;
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

        private BoardController boardController;

        public void Initialize(BoardController boardController)
        {
            this.boardController = boardController;
        }

        private void Awake()
        {
            boardController ??= new BoardController(boardRows, boardColumns, startingPlayerTurn);
        }

        private void OnGUI()
        {
            DrawTitle();
            DrawBoard();
            DrawPlayAgainButton();
        }

        public void DrawTitle()
        {
            GUI.Label(new Rect((Screen.width / 2) - 160, (Screen.height / 2) - 225, 320, 300), "<size=50><b>TIC TAC TOE</b></size>");
        }

        public void DrawBoard(bool buttonClickSimulated = false)
        {
            try
            {
                GUI.BeginGroup(new Rect((Screen.width / 2) - (boardRenderSize / 2), (Screen.height / 2) - (boardRenderSize / 2), boardRenderSize, boardRenderSize));
                GUI.Box(new Rect(0, 0, boardRenderSize, boardRenderSize), string.Empty);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    var buttonRect = new Rect((i * cellsSpacing) + cellsPadding, (j * cellsSpacing) + cellsPadding, cellSize, cellSize);
                    var coordinates = new Vector2Int(i, j);
                    string cellValue = new TokenModel(boardController.GetCell(coordinates)).ToString();
                    bool buttonClicked = false;

                    try
                    {
                        buttonClicked = GUI.Button(buttonRect, cellValue);
                    }
                    catch (Exception) { }
                    finally
                    {
                        if ((buttonClicked || buttonClickSimulated) && boardController.GameIsPlaying)
                        {
                            int playerId = boardController.GetPlayerTurn();
                            boardController.SetCell(coordinates, playerId);
                        }
                    }
                }
            }

            try
            {
                GUI.EndGroup();
            }
            catch (Exception) { }
        }

        public void DrawPlayAgainButton(bool buttonClickedSimulated = false)
        {
            if (boardController.GameIsPlaying)
            {
                return;
            }

            bool buttonClicked = false;

            try
            {
                var playAgainButtonRect = new Rect((Screen.width / 2) - 50, (Screen.height / 2) + 175, 100, 25);
                buttonClicked = GUI.Button(playAgainButtonRect, "<b>PLAY AGAIN</b>");
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
            finally
            {
                if (buttonClicked || buttonClickedSimulated)
                {
                    boardController.Restart();
                }
            }
        }
    }
}