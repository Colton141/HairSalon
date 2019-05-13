using Microsoft.AspNetCore.Mvc;

namespace RestaurantList.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [Produces("text/html")]
    [Route("/favorite_photos")]
    public ActionResult FavoritePhotos()
    {
      return View();
    }

  }
}
