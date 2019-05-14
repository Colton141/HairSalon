using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalonList.Models;
using System.Collections.Generic;
using System;

namespace HairSalonList.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=colton_lacey_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("test", 1);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void SetName_SetName_String()
    {
      string name = "Client Name";
      Client newClient = new Client(name, 1);

      string updatedName = "billy jo";
      newClient.SetName(updatedName);
      string result = newClient.Name;

      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_HairSalonList()
    {
      List<Client> newList = new List<Client> { };

      List<Client> result = Client.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsClients_HairSalonList()
    {
      string name01 = "Billy Jo";
      string name02 = "Bailey";
      Client newClient1 = new Client(name01, 1);
      newClient1.Save();
      Client newClient2 = new Client(name02, 1);
      newClient2.Save();
      List<Client> newList = new List<Client> { newClient1, newClient2 };

      List<Client> result = Client.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {
      Client testClient = new Client("Generic Name", 1);
      testClient.Save();

      Client foundClient = Client.Find(testClient.Id);

      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Client()
    {
      Client firstClient = new Client("Generic Name", 1);
      Client secondClient = new Client("Generic Name", 1);

      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void Save_SavesToDatabase_HairSalonList()
    {
      Client testClient = new Client("Generic Name", 1);

      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Edit_UpdatesClientInDatabase_String()
    {
      Client testClient = new Client("Client Name", 1);
      testClient.Save();
      string secondName = "Generic Name";

      testClient.Edit(secondName);
      string result = Client.Find(testClient.Id).Name;

      Assert.AreEqual(secondName, result);
    }

    [TestMethod]
    public void DeleteClient_UpdatesClientInDatabase_String()
    {
      string firstClient = "Other Name";
      string secondClient = "Generic Name";
      Client testClient = new Client(firstClient, 1);
      Client testClient2 = new Client(secondClient, 1);
      testClient.Save();
      testClient2.Save();

      testClient.DeleteClient( );

      int testId = testClient2.Id;

      Assert.AreEqual(testId, Client.GetAll()[0].Id);
    }

  }
}
