using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Produce_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string city { get; set; }
        private readonly ILogger<IndexModel> _logger;
        
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        

        public void test()
        {
           
        }
    }
}
