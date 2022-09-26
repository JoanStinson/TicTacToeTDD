using JGM.Controller;
using JGM.Model;
using Moq;
using NUnit.Framework;
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
            boardControllerMock.Object.GameIsPlaying = true;
        }

        [TestCase(0f,
                  0, 0, 0,
                  1, -1, 1,
                  1, -1, -1)]
        [TestCase(0f,
                 -1, 1, 1,
                  0, 0, 0,
                  1, -1, -1)]
        [TestCase(0f,
                 -1, 1, 1,
                  1, -1, -1,
                  0, 0, 0)]
        [TestCase(1f,
                  1, 1, 1,
                  0, -1, 0,
                  0, -1, -1)]
        [TestCase(1f,
                 -1, 0, 0,
                  1, 1, 1,
                  0, -1, -1)]
        [TestCase(1f,
                 -1, 0, 0,
                  0, -1, -1,
                  1, 1, 1)]
        public void When_CalculatingWinnerResult_Expect_GameIsPlayingAsFalse(float winner, params int[] gridValues)
        {
            FillBoardWithValues(gridValues);
            rowsResult.Calculate(boardControllerMock.Object);
            boardControllerMock.Verify(mock => mock.GetCell(It.IsAny<Vector2Int>()), Times.AtLeast(defaultBoardRows));
            LogAssert.Expect(LogType.Log, $"Game Over - Winner is Player {(int)winner}!");
            Assert.IsFalse(boardControllerMock.Object.GameIsPlaying);
        }

        [TestCase(1, 0, 0,
                  1, -1, 1,
                  0, -1, -1)]
        [TestCase(0, 1, 1,
                  0, 0, 1,
                 -1, -1, -1)]
        [TestCase(-1, 0, 0,
                   0, 1, 1,
                  -1, -1, -1)]
        public void When_CalculatingNoWinnerResult_Expect_GameIsPlayingAsTrue(params int[] gridValues)
        {
            FillBoardWithValues(gridValues);
            rowsResult.Calculate(boardControllerMock.Object);
            boardControllerMock.Verify(mock => mock.GetCell(It.IsAny<Vector2Int>()), Times.Exactly(defaultBoardRows * defaultBoardColumns));
            Assert.IsTrue(boardControllerMock.Object.GameIsPlaying);
        }

        private void FillBoardWithValues(int[] gridValues)
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
            boardControllerMock.Setup(mock => mock.GetCell(It.IsAny<Vector2Int>())).Returns((Vector2Int coordinates) => boardModel.GetCell(coordinates));
        }
    }
}