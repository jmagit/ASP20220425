using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemosPages.Pages.Productos
{
    public class InformesModel : PageModel
    {
        public void OnGet(int año, int? mes)
        {
            ViewData["año"] = año;
            ViewData["mes"] = mes;

        }
    }
}
