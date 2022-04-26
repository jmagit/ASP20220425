using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemosMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemosMVC.Controllers {
    public class DemosController : Controller {
        public IActionResult Index() {
            var dao = new PersonaRepository();
            Persona item;
            if (true) {
                item = dao.Get(1);
            } else {
                item = dao.Get(2);
            }
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
        public IActionResult VerifyFecha(DateTime fecha) {
            return fecha.Date.CompareTo(DateTime.Today) == 1 ? Json(true) : Json($"Todavía no ha nacido");
        }

    }
}
