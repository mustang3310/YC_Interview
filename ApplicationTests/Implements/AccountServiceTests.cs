namespace ApplicationTests.Implements
{
    using Application.Implements;
    using Domain.Interfaces;
    using Domain.Models;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass()]
    public class AccountServiceTests
    {
        #region IsLogin

        [TestMethod()]
        public void IsLogin_WithMatchingPassword_ReturnsTrue()
        {
            // Arrange
            Mock<IUserDac> userDacMock = new();
            _ = userDacMock
                .Setup(dac => dac.CheckNameMatchPassword(It.IsAny<UserModel>()))
                .ReturnsAsync(true);

            Mock<AccountService> accountService = new(userDacMock.Object);

            // Act
            bool result = accountService.Object.IsLogin(new UserModel());

            // Assert
            _ = result.Should().BeTrue();
        }

        [TestMethod()]
        public void IsLogin_WithNonMatchingPassword_ReturnsFalse()
        {
            // Arrange
            Mock<IUserDac> userDacMock = new();

            _ = userDacMock.Setup(dac => dac.CheckNameMatchPassword(It.IsAny<UserModel>()))
                .ReturnsAsync(false);

            _ = userDacMock.Setup(dac => dac.IsUserLocked(It.IsAny<string>()))
                .Returns(false);

            AccountService accountService = new(userDacMock.Object);

            // Act
            bool result = accountService.IsLogin(new UserModel());

            // Assert
            _ = result.Should().BeFalse();
        }

        [TestMethod()]
        public void IsLogin_WithIsLockedUser_ReturnFalse()
        {
            // Arrange
            Mock<IUserDac> userDacMock = new();

            _ = userDacMock.Setup(dac => dac.CheckNameMatchPassword(It.IsAny<UserModel>()))
                .ReturnsAsync(true);

            _ = userDacMock.Setup(dac => dac.IsUserLocked(It.IsAny<string>()))
                .Returns(true);

            AccountService accountService = new(userDacMock.Object);

            // Act
            bool result = accountService.IsLogin(new UserModel());

            // Assert
            _ = result.Should().BeFalse();
        }

        #endregion
    }
}