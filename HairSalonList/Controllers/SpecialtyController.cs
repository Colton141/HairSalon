using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HairSalonList.Models;

namespace HairSalonList.Controllers
{
  public class SpecialtyController : Controller
  {
    public IActionResult Index()
    {
      List<Specialty> allSpecialtys = Specialty.GetAll();
      return View(allSpecialtys);
    }

    public IActionResult New()
    {
      return View();
    }

    [HttpPost("/specialty/create")]
    public IActionResult Create(string specialtyName)
    {
      Specialty newSpecialty = new Specialty(specialtyName);
      newSpecialty.Save();
      return RedirectToAction("Index");
    }

    [HttpPost("/specialtys/{specialtyId}/delete-specialty")]
    public ActionResult DeleteSpecialty(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      specialty.Delete();
      // Dictionary<string, object> model = new Dictionary<string, object>();
      // Stylist foundStylist = Stylist.Find(authorId);
      // List<Specialty> authorSpecialtys = foundStylist.GetSpecialtys();
      // model.Add("specialty", authorSpecialtys);
      return RedirectToAction("Index");
    }

    [HttpGet("/specialty/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Stylist> specialtyStylists = selectedSpecialty.GetStylists(id);
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("specialtyStylists", specialtyStylists);
      return View(model);
    }
  }
}
