using JGM.Controller;
using Moq;
using NUnit.Framework;

namespace JGM.Tests
{
    public class BoardResultControllerTest
    {
        private BoardResultController boardResultController;
        private Mock<RowsResult> rowsResult;

        [SetUp]
        public void SetUp()
        {
            rowsResult = new Mock<RowsResult>();
            boardResultController = new BoardResultController(rowsResult.Object);
        }

        [Test]
        public void When_CalculateIsCalled_Expect_RowsResultCalledOneTime()
        {
            boardResultController.Calculate(new Mock<BoardController>().Object);
            rowsResult.Verify(mock => mock.Calculate(It.IsAny<BoardController>()), Times.Once());
        }
    }
}