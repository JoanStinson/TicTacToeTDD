using JGM.Model;
using UnityEngine;

namespace JGM.Controller
{
    public class BoardController
    {
        public const int MaxPlayerRolls = 3;
        public int Rows => boardModel.Rows;
        public int Columns => boardModel.Columns;
        public bool IsGameRunning { get; set; }

        private const int maxPlayers = 2;
        private readonly BoardModel boardModel;
        private readonly BoardResultController boardResultController;
        private readonly int[] playerRolls;
        private int playerTurn;

        public BoardController(int rows, int columns, int playerTurn)
        {
            boardModel = new BoardModel(rows, columns);
            boardResultController = new BoardResultController();
            playerRolls = new int[maxPlayers];
            this.playerTurn = playerTurn;
        }

        public void SetCell(Vector2Int cell, int playerId)
        {
            if (playerTurn == playerId)
            {
                if (boardModel.GetCell(cell) == -1)
                {
                    boardModel.SetCell(cell, playerId);
                    playerRolls[playerId]++;

                    if (ShouldCheckResult())
                    {
                        boardResultController.Calculate(this);
                    }

                    playerTurn = 1 - playerTurn;
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
            return (IsLastTurn() || playerRolls[0] == MaxPlayerRolls || playerRolls[1] == MaxPlayerRolls);
        }

        public bool IsLastTurn()
        {
            return (playerRolls[0] == MaxPlayerRolls && playerRolls[1] == MaxPlayerRolls);
        }

        public int GetPlayerTurn()
        {
            return playerTurn;
        }

        public int GetCell(Vector2Int cell)
        {
            return boardModel.GetCell(cell);
        }

        public void ClearBoard()
        {
            boardModel.ClearCells();

            for (int i = 0; i < playerRolls.Length; i++)
            {
                playerRolls[i] = 0;
            }
        }
    }
}