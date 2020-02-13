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
        public Controller controller1;
        public string error = "";
        

        public IActionResult OnPostTest()
        {
            //Ensure the Data has been Correctly Entered into the form.
            //If so create an instance of the DataController and pass the Data Model as an argument.
            if (!ModelState.IsValid)
            {
                error = "The Data was not In the correct form,Please try again.";
                testing();
                return Page();

            }
            error = "Is this working";

            return RedirectToPage("/Index", new { city = UserData.Address });
        }

        public void testing()
        {
            BfvEncryption bfv = new BfvEncryption();

        }

    }
}