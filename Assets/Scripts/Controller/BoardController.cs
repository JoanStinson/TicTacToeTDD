using JGM.Model;
using UnityEngine;

namespace JGM.Controller
{
    public class BoardController
    {
        public const int ConsecutiveTokensToWin = 3;
        public virtual int Rows => boardModel.Rows;
        public virtual int Columns => boardModel.Columns;
        public bool GameIsPlaying { get; set; }

        private const int maxPlayers = 2;
        private readonly BoardModel boardModel;
        private readonly BoardResultController boardResultController;
        private readonly int[] playerTokens;
        private int playerTurn;

        public BoardController() { }

        public BoardController(int rows, int columns, int playerTurn)
        {
            boardModel = new BoardModel(rows, columns);
            boardResultController = new BoardResultController();
            playerTokens = new int[maxPlayers];
            this.playerTurn = playerTurn;
            GameIsPlaying = true;
        }

        public virtual void SetCell(Vector2Int coordinates, int playerId)
        {
            if (playerTurn == playerId)
            {
                if (boardModel.GetCell(coordinates) == -1)
                {
                    boardModel.SetCell(coordinates, playerId);
                    playerTokens[playerId]++;
                    playerTurn = 1 - playerTurn;

                    if (ShouldCheckResult())
                    {
                        boardResultController.Calculate(this);
                    }
                }
                else
                {
                    Debug.LogWarning("Specified cell is already taken!");
                }
            }
            else
            {
                Debug.LogWarning("Incorrect player turn!");
            }
        }

        private bool ShouldCheckResult()
        {
            return (playerTokens[0] >= ConsecutiveTokensToWin || playerTokens[1] >= ConsecutiveTokensToWin);
        }

        public virtual bool IsLastTurn()
        {
            return (playerTokens[0] + playerTokens[1] == Rows * Columns);
        }

        public int GetPlayerTurn()
        {
            return playerTurn;
        }

        public virtual int GetCell(Vector2Int coordinates)
        {
            return boardModel.GetCell(coordinates);
        }

        public virtual void Restart()
        {
            ClearBoard();
            GameIsPlaying = true;
        }

        private void ClearBoard()
        {
            boardModel.ClearCells();

            for (int i = 0; i < playerTokens.Length; i++)
            {
                playerTokens[i] = 0;
            }
        }
    }
}