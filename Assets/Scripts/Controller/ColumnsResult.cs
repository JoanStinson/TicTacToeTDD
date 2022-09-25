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
                    var cell = new Vector2Int(j, i);
                    int cellValue = boardController.GetCell(cell);

                    if (columnValue == -1 && cellValue != -1)
                    {
                        columnValue = cellValue;
                        columnValueCount++;
                    }
                    else if (columnValue == cellValue && cellValue != -1)
                    {
                        columnValueCount++;

                        if (columnValueCount == BoardController.MaxPlayerRolls)
                        {
                            Debug.Log($"Game Over - Winner is Player {cellValue}!");
                            boardController.IsGameRunning = false;
                            return;
                        }
                    }
                }
            }

            nextChain.Calculate(boardController);
        }
    }
}