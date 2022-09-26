using UnityEngine;

namespace JGM.Controller
{
    public class DiagonalsResult : IResultChain
    {
        private IResultChain nextChain;

        public void SetNextChain(IResultChain nextChain)
        {
            this.nextChain = nextChain;
        }

        public void Calculate(BoardController boardController)
        {
            if (WinnerInLeftDiagonal(boardController) || WinnerInRightDiagonal(boardController))
            {
                Debug.Log($"Game Over - Winner is Player {boardController.GetCell(new Vector2Int(1, 1))}!");
                boardController.GameIsPlaying = false;
            }
            else if (boardController.IsLastTurn())
            {
                Debug.Log("Game Over - It's a tie!");
                boardController.GameIsPlaying = false;
            }
        }

        private bool WinnerInLeftDiagonal(BoardController boardController)
        {
            // 1 | 0 | 0 
            // 0 | 1 | 0
            // 0 | 0 | 1
            return (boardController.GetCell(new Vector2Int(0, 0)) == boardController.GetCell(new Vector2Int(1, 1)) &&
                    boardController.GetCell(new Vector2Int(1, 1)) == boardController.GetCell(new Vector2Int(2, 2)));
        }

        private bool WinnerInRightDiagonal(BoardController boardController)
        {
            // 0 | 0 | 1 
            // 0 | 1 | 0
            // 1 | 0 | 0
            return (boardController.GetCell(new Vector2Int(0, 2)) == boardController.GetCell(new Vector2Int(1, 1)) &&
                    boardController.GetCell(new Vector2Int(1, 1)) == boardController.GetCell(new Vector2Int(2, 0)));
        }
    }
}