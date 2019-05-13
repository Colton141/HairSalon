using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalonList.Models;

namespace HairSalonList.Controllers
{
  public class StylistsController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      List<Stylist> allStylists = Stylist.GetAll();
      newStylist.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("stylistClients", stylistClients);
      return View(selectedStylist);
    }

    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName)
    {
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(clientName, stylistId);
      newClient.Save();
      foundStylist.GetClients();
      // List<Client> stylistClients = foundStylist.GetClients;
      // model.Add("clients", stylistClients);
      // model.Add("stylist", foundStylist);
      return View("Show", foundStylist);
    }

    [HttpPost("/stylists/{stylistId}/delete")]
    public ActionResult Delete(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.DeleteStylist();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", stylist);
      return RedirectToAction("Index");
    }

  }
}
