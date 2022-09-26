using JGM.Model;
using UnityEngine;

namespace JGM.Controller
{
    public class BoardController
    {
        public const int MaxPlayerRolls = 3;
        public virtual int Rows => boardModel.Rows;
        public virtual int Columns => boardModel.Columns;
        public bool GameIsPlaying { get; set; }

        private const int maxPlayers = 2;
        private readonly BoardModel boardModel;
        private readonly BoardResultController boardResultController;
        private readonly int[] playerRolls;
        private int playerTurn;

        public BoardController() { }

        public BoardController(int rows, int columns, int playerTurn)
        {
            boardModel = new BoardModel(rows, columns);
            boardResultController = new BoardResultController();
            playerRolls = new int[maxPlayers];
            this.playerTurn = playerTurn;
            GameIsPlaying = true;
        }

        public void SetCell(Vector2Int coordinates, int playerId)
        {
            if (playerTurn == playerId)
            {
                if (boardModel.GetCell(coordinates) == -1)
                {
                    boardModel.SetCell(coordinates, playerId);
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

        public virtual bool IsLastTurn()
        {
            return (playerRolls[0] == MaxPlayerRolls && playerRolls[1] == MaxPlayerRolls);
        }

        public int GetPlayerTurn()
        {
            return playerTurn;
        }

        public virtual int GetCell(Vector2Int coordinates)
        {
            return boardModel.GetCell(coordinates);
        }

        public void Restart()
        {
            ClearBoard();
            GameIsPlaying = true;
        }

        private void ClearBoard()
        {
            boardModel.ClearCells();

            for (int i = 0; i < playerRolls.Length; i++)
            {
                playerRolls[i] = 0;
            }
        }
    }
}