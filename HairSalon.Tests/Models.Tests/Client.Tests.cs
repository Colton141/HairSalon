using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantList.Models;
using System.Collections.Generic;
using System;

namespace RestaurantList.Tests
{
  [TestClass]
  public class RestaurantTest : IDisposable
  {

    public void Dispose()
    {
      Restaurant.ClearAll();
    }

    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_list_test;";
    }

    [TestMethod]
    public void RestaurantConstructor_CreatesInstanceOfRestaurant_Restaurant()
    {
      Restaurant newRestaurant = new Restaurant("test", 1);
      Assert.AreEqual(typeof(Restaurant), newRestaurant.GetType());
    }

    [TestMethod]
    public void SetName_SetName_String()
    {
      string name = "Restaurant Name";
      Restaurant newRestaurant = new Restaurant(name, 1);

      string updatedName = "Do the dishes";
      newRestaurant.SetName(updatedName);
      string result = newRestaurant.Name;

      Assert.AreEqual(updatedName, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_RestaurantList()
    {
      List<Restaurant> newList = new List<Restaurant> { };

      List<Restaurant> result = Restaurant.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsRestaurants_RestaurantList()
    {
      string name01 = "Walk the dog";
      string name02 = "Wash the dishes";
      Restaurant newRestaurant1 = new Restaurant(name01, 1);
      newRestaurant1.Save();
      Restaurant newRestaurant2 = new Restaurant(name02, 1);
      newRestaurant2.Save();
      List<Restaurant> newList = new List<Restaurant> { newRestaurant1, newRestaurant2 };

      List<Restaurant> result = Restaurant.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectRestaurantFromDatabase_Restaurant()
    {
      Restaurant testRestaurant = new Restaurant("Generic Name", 1);
      testRestaurant.Save();

      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.Id);

      Assert.AreEqual(testRestaurant, foundRestaurant);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Restaurant()
    {
      Restaurant firstRestaurant = new Restaurant("Generic Name", 1);
      Restaurant secondRestaurant = new Restaurant("Generic Name", 1);

      Assert.AreEqual(firstRestaurant, secondRestaurant);
    }

    [TestMethod]
    public void Save_SavesToDatabase_RestaurantList()
    {
      Restaurant testRestaurant = new Restaurant("Generic Name", 1);

      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Edit_UpdatesRestaurantInDatabase_String()
    {
      Restaurant testRestaurant = new Restaurant("Restaurant Name", 1);
      testRestaurant.Save();
      string secondName = "Generic Name";

      testRestaurant.Edit(secondName);
      string result = Restaurant.Find(testRestaurant.Id).Name;

      Assert.AreEqual(secondName, result);
    }

    [TestMethod]
    public void DeleteRestaurant_UpdatesRestaurantInDatabase_String()
    {
      string firstRestaurant = "Other Name";
      string secondRestaurant = "Generic Name";
      Restaurant testRestaurant = new Restaurant(firstRestaurant, 1);
      Restaurant testRestaurant2 = new Restaurant(secondRestaurant, 1);
      testRestaurant.Save();
      testRestaurant2.Save();

      testRestaurant.DeleteRestaurant( );

      int testId = testRestaurant2.Id;

      Assert.AreEqual(testId, Restaurant.GetAll()[0].Id);
    }

  }
}
