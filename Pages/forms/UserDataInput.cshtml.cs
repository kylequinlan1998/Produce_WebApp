using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Produce_WebApp.Models;
using Produce_WebApp.Encryption;
using Produce_WebApp.DataFlowController;
using Produce_WebApp.Pages;
using System.Web.Helpers;

namespace Produce_WebApp
{
    public class userData : PageModel
    {
        [BindProperty]
        public InputDataModel UserDataPlain { get; set; }
        public FlowController controller;
        public string error = "";
        
        

        public IActionResult OnPostTest()
        {
            //Ensure the Data has been Correctly Entered into the form.
            //If so create an instance of the DataController and pass the Data Model as an argument.
            if (!ModelState.IsValid)
            {
                error = "The Data was not In the correct form,Please try again.";
                return Page();

            }
            error = "working";
            
            var computedDataModel = StartDataFlow();
            //Pass the Computed Data for display.
            PassToDataDisplay(computedDataModel);
            return RedirectToPage("/ProductivitySummary", new { });
        }

        public ComputedDataModel StartDataFlow()
        {
            //Creates an instance of th Flow controller
            controller = new FlowController();
            //Stores the result of the SecureComputation in a List of doubles.
            var ComputationResult = controller.StartDataProcessing(UserDataPlain);
            
            return ComputationResult;
        }

        public void PassToDataDisplay(ComputedDataModel computedDataModel)
        {
            DataDisplay.computedDataModel = computedDataModel;
        }

    }
}