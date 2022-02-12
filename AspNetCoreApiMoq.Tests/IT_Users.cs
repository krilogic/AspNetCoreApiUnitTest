using AspNetCoreApi.Models;
using AspNetCoreApi.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AspNetCoreApiMoq.Tests
{
    [TestClass]
    public class IT_Users
    {
        #region Private members

        private Mock<IUserRepository> mUserRepository;
        private AspNetCoreApi.Controllers.UsersController mUsersController;
        private List<User> mUserList;
        private IEnumerable<User> mUsers;

        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            mUserRepository = new Mock<IUserRepository>();
            mUsersController = new AspNetCoreApi.Controllers.UsersController(mUserRepository.Object);
            mUserList = new List<User>();
        }

        #region Test methods

        [TestMethod]
        public void Get_WhenCalled_ReturnUsers()
        {
            //Arrange
            mUserList = new List<User>
            {
                new User()
                {
                   id= 1,
                   firstName= "g",
                   lastName= "k",
                   email= "gk@gmail.com",
                   password= "gk"
                }
            };

            mUserRepository.Setup(user => user.GetList()).Returns(mUserList);

            //Act
            mUsers = mUsersController.Get();

            //Assert
            NUnit.Framework.Assert.IsNotEmpty(mUsers);
        }

        #endregion
    }
}
