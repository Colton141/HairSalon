using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalonList.Models;
using System.Collections.Generic;
using System;

namespace HairSalonList.Tests
{
  [TestClass]
  public class SpecialtyTest: IDisposable
  {
    public void Dispose()
    {
      Specialty.ClearAll();
      Client.ClearAll();
      Stylist.ClearAll();
    }
    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=colton_lacey_test;";
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Specialty()
    {
      Specialty firstSpecialty = new Specialty("Chad");
      Specialty secondSpecialty = new Specialty("Chad");

      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }

    [TestMethod]
    public void GetAll_ReturnsAllSpecialtyObjects_SpecialtyList()
    {
      string name01 = "Chad";
      string name02 = "Steve";
      Specialty newSpecialty1 = new Specialty(name01);
      newSpecialty1.Save();
      Specialty newSpecialty2 = new Specialty(name02);
      newSpecialty2.Save();
      List<Specialty> newList = new List<Specialty> { newSpecialty1, newSpecialty2 };

      List<Specialty> result = Specialty.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }



    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
      Specialty newSpecialty = new Specialty("test stylist");
      Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
    }


    [TestMethod]
    public void Save_SavesSpecialtyToDatabase_SpecialtyList()
    {
      Specialty testSpecialty = new Specialty("Chad");
      testSpecialty.Save();

      List<Specialty> result = Specialty.GetAll();
      List<Specialty> testList = new List<Specialty>{testSpecialty};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToSpecialty_Id()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Billy Jo");
      testSpecialty.Save();

      //Act
      Specialty savedSpecialty = Specialty.GetAll()[0];

      int result = savedSpecialty.Id;
      int testId = testSpecialty.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsSpecialtyInDatabase_Specialty()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Good Specialty");
      testSpecialty.Save();

      //Act
      Specialty foundSpecialty = Specialty.Find(testSpecialty.Id);

      //Assert
      Assert.AreEqual(testSpecialty, foundSpecialty);
    }

    [TestMethod]
    public void GetClients_ReturnsEmptyHairSalonList_HairSalonList()
    {
      //Arrange
      string name = "Client";
      Specialty newSpecialty = new Specialty(name);
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = newSpecialty.GetClients();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithSpecialty_HairSalonList()
    {
      //Arrange, Act
      Specialty testSpecialty = new Specialty("Bailey");
      testSpecialty.Save();
      Client firstClient = new Client("Test Client", testSpecialty.Id);
      firstClient.Save();
      Client secondClient = new Client("Test Client 2", testSpecialty.Id);
      secondClient.Save();
      List<Client> testHairSalonList = new List<Client> {firstClient, secondClient};
      List<Client> resultHairSalonList = testSpecialty.GetClients();

      //Assert
      CollectionAssert.AreEqual(testHairSalonList, resultHairSalonList);
    }


    [TestMethod]
    public void GetStylist()
    {
    Specialty testSpecialty = new Specialty("Bailey");
    testSpecialty.Save();
    Stylist firstStylist = new Stylist("Test Stylist", "Test1", 1);
    firstStylist.Save();
    Stylist secondStylist = new Stylist("Test Stylist 2", "Test2", 2);
    secondStylist.Save();

    testSpecialty.AddStylist(firstStylist);
    List<Stylist> testHairSalonList = new List<Stylist> {firstStylist};
    List<Stylist> resultHairSalonList = testSpecialty.GetStylists(1);

    //Assert
    CollectionAssert.AreEqual(testHairSalonList, resultHairSalonList);
  }




    // [TestMethod] public void GetStylists() { //Arrange, Act //create a stylist //create a specialty //add a stylist to a specialty or vice versa (AssignSpecialty method) // create test list of specialties with that specialty // run the GetStylists method on your stylist object //Assert
    // }


  }
}
