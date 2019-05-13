
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalonList.Controllers;
using HairSalonList.Models;

namespace HairSalonList.Tests
{
    [TestClass]
    public class ClientControllerTest
    {

      [TestMethod]
      public void Create_ReturnsCorrectActionType_RedirectToActionResult()
      {
        //Arrange
        ClientsController controller = new ClientsController();

        //Act
        IActionResult view = controller.Create("Billy Jo");

        //Assert
        Assert.IsInstanceOfType(view, typeof(RedirectToActionResult));
      }

      [TestMethod]
      public void Create_RedirectsToCorrectAction_Index()
      {
        //Arrange
        ClientsController controller = new ClientsController();
        RedirectToActionResult actionResult = controller.Create("Billy Jo") as RedirectToActionResult;

        //Act
        string result = actionResult.ActionName;

        //Assert
        Assert.AreEqual(result, "Index");
      }

    }
}
