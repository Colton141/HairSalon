using Microsoft.AspNetCore.Mvc;
using RestaurantList.Models;
using System.Collections.Generic;
using System;

namespace RestaurantList.Controllers
{
  public class RestaurantsController : Controller
  {

    [HttpGet("/cuisines/{cuisineId}/restaurants/new")]
    public ActionResult New(int cuisineId)
    {
       Cuisine cuisine = Cuisine.Find(cuisineId);
       return View(cuisine);
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}")]
    public ActionResult Show(int cuisineId, int restaurantId)
    {
      Restaurant restaurant = Restaurant.Find(restaurantId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(cuisineId);
      model.Add("restaurant", restaurant);
      model.Add("cuisine", cuisine);
      return View(model);
    }

    [HttpPost("/restaurants/delete")]
    public ActionResult DeleteAll()
    {
      Restaurant.ClearAll();
      return View();
    }

    [HttpPost("/cuisines/{cuisineId}/restaurants/{restaurantId}/delete")]
    public ActionResult Delete(int cuisineId, int restaurantId)
    {
      Restaurant restaurant = Restaurant.Find(restaurantId);
      restaurant.DeleteRestaurant();
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(cuisineId);
      model.Add("cuisine", cuisine);
      model.Add("restaurant", restaurant);
      return RedirectToAction("Show", "Cuisines");
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}/edit")]
    public ActionResult Edit(int cuisineId, int restaurantId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(cuisineId);
      model.Add("cuisine", cuisine);
      Restaurant restaurant = Restaurant.Find(restaurantId);
      model.Add("restaurant", restaurant);
      return View(model);
    }

    [HttpPost("/cuisines/{cuisineId}/restaurants/{restaurantId}")]
    public ActionResult Update(int cuisineId, int restaurantId, string newName)
    {
      Restaurant restaurant = Restaurant.Find(restaurantId);
      restaurant.Edit(newName);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(cuisineId);
      model.Add("cuisine", cuisine);
      model.Add("restaurant", restaurant);
      return View("Show", model);
    }

    // [HttpGet("/cuisines/{id}")]
    // public ActionResult Show(int id)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Cuisine selectedCuisine = Cuisine.Find(id);
    //   List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
    //   model.Add("cuisine", selectedCuisine);
    //   model.Add("restaurants", cuisineRestaurants);
    //   return View(model);
    // }
  }
}
