using MyGameScore.Models;
using System;
using System.Web.Mvc;

namespace MyGameScore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult About(JsonResult json)
        {
            ViewBag.Message = "Your application description page.";
            DBContext db = new DBContext();
            Jogo jogo = new Jogo();

            jogo.pontuacao = 14;
            jogo.data_jogo = DateTime.Now;

            db.jogo.Add(jogo);

            db.SaveChanges();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}