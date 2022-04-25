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
            return View();
        }
    }
}
