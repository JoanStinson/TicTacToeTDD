using JGM.Controller;
using JGM.Model;
using System;
using UnityEngine;

namespace JGM.View
{
    public class BoardView : MonoBehaviour
    {
        public bool ShouldDrawTitle { get; set; } = true;
        public bool ShouldDrawBoard { get; set; } = true;
        public bool ShouldDrawPlayAgainButton => !boardController.GameIsPlaying;

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

        public bool DrawTitle(bool render = true)
        {
            if (!ShouldDrawTitle)
            {
                return false;
            }

            if (render)
            {
                GUI.Label(new Rect(screenWidthInHalf - 160, screenHeightInHalf - 225, 320, 300), "<size=50><b>TIC TAC TOE</b></size>");
            }

            return true;
        }

        public bool DrawBoard(bool render = true, bool buttonClickSimulated = false)
        {
            if (!ShouldDrawBoard)
            {
                return false;
            }

            if (render)
            {
                try
                {
                    GUI.BeginGroup(new Rect(screenWidthInHalf - (boardRenderSize / 2), screenHeightInHalf - (boardRenderSize / 2), boardRenderSize, boardRenderSize));
                    GUI.Box(new Rect(0, 0, boardRenderSize, boardRenderSize), string.Empty);
                }
                catch (Exception exception)
                {
                    //Debug.LogError(exception.Message);
                }
            }

            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    var buttonRect = new Rect((i * cellsSpacing) + cellsPadding, (j * cellsSpacing) + cellsPadding, cellSize, cellSize);
                    var coordinates = new Vector2Int(i, j);
                    string cellValue = new TokenModel(boardController.GetCell(coordinates)).ToString();
                    bool buttonClicked = false;

                    if (render)
                    {
                        try
                        {
                            buttonClicked = GUI.Button(buttonRect, cellValue);
                        }
                        catch (Exception exception)
                        {
                            //Debug.LogError(exception.Message);
                        }
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
            }

            if (render)
            {
                try
                {
                    GUI.EndGroup();
                }
                catch (Exception exception)
                {
                    //Debug.LogError(exception.Message);
                }
            }

            return true;
        }

        public bool DrawPlayAgainButton(bool render = true, bool buttonClickSimulated = false)
        {
            if (!ShouldDrawPlayAgainButton)
            {
                return false;
            }

            bool buttonClicked = false;

            if (render)
            {
                try
                {
                    var playAgainButtonRect = new Rect(screenWidthInHalf - 50, screenHeightInHalf + 175, 100, 25);
                    buttonClicked = GUI.Button(playAgainButtonRect, "<b>PLAY AGAIN</b>");
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.Message);
                }
                finally
                {
                    if (buttonClicked || buttonClickSimulated)
                    {
                        boardController.Restart();
                    }
                }
            }
            else if (buttonClickSimulated)
            {
                boardController.Restart();
            }

            return true;
        }
    }
}