using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace RestaurantList.Models
{
  public class Cuisine
  {
    public string Name { get; set;}
    public int Id { get; set;}

    public Cuisine(string cuisineName, int id = 0)
    {
      Name = cuisineName;
      Id = id;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = this.Id.Equals(newCuisine.Id);
        bool nameEquality = this.Name.Equals(newCuisine.Name);
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisine (name) VALUES (@name);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCategories = new List<Cuisine> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int CuisineId = rdr.GetInt32(0);
        string CuisineName = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(CuisineName, CuisineId); // <-- This line now has two arguments
        allCategories.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCategories;
    }


    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int CuisineId = 0;
      string CuisineName = "";
      while(rdr.Read())
      {
        CuisineId = rdr.GetInt32(0);
        CuisineName = rdr.GetString(1);
      }
      Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCuisine;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisine;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }


    public List<Restaurant> GetRestaurants()
    {
      List<Restaurant> allCuisineRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = @cuisine_id;";
      MySqlParameter cuisineId = new MySqlParameter();
      cuisineId.ParameterName = "@cuisine_id";
      cuisineId.Value = this.Id;
      cmd.Parameters.Add(cuisineId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int restaurantCuisineId = rdr.GetInt32(2);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantId);
        allCuisineRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisineRestaurants;
    }


    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisine;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void DeleteCuisine()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants WHERE cuisine_id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = Id;
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      cmd.CommandText = @"DELETE FROM cuisine WHERE id = @thisId;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
