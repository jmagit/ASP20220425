using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemosPages.Pages {
    public class DemosModel : PageModel {
        [ViewData]
        public string Nombre { get; set; }
        public void OnGet() {
            Nombre = "mundo";
        }
    }
}
