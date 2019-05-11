using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantList.Models;
using System.Collections.Generic;
using System;

namespace RestaurantList.Tests
{
  [TestClass]
  public class CuisineTest: IDisposable
  {
    public void Dispose()
    {
      Cuisine.ClearAll();
      Restaurant.ClearAll();
    }
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_list_test;";
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Cuisine()
    {
      Cuisine firstCuisine = new Cuisine("Edible");
      Cuisine secondCuisine = new Cuisine("Edible");

      Assert.AreEqual(firstCuisine, secondCuisine);
    }

    [TestMethod]
    public void GetAll_ReturnsAllCuisineObjects_CuisineList()
    {
      string name01 = "Edible";
      string name02 = "Not Edible";
      Cuisine newCuisine1 = new Cuisine(name01);
      newCuisine1.Save();
      Cuisine newCuisine2 = new Cuisine(name02);
      newCuisine2.Save();
      List<Cuisine> newList = new List<Cuisine> { newCuisine1, newCuisine2 };

      List<Cuisine> result = Cuisine.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }



    [TestMethod]
    public void CuisineConstructor_CreatesInstanceOfCuisine_Cuisine()
    {
      Cuisine newCuisine = new Cuisine("test cuisine");
      Assert.AreEqual(typeof(Cuisine), newCuisine.GetType());
    }


    [TestMethod]
    public void Save_SavesCuisineToDatabase_CuisineList()
    {
      Cuisine testCuisine = new Cuisine("Edible Food");
      testCuisine.Save();

      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCuisine_Id()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Edible Food");
      testCuisine.Save();

      //Act
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.Id;
      int testId = testCuisine.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCuisineInDatabase_Cuisine()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Edible Food");
      testCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.Id);

      //Assert
      Assert.AreEqual(testCuisine, foundCuisine);
    }

    [TestMethod]
    public void GetRestaurants_ReturnsEmptyRestaurantList_RestaurantList()
    {
      //Arrange
      string name = "Food Restaurant";
      Cuisine newCuisine = new Cuisine(name);
      List<Restaurant> newList = new List<Restaurant> { };

      //Act
      List<Restaurant> result = newCuisine.GetRestaurants();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetRestaurants_RetrievesAllRestaurantsWithCuisine_RestaurantList()
    {
      //Arrange, Act
      Cuisine testCuisine = new Cuisine("NonEdible Food");
      testCuisine.Save();
      Restaurant firstRestaurant = new Restaurant("Food Restaurant", testCuisine.Id);
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Good Restaurant", testCuisine.Id);
      secondRestaurant.Save();
      List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

      //Assert
      CollectionAssert.AreEqual(testRestaurantList, resultRestaurantList);
    }


  }
}
