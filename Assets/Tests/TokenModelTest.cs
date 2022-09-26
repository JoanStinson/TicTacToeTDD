using JGM.Model;
using NUnit.Framework;

namespace JGM.Tests
{
    public class TokenModelTest
    {
        private TokenModel tokenModel;

        [TestCase(0, "<size=70><b>O</b></size>")]
        [TestCase(1, "<size=70><b>X</b></size>")]
        [TestCase(-1, "")]
        [TestCase(44, "")]
        [TestCase(345, "")]
        [TestCase(-945, "")]
        public void When_TokenToStringIsCalled_Expect_CorrectString(int value, string expectedString)
        {
            tokenModel = new TokenModel(value);
            Assert.AreEqual(expectedString, tokenModel.ToString());
        }
    }
}