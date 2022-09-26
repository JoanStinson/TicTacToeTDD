using UnityEngine;

namespace JGM.Controller
{
    public class RowsResult : IResultChain
    {
        private IResultChain nextChain;

        public void SetNextChain(IResultChain nextChain)
        {
            this.nextChain = nextChain;
        }

        public virtual void Calculate(BoardController boardController)
        {
            for (int i = 0; i < boardController.Rows; i++)
            {
                int rowValue = -1;
                int rowValueCount = 0;

                for (int j = 0; j < boardController.Columns; j++)
                {
                    var coordinates = new Vector2Int(i, j);
                    int cellValue = boardController.GetCell(coordinates);

                    if (rowValue == -1 && cellValue != -1)
                    {
                        rowValue = cellValue;
                        rowValueCount++;
                    }
                    else if (rowValue == cellValue && cellValue != -1)
                    {
                        rowValueCount++;

                        if (rowValueCount == BoardController.MaxPlayerRolls)
                        {
                            Debug.Log($"Game Over - Winner is Player {cellValue}!");
                            boardController.GameIsPlaying = false;
                            return;
                        }
                    }
                }
            }

            nextChain.Calculate(boardController);
        }
    }
}