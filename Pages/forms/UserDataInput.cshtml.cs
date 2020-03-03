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
            
            var ListOfDoubles = StartDataFlow();
            //Pass the Computed Data for display.
            PassToDataDisplay(ListOfDoubles);
            return RedirectToPage("/ProductivitySummary", new { });
        }

        public List<double> StartDataFlow()
        {
            //Creates an instance of th Flow controller
            controller = new FlowController();
            //Stores the result of the SecureComputation in a List of doubles.
            var ComputationResult = controller.StartDataProcessing(UserDataPlain);
            
            return ComputationResult;
        }

        public void PassToDataDisplay(List<double> DecryptedDoubleList)
        {
            DataDisplay.Age = DecryptedDoubleList[0];
            DataDisplay.HoursPerWeek = DecryptedDoubleList[6];
            DataDisplay.Height = DecryptedDoubleList[2];
            DataDisplay.Weight = DecryptedDoubleList[3];
            DataDisplay.Sleep = DecryptedDoubleList[5];
            DataDisplay.SleepLoss = DecryptedDoubleList[8];
            DataDisplay.Breaks = DecryptedDoubleList[1];
            //DataDisplay.BreaksDeficit = DecryptedDoubleList[];
            DataDisplay.Hydration = DecryptedDoubleList[4];
            DataDisplay.HydrationLoss = DecryptedDoubleList[9];
            DataDisplay.TotalProductivityLoss = DecryptedDoubleList[12];
            DataDisplay.WeeklySalary = DecryptedDoubleList[10];
        }

    }
}