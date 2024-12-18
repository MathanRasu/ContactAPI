using ContactApi.BusinessLogicLayer.Interface;
using ContactApi.Controllers;
using ContactApi.DataAccessLayer.DataObject.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactAPI.UnitTest
{
    [TestClass]
    public class ContactControllerTests
    {
        private Mock<IContactBal> _mockContactBal;
        private ContactController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _mockContactBal = new Mock<IContactBal>();
            _controller = new ContactController(_mockContactBal.Object);
        }

        // Authentication Tests

        [TestMethod]
        public async Task Authentication_ReturnsToken_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new SigninRequestModel
            {
                EmailId = "test@example.com",
                Password = "password123"
            };

            var mockResponse = new ResponseEntity<string>
            {
                Result = "Login Successful",
                Token = "mock_token_123",
                IsSuccess = true
            };

            _mockContactBal
                .Setup(bal => bal.Authentication(loginRequest))
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.Authentication(loginRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            var response = (ResponseEntity<string>)jsonResult.Value;

            Assert.AreEqual("mock_token_123", response.Token);
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual("Login Successful", response.Result);
        }

        [TestMethod]
        public async Task Authentication_ReturnsFailure_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginRequest = new SigninRequestModel
            {
                EmailId = "invalid@example.com",
                Password = "wrongpassword"
            };

            var mockResponse = new ResponseEntity<string>
            {
                Result = "Invalid Credentials",
                Token = null,
                IsSuccess = false
            };

            _mockContactBal
                .Setup(bal => bal.Authentication(loginRequest))
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.Authentication(loginRequest);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            var response = (ResponseEntity<string>)jsonResult.Value;

            Assert.IsNull(response.Token);
            Assert.IsFalse(response.IsSuccess);
            Assert.AreEqual("Invalid Credentials", response.Result);
        }



        // RegisterEmployee Tests

        [TestMethod]
        public async Task RegisterEmployee_ReturnsSuccess_WhenModelIsValid()
        {
            // Arrange
            var contact = new ContactViewEntity { FirstName = "John", LastName = "Doe" };

            _mockContactBal
                .Setup(bal => bal.PostEmployee(contact))
                .ReturnsAsync(new ResponseEntity<string>
                {
                    Result = "Contact added successfully",
                    IsSuccess = true
                });

            // Act
            var result = await _controller.RegisterEmployee(contact);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            var response = (ResponseEntity<string>)jsonResult.Value;

            Assert.AreEqual("Contact added successfully", response.Result);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestMethod]
        public async Task RegisterEmployee_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var contact = new ContactViewEntity { FirstName = "", LastName = "Doe" }; // Invalid model
            _controller.ModelState.AddModelError("FirstName", "FirstName is required");

            // Act
            var result = await _controller.RegisterEmployee(contact);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        // DeleteContact Tests

        [TestMethod]
        public async Task DeleteContact_ReturnsSuccess_WhenContactExists()
        {
            // Arrange
            var contactId = 1;

            _mockContactBal
                .Setup(bal => bal.DeleteContact(contactId))
                .ReturnsAsync(new ResponseEntity<bool>
                {
                    Result = true,
                    IsSuccess = true
                });

            // Act
            var result = await _controller.DeleteContact(contactId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            var response = (ResponseEntity<bool>)jsonResult.Value;

            Assert.IsTrue(response.Result);
            Assert.IsTrue(response.IsSuccess);
        }

        // GetByContactID Tests

        [TestMethod]
        public async Task GetByContactID_ReturnsContact_WhenContactExists()
        {
            // Arrange
            var contactId = 1;
            var mockContact = new ContactViewEntity
            {
                ContactId = contactId,
                FirstName = "John",
                LastName = "Doe"
            };

            _mockContactBal
                .Setup(bal => bal.GetByContactID(contactId))
                .ReturnsAsync(new ResponseEntity<ContactViewEntity>
                {
                    Result = mockContact,
                    IsSuccess = true
                });

            // Act
            var result = await _controller.GetByContactID(contactId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            var response = (ResponseEntity<ContactViewEntity>)jsonResult.Value;

            Assert.AreEqual(contactId, response.Result.ContactId);
            Assert.IsTrue(response.IsSuccess);
        }

        [TestMethod]
        public async Task GetByContactID_ReturnsNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            var contactId = 99; // Non-existent contact ID

            _mockContactBal
                .Setup(bal => bal.GetByContactID(contactId))
                .ReturnsAsync(new ResponseEntity<ContactViewEntity>
                {
                    Result = null,
                    IsSuccess = false,
                    ResponseMessage = "Contact not found"
                });

            // Act
            var result = await _controller.GetByContactID(contactId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            var jsonResult = (JsonResult)result;
            var response = (ResponseEntity<ContactViewEntity>)jsonResult.Value;

            Assert.IsNull(response.Result);
            Assert.IsFalse(response.IsSuccess);
            Assert.AreEqual("Contact not found", response.ResponseMessage);
        }
    }
}
