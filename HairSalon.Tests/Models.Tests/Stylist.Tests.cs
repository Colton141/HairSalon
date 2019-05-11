using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest: IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
    }
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=colton_lacey_test;";
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    {
      Stylist firstStylist = new Stylist("Edible");
      Stylist secondStylist = new Stylist("Edible");

      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      string name01 = "Edible";
      string name02 = "Not Edible";
      Stylist newStylist1 = new Stylist(name01);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name02);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

      List<Stylist> result = Stylist.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }



    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("test cuisine");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }


    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      Stylist testStylist = new Stylist("Edible Food");
      testStylist.Save();

      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("Edible Food");
      testStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.Id;
      int testId = testStylist.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("Good Stylist");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.Id);

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_ReturnsEmptyHairSalon_HairSalon()
    {
      //Arrange
      string name = "Food Client";
      Stylist newStylist = new Stylist(name);
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = newStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylist_HairSalon()
    {
      //Arrange, Act
      Stylist testStylist = new Stylist("NonEdible Food");
      testStylist.Save();
      Client firstClient = new Client("Test Client", testStylist.Id);
      firstClient.Save();
      Client secondClient = new Client("Test Client 2", testStylist.Id);
      secondClient.Save();
      List<Client> testHairSalon = new List<Client> {firstClient, secondClient};
      List<Client> resultHairSalon = testStylist.GetClients();

      //Assert
      CollectionAssert.AreEqual(testHairSalon, resultHairSalon);
    }


  }
}
