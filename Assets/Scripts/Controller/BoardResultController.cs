namespace JGM.Controller
{
    public class BoardResultController
    {
        private readonly RowsResult rowsResult;

        public BoardResultController()
        {
            rowsResult ??= new RowsResult();
            var columnsResult = new ColumnsResult();
            var diagonalsResult = new DiagonalsResult();

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