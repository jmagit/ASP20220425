using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemosMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemosMVC.Controllers {
    [Authorize]
    public class DemosController : Controller {
        public IActionResult Index([FromHeader(Name = "accept-language")] string idioma) {
            var dao = new PersonaRepository();
            Persona item;
            if (true) {
                item = dao.Get(1);
            } else {
                item = dao.Get(2);
            }
            ViewBag.Idioma = idioma; // -->   ViewData["Idioma"] = idioma;
            ViewData["item"] = item;
            ViewData["listado"] = dao.Get();
            return View();
        }
        public IActionResult Details() {
            var dao = new PersonaRepository();
            return View(dao.Get(101));
        }
        public IActionResult Edit() {
            var dao = new PersonaRepository();
            return View(dao.Get(101)); 
        }
        [HttpPost]
        public IActionResult Edit(Persona item) {
            if(ModelState.IsValid) {
                var dao = new PersonaRepository();

            } else {
                ModelState.AddModelError("", "Esto es para el sumario");
                ModelState.AddModelError("Edad", "Esto es para la edad");
            }
            return View(item);
        }

        [AcceptVerbs("GET", "POST")] 
        public IActionResult VerifyFecha([FromQuery(Name = "FechaNacimiento")] DateTime fecha) {
            return fecha.Date.CompareTo(DateTime.Today) > 0 ? Json($"Todavía no ha nacido") : Json(true);
        }

        [Route("ejemplo/json/{id}")]
        [RequireHttps]
        public IActionResult Json(int? id) {
            if (id == null)
                return StatusCode(404);
            if(id == 100)
                return StatusCode(401);
            var dao = new PersonaRepository();
            return Json(dao.Get(id.Value));
        }
    }
}
