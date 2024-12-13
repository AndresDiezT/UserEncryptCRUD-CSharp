using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserEncrypt.Context;

namespace UserEncrypt.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserEncryptContext db = new UserEncryptContext();

        [Authorize]
        public ActionResult Index()
        {
            var totalPersonas = db.People.Count();
            var totalUsuarios = db.Users.Count();
            var ultimosRegistros = db.Users.OrderByDescending(u => u.CreatedAt)
                                    .Take(5)
                                    .ToList();

            ViewBag.TotalPersonas = totalPersonas;
            ViewBag.TotalUsuarios = totalUsuarios;
            ViewBag.UltimosRegistros = ultimosRegistros;

            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}