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
        public void OnGet(int a�o, int? mes)
        {
            ViewData["a�o"] = a�o;
            ViewData["mes"] = mes;

        }
    }
}
