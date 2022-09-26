using JGM.Controller;
using JGM.View;
using Moq;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.TestTools;

namespace JGM.Tests
{
    public class BoardViewTest
    {
        private BoardView boardView;
        private Mock<BoardController> boardControllerMock;

        [SetUp]
        public void Setup()
        {
            var dummy = new GameObject("Dummy");
            boardView = dummy.AddComponent<BoardView>();
            boardControllerMock = new Mock<BoardController>();
            boardControllerMock.Object.GameIsPlaying = true;
            boardView.Initialize(boardControllerMock.Object);
        }

        [Test]
        public void When_BoardViewIsCreated_Expect_ShouldDrawTitleAndBoardAsTrue()
        {
            Assert.IsTrue(boardView.ShouldDrawTitle);
            Assert.IsTrue(boardView.ShouldDrawBoard);
            Assert.IsFalse(boardView.ShouldDrawPlayAgainButton);
        }

        [Test]
        public void When_ItsGameOver_Expect_ShouldDrawPlayAgainButtonAsTrue()
        {
            boardControllerMock.Object.GameIsPlaying = false;
            Assert.IsTrue(boardView.ShouldDrawTitle);
            Assert.IsTrue(boardView.ShouldDrawBoard);
            Assert.IsTrue(boardView.ShouldDrawPlayAgainButton);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void When_CallingDrawTitle_Expect_CorrectBooleanReturnValue(bool value)
        {
            boardView.ShouldDrawTitle = value;
            Assert.AreEqual(value, boardView.DrawTitle(false));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void When_CallingDrawBoard_Expect_CorrectBooleanReturnValue(bool value)
        {
            boardView.ShouldDrawBoard = value;
            Assert.AreEqual(value, boardView.DrawBoard(false));
            boardControllerMock.Verify(mock => mock.GetCell(It.IsAny<Vector2Int>()), value ? Times.AtLeastOnce() : Times.Never());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void When_CallingDrawPlayAgainButton_Expect_CorrectBooleanReturnValue(bool value)
        {
            boardControllerMock.Object.GameIsPlaying = !value;
            Assert.AreEqual(value, boardView.ShouldDrawPlayAgainButton);
            Assert.AreEqual(value, boardView.DrawPlayAgainButton(false));
        }

        [Test]
        public void When_PlayAgainButtonIsClicked_Expect_RestartMethodToBeCalleOnce()
        {
            boardControllerMock.Object.GameIsPlaying = false;
            Assert.IsTrue(boardView.ShouldDrawPlayAgainButton);
            bool value = boardView.DrawPlayAgainButton(false, true);
            Assert.IsTrue(value);
            boardControllerMock.Verify(mock => mock.Restart(), Times.Once());
        }

        [Test]
        public void When_DrawingTitleOutsideOnGUIMethod_Expect_ArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() => boardView.DrawTitle(true));
        }

        [Test]
        public void When_DrawingBoardOutsideOnGUIMethod_Expect_ArgumentExceptionThrown()
        {
            boardControllerMock.Setup(mock => mock.SetCell(It.IsAny<Vector2Int>(), It.IsAny<int>())).Verifiable();
            boardView.DrawBoard(true, true);
            //LogAssert.Expect(LogType.Error, "[Error] You can only call GUI functions from inside OnGUI.");
            boardControllerMock.Verify(mock => mock.SetCell(It.IsAny<Vector2Int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Test]
        public void When_DrawingPlayAgainButtonOutsideOnGUIMethod_Expect_ArgumentExceptionThrown()
        {
            boardControllerMock.Object.GameIsPlaying = false;
            boardView.DrawPlayAgainButton(true, true);
            LogAssert.Expect(LogType.Error, "You can only call GUI functions from inside OnGUI.");
            boardControllerMock.Verify(mock => mock.Restart(), Times.Once());
        }
    }
}