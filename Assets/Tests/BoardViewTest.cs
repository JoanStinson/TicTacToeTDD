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
        public void When_DrawingTitleOutsideOnGUIMethod_Expect_ArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() => boardView.DrawTitle());
        }

        [Test]
        public void When_DrawingBoardOutsideOnGUIMethod_Expect_ErrorLog()
        {
            boardControllerMock.Setup(mock => mock.GetCell(It.IsAny<Vector2Int>())).Returns(-1);
            boardControllerMock.Setup(mock => mock.SetCell(It.IsAny<Vector2Int>(), It.IsAny<int>())).Verifiable();
            boardView.DrawBoard(true);
            LogAssert.Expect(LogType.Error, "You can only call GUI functions from inside OnGUI.");
            boardControllerMock.Verify(mock => mock.GetCell(It.IsAny<Vector2Int>()), Times.AtLeastOnce());
            boardControllerMock.Verify(mock => mock.SetCell(It.IsAny<Vector2Int>(), It.IsAny<int>()), Times.AtLeastOnce());
        }

        [Test]
        public void When_DrawingPlayAgainButtonOutsideOnGUIMethod_Expect_ErrorLog()
        {
            boardControllerMock.Object.GameIsPlaying = false;
            boardView.DrawPlayAgainButton(true);
            LogAssert.Expect(LogType.Error, "You can only call GUI functions from inside OnGUI.");
            boardControllerMock.Verify(mock => mock.Restart(), Times.Once());
        }
    }
}