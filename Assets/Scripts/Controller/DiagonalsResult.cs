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
            // 1 | 0 | 0 
            // 0 | 1 | 0
            // 0 | 0 | 1
            if (boardController.GetCell(new Vector2Int(0, 0)) == boardController.GetCell(new Vector2Int(1, 1)) &&
                boardController.GetCell(new Vector2Int(1, 1)) == boardController.GetCell(new Vector2Int(2, 2)))
            {
                Debug.Log($"Game Over - Winner is Player {boardController.GetCell(new Vector2Int(0, 0))}!");
                boardController.GameIsPlaying = false;
                return;
            }
            // 0 | 0 | 1 
            // 0 | 1 | 0
            // 1 | 0 | 0
            else if (boardController.GetCell(new Vector2Int(0, 2)) == boardController.GetCell(new Vector2Int(1, 1)) &&
                     boardController.GetCell(new Vector2Int(1, 1)) == boardController.GetCell(new Vector2Int(2, 0)))
            {
                Debug.Log($"Game Over - Winner is Player {boardController.GetCell(new Vector2Int(0, 2))}!");
                boardController.GameIsPlaying = false;
                return;
            }

            if (boardController.IsLastTurn())
            {
                Debug.Log($"Game Over - It's a tie!");
                boardController.GameIsPlaying = false;
            }
        }
    }
}