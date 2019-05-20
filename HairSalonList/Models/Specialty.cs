using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalonList.Models
{
  public class Specialty
  {
    public string Title {get; set;}
    public int Id {get; set;}

    public Specialty(string specialtyTitle, int id = 0)
    {
      this.Title = specialtyTitle;
      this.Id = id;
    }

    public static Specialty Find(int specialtyId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialtys WHERE id = @specialtyId;";
      cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      rdr.Read();
      int id = rdr.GetInt32(0);
      string specialtyTitle = rdr.GetString(1);
      Specialty foundSpecialty = new Specialty(specialtyTitle, id);
      conn.Close();
      return foundSpecialty;
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialtys = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialtys;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {

        int id = rdr.GetInt32(0);
        string specialtyTitle = rdr.GetString(1);
        Specialty newSpecialty = new Specialty(specialtyTitle, id);
        allSpecialtys.Add(newSpecialty);
      }
      conn.Close();
      return allSpecialtys;
    }

    // public override bool Equals(System.Object otherStylist)
    // {
    //   if (!(otherStylist is Stylist))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Stylist newStylist = (Stylist) otherStylist;
    //     bool idEquality = (this.GetId() == newStylist.GetId());
    //     bool nameEquality = (this.GetStylistName() == newStylist.GetStylistName());
    //     return (idEquality && nameEquality);
    //   }
    // }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialtys (specialtyTitle) VALUES (@specialtyTitle);";
      cmd.Parameters.AddWithValue("@specialtyTitle", this.Title);
      cmd.ExecuteNonQuery();
      this.Id = (int) cmd.LastInsertedId;
      conn.Close();
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialtys WHERE id = @SpecialtyId; DELETE FROM stylist_specialtys WHERE specialty_id = @SpecialtyId;";
      cmd.Parameters.AddWithValue("@SpecialtyId", this.Id);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }


    public void Remove()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylist_specialtys WHERE specialty_id = @SpecialtyId;";
      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@SpecialtyId";
      specialtyIdParameter.Value = this.Id;
      cmd.Parameters.Add(specialtyIdParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }


    public List<Stylist> GetStylists(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylist.* FROM specialtys
      JOIN stylist_specialtys ON (specialtys.id = stylist_specialtys.specialty_id)
      JOIN stylist ON (stylist_specialtys.stylist_id = stylist.id)
      WHERE specialtys.id = @SpecialtyId;";
      // MySqlParameter SpecialtyIdParameter = new MySqlParameter();
      // SpecialtyIdParameter.ParameterName = "@SpecialtyId";
      // SpecialtyIdParameter.Value = _id;
      // cmd.Parameters.Add(SpecialtyIdParameter);
      cmd.Parameters.AddWithValue("@SpecialtyId", id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist>{};
      while(rdr.Read())
      {
        int thisStylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist foundStylist = new Stylist(stylistName, thisStylistId);
        stylists.Add(foundStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylists;
    }
  }
}
