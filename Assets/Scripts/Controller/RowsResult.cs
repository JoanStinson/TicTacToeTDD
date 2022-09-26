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

                    if (FirstValueOnRow(rowValue, cellValue))
                    {
                        rowValue = cellValue;
                        rowValueCount++;
                    }
                    else if (ValueIsEqualToFirstValue(rowValue, cellValue))
                    {
                        rowValueCount++;

                        if (WinConditionIsMet(rowValueCount))
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

        private bool FirstValueOnRow(in int rowValue, in int cellValue)
        {
            return (rowValue == -1 && cellValue != -1);
        }

        private bool ValueIsEqualToFirstValue(int rowValue, int cellValue)
        {
            return (rowValue == cellValue && cellValue != -1);
        }

        private bool WinConditionIsMet(int rowValueCount)
        {
            return (rowValueCount == BoardController.ConsecutiveTokensToWin);
        }
    }
}