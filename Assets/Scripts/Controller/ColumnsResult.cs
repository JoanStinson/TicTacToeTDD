using UnityEngine;

namespace JGM.Controller
{
    public class ColumnsResult : IResultChain
    {
        private IResultChain nextChain;

        public void SetNextChain(IResultChain nextChain)
        {
            this.nextChain = nextChain;
        }

        public void Calculate(BoardController boardController)
        {
            for (int i = 0; i < boardController.Rows; i++)
            {
                int columnValue = -1;
                int columnValueCount = 0;

                for (int j = 0; j < boardController.Columns; j++)
                {
                    var coordinates = new Vector2Int(j, i);
                    int cellValue = boardController.GetCell(coordinates);

                    if (FirstValueOnColumn(columnValue, cellValue))
                    {
                        columnValue = cellValue;
                        columnValueCount++;
                    }
                    else if (ValueIsEqualToFirstValue(columnValue, cellValue))
                    {
                        columnValueCount++;

                        if (WinConditionIsMet(columnValueCount))
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

        private bool FirstValueOnColumn(int columnValue, int cellValue)
        {
            return (columnValue == -1 && cellValue != -1);
        }

        private bool ValueIsEqualToFirstValue(int columnValue, int cellValue)
        {
            return (columnValue == cellValue && cellValue != -1);
        }

        private bool WinConditionIsMet(int columnValueCount)
        {
            return (columnValueCount == BoardController.ConsecutiveTokensToWin);
        }
    }
}