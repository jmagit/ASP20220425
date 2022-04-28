using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemosPages.Pages.Productos
{
    public class ProductosModel : PageModel
    {
        public void OnGet(int ano, int? mes)
        {
            ViewData["a�o"] = ano;
            ViewData["mes"] = mes;

        }
    }
}
