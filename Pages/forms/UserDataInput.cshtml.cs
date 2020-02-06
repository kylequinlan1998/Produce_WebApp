using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Produce_WebApp.Models;
using Produce_WebApp.Encryption;
using Produce_WebApp.DataFlowController;

namespace Produce_WebApp
{
    public class userData : PageModel
    {
        [BindProperty]
        public UserDataModel UserData { get; set; }
        public string error = "";
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                error = "The Data was not In the correct form,Please try again.";
                DataController controller = new DataController(UserData);
                return Page();

            }
            //DataController controller = new DataController(UserData);
            
            return RedirectToPage("/Index", new { city = UserData.Address });
        }
    }
}