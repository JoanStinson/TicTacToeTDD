using JGM.Controller;
using JGM.Model;
using Moq;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace JGM.Tests
{
    public class RowsResultTest
    {
        private const int defaultBoardRows = 3;
        private const int defaultBoardColumns = 3;

        private RowsResult rowsResult;
        private Mock<BoardController> boardControllerMock;

        [SetUp]
        public void SetUp()
        {
            rowsResult = new RowsResult();
            rowsResult.SetNextChain(new Mock<IResultChain>().Object);
            boardControllerMock = new Mock<BoardController>();
            boardControllerMock.Setup(mock => mock.Rows).Returns(defaultBoardRows);
            boardControllerMock.Setup(mock => mock.Columns).Returns(defaultBoardColumns);
        }

        [TestCase(-1, 0, 0,
                   1, 1, 1,
                   0, -1, -1)]
        public void When_CalculatingWinnerResult_Expect_Log(params int[] gridValues)
        {
            var boardModel = new BoardModel(defaultBoardRows, defaultBoardColumns);
            int counter = 0;
            for (int i = 0; i < boardModel.Rows; i++)
            {
                for (int j = 0; j < boardModel.Columns; j++)
                {
                    boardModel.SetCell(new Vector2Int(i, j), gridValues[counter++]);
                }
            }
            boardControllerMock.Setup(mock => mock.GetCell(It.IsAny<Vector2Int>())).Returns(boardModel.GetCell(It.IsAny<Vector2Int>()));
            rowsResult.Calculate(new Mock<BoardController>().Object);
            LogAssert.Expect(LogType.Log, $"Game Over - Winner is Player {1}!");
        }

        [Test]
        public void When_CalculatingNoWinnerResult_Expect_NextChainMethodIsCalled()
        {

        }
    }
}