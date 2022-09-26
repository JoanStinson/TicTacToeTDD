using JGM.Controller;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace JGM.Tests
{
    public class BoardControllerTest
    {
        private const int defaultBoardRows = 3;
        private const int defaultBoardColumns = 3;

        private BoardController boardController;

        [SetUp]
        public void SetUp()
        {
            boardController = new BoardController(defaultBoardRows, defaultBoardColumns, 0);
        }

        [Test]
        public void When_BoardIsCreated_Expect_CorrectBoardSize()
        {
            Assert.AreEqual(boardController.Rows, defaultBoardRows);
            Assert.AreEqual(boardController.Columns, defaultBoardColumns);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void When_SettingCells_Expect_CorrectTurnChange(int firstPlayer)
        {
            boardController = new BoardController(defaultBoardRows, defaultBoardColumns, firstPlayer);
            int secondPlayer = 1 - firstPlayer;
            boardController.SetCell(new Vector2Int(0, 0), firstPlayer);
            Assert.AreEqual(secondPlayer, boardController.GetPlayerTurn());
            boardController.SetCell(new Vector2Int(0, 2), secondPlayer);
            Assert.AreEqual(firstPlayer, boardController.GetPlayerTurn());
            boardController.SetCell(new Vector2Int(1, 1), firstPlayer);
            Assert.AreEqual(secondPlayer, boardController.GetPlayerTurn());
        }

        [Test]
        public void When_SetCellWithSamePlayerTwiceInARow_Expect_LogWarning()
        {
            boardController.SetCell(new Vector2Int(0, 0), 0);
            boardController.SetCell(new Vector2Int(0, 2), 0);
            LogAssert.Expect(LogType.Warning, "Incorrect player turn!");
        }

        [Test]
        public void When_SettingAnOccupiedCell_Expect_LogWarning()
        {
            boardController.SetCell(new Vector2Int(0, 0), 0);
            boardController.SetCell(new Vector2Int(0, 2), 1);
            boardController.SetCell(new Vector2Int(0, 2), 0);
            LogAssert.Expect(LogType.Warning, "Specified cell is already taken!");
        }

        [Test]
        public void When_AllCellsAreOccupied_Expect_IsLastTurnAsTrue()
        {
            boardController.SetCell(new Vector2Int(0, 0), 0);
            boardController.SetCell(new Vector2Int(0, 1), 1);
            boardController.SetCell(new Vector2Int(0, 2), 0);
            boardController.SetCell(new Vector2Int(1, 0), 1);
            boardController.SetCell(new Vector2Int(1, 1), 0);
            boardController.SetCell(new Vector2Int(1, 2), 1);
            boardController.SetCell(new Vector2Int(2, 0), 0);
            boardController.SetCell(new Vector2Int(2, 1), 1);
            boardController.SetCell(new Vector2Int(2, 2), 0);
            Assert.IsTrue(boardController.IsLastTurn());
        }

        [TestCase(new int[] { 0, 0 }, 0)]
        [TestCase(new int[] { 2, 1 }, 1)]
        [TestCase(new int[] { 0, 1 }, 0)]
        [TestCase(new int[] { 1, 1 }, 1)]
        public void When_GettingCell_Expect_CorrectValue(int[] cellCoordinates, int value)
        {
            boardController = new BoardController(defaultBoardRows, defaultBoardColumns, value);
            var coordinates = new Vector2Int(cellCoordinates[0], cellCoordinates[1]);
            boardController.SetCell(coordinates, value);
            Assert.AreEqual(value, boardController.GetCell(coordinates));
        }

        [TestCase(new int[] { 0, 0 }, -1)]
        [TestCase(new int[] { 2, 1 }, -1)]
        [TestCase(new int[] { 0, 1 }, -1)]
        [TestCase(new int[] { 1, 1 }, -1)]
        public void When_GettingUnOccupiedCell_Expect_EmptyValue(int[] cellCoordinates, int value)
        {
            var coordinates = new Vector2Int(cellCoordinates[0], cellCoordinates[1]);
            Assert.AreEqual(value, boardController.GetCell(coordinates));
        }

        [Test]
        public void When_RestartingGame_Expect_GameIsPlayingAsTrue()
        {
            boardController.GameIsPlaying = false;
            boardController.Restart();
            Assert.IsTrue(boardController.GameIsPlaying);
        }
    }
}