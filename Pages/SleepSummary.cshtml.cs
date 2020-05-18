using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Produce_WebApp.Models;

namespace Produce_WebApp
{
    public class SleepSummaryModel : PageModel
    {
        public ComputedDataModel computedDataModel { get; set; }
        public void OnGet()
        {
            computedDataModel = DataDisplay.computedDataModel;
        }
    }
}