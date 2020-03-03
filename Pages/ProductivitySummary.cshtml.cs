using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Produce_WebApp
{
    public class ProductivitySummaryModel : PageModel
    {
        public double Age { get; set; }
        public string Name { get; set; }
        public double HoursPerWeek { get; set; }
        public double ProductivityLoss { get; set; }
        public double  SleepDeficit{ get; set; }
        public double WaterDeficit { get; set; }

        public void OnGet()
        {
            Age = DataDisplay.Age;
            HoursPerWeek = DataDisplay.HoursPerWeek;
            //Percentage of lost productivity.
            ProductivityLoss = DataDisplay.TotalProductivityLoss;
        }

        public void testing(int number)
        {
            Debug.WriteLine(number);
        }
    }
}