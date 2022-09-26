namespace JGM.Controller
{
    public class BoardResultController
    {
        private readonly IResultChain rowsResult;

        public BoardResultController()
        {
            rowsResult ??= new RowsResult();
            IResultChain columnsResult = new ColumnsResult();
            IResultChain diagonalsResult = new DiagonalsResult();

            rowsResult.SetNextChain(columnsResult);
            columnsResult.SetNextChain(diagonalsResult);
        }

        public BoardResultController(RowsResult rowsResult) : this()
        {
            this.rowsResult = rowsResult;
        }

        public void Calculate(BoardController boardController)
        {
            rowsResult.Calculate(boardController);
        }
    }
}