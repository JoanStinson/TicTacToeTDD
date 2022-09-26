using JGM.Model;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace JGM.Tests
{
    public class BoardModelTest
    {
        private const int defaultBoardRows = 3;
        private const int defaultBoardColumns = 3;

        private BoardModel boardModel;

        [SetUp]
        public void SetUp()
        {
            boardModel = new BoardModel(defaultBoardRows, defaultBoardColumns);
        }

        [TestCase(3, 4)]
        [TestCase(1, 1)]
        [TestCase(5, 7)]
        [TestCase(14, 25)]
        public void When_BoardIsCreatedWithGreaterThanZeroRowsAndColumns_Expect_CorrectSize(int rows, int columns)
        {
            boardModel = new BoardModel(rows, columns);
            Assert.AreEqual(boardModel.Rows, rows);
            Assert.AreEqual(boardModel.Columns, columns);
        }

        [TestCase(0, 0)]
        [TestCase(0, 11)]
        [TestCase(7, 0)]
        public void When_BoardIsCreatedWithRowsAndOrColumnsEqualToZero_Expect_AssertError(int rows, int columns)
        {
            boardModel = new BoardModel(rows, columns);
            LogAssert.Expect(LogType.Assert, "Assertion failed");
        }

        [TestCase(new int[] { 0, 0 })]
        [TestCase(new int[] { 0, 1 })]
        [TestCase(new int[] { 0, 2 })]
        [TestCase(new int[] { 1, 0 })]
        [TestCase(new int[] { 1, 1 })]
        [TestCase(new int[] { 1, 2 })]
        [TestCase(new int[] { 2, 0 })]
        [TestCase(new int[] { 2, 1 })]
        [TestCase(new int[] { 2, 2 })]
        public void When_BoardIsCleared_Expect_AllCellValuesToBeReset(int[] cellCoordinates)
        {
            boardModel.ClearCells();
            var coordinates = new Vector2Int(cellCoordinates[0], cellCoordinates[1]);
            Assert.AreEqual(-1, boardModel.GetCell(coordinates));
        }

        [TestCase(new int[] { 0, 0 }, 1)]
        [TestCase(new int[] { 2, 1 }, 0)]
        [TestCase(new int[] { 1, 1 }, 0)]
        [TestCase(new int[] { 0, 2 }, 1)]
        public void When_CellIsSetInsideBounds_Expect_CorrectReturnValue(int[] cellCoordinates, int value)
        {
            var coordinates = new Vector2Int(cellCoordinates[0], cellCoordinates[1]);
            boardModel.SetCell(coordinates, value);
            Assert.AreEqual(boardModel.GetCell(coordinates), value);
        }

        [TestCase(new int[] { 4, 7 }, 1)]
        [TestCase(new int[] { 2, 11 }, 0)]
        [TestCase(new int[] { 9, 23 }, 0)]
        public void When_CellIsSetOutsideBounds_Expect_AssertError(int[] cellCoordinates, int value)
        {
            var coordinates = new Vector2Int(cellCoordinates[0], cellCoordinates[1]);
            boardModel.SetCell(coordinates, value);
            LogAssert.Expect(LogType.Assert, "Assertion failed");
        }

        [TestCase(new int[] { 0, 0 }, -1)]
        [TestCase(new int[] { 0, 1 }, 1)]
        [TestCase(new int[] { 2, 0 }, 0)]
        public void When_GetCellInsideBounds_Expect_CorrectReturnValue(int[] cellCoordinates, int value)
        {
            var coordinates = new Vector2Int(cellCoordinates[0], cellCoordinates[1]);
            if (value != -1)
            {
                boardModel.SetCell(coordinates, value);
            }
            int cellValue = boardModel.GetCell(coordinates);
            Assert.AreEqual(cellValue, value);
        }

        [TestCase(new int[] { 9, 17 })]
        [TestCase(new int[] { 0, 5 })]
        [TestCase(new int[] { 7, 7 })]
        public void When_GetCellOutsideBounds_Expect_AssertError(int[] cellCoordinates)
        {
            var coordinates = new Vector2Int(cellCoordinates[0], cellCoordinates[1]);
            boardModel.GetCell(coordinates);
            LogAssert.Expect(LogType.Assert, "Assertion failed");
        }
    }
}