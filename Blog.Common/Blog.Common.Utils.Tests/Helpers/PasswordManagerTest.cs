using Blog.Common.Utils.Helpers;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    public class PasswordManagerTest
    {
        [Test]
        public void ShouldReturnBlankOnEmptyPassword()
        {
            var result = PasswordManager.CheckStrength(string.Empty);
            Assert.AreEqual(Constants.PasswordScore.Blank, result);
        }

        [Test]
        public void ShouldReturnVeryWeakOnShortPassword()
        {
            var result = PasswordManager.CheckStrength("foo");
            Assert.AreEqual(Constants.PasswordScore.VeryWeak, result);
        }

        [Test]
        public void ShouldReturnWeakOnMediumLengthPassword()
        {
            var result = PasswordManager.CheckStrength("foobarbaz");
            Assert.AreEqual(Constants.PasswordScore.Weak, result);
        }

        [Test]
        public void ShouldReturnMediumOnLongLengthPassword()
        {
            var result = PasswordManager.CheckStrength("loremipsumdolor");
            Assert.AreEqual(Constants.PasswordScore.Medium, result);
        }

        [Test]
        public void ShouldReturnStrongOnPasswordWithNumbers()
        {
            var result = PasswordManager.CheckStrength("loremipsumdolor123");
            Assert.AreEqual(Constants.PasswordScore.Strong, result);
        }

        [Test]
        public void ShouldReturnVeryStrongOnPasswordWithNumbersAndUpperCaseLetters()
        {
            var result = PasswordManager.CheckStrength("LoremIpsumDolor123");
            Assert.AreEqual(Constants.PasswordScore.VeryStrong, result);
        }

        [Test]
        public void ShouldReturnMediumOnPasswordWithNumbersAndUpperAndLowerCaseLetters()
        {
            var result = PasswordManager.CheckStrength("LOREMipsum");
            Assert.AreEqual(Constants.PasswordScore.Medium, result);
        }

        [Test]
        public void ShouldReturnLikeABossOnPasswordWithNumbersAndUpperCaseLettersAndSpecialCharacters()
        {
            var result = PasswordManager.CheckStrength("LoremIpsumDolor123!!");
            Assert.AreEqual(Constants.PasswordScore.LikeABoss, result);
        }
    }
}
