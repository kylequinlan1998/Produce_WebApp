using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Produce_WebApp.Models;

namespace Produce_WebApp
{
    public class ProductivitySummaryModel : PageModel
    {
        public ComputedDataModel computedDataModel { get; set; }

        public void OnGet()
        {
            //Read in ComputedDataModel for displaying of Data.
            computedDataModel = DataDisplay.computedDataModel;
            computedDataModel.TotalProductivityLoss = (computedDataModel.TotalProductivityLoss * 100);
        }

        public void testing(int number)
        {
            Debug.WriteLine(number);
        }
    }
}