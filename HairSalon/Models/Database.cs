sing System;
using MySql.Data.MySqlClient;
using HairSalonList;

namespace HairSalonList.Models
{
  public class DB
  {
    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
