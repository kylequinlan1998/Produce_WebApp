using Microsoft.Research.SEAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Produce_WebApp.Api
{
	public class TestRequest
	{
        public TestRequest()
        {
            CallWebAPIAsync()
                .Wait();
        }
        
        static async Task CallWebAPIAsync()
        {
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here.  
                //Setting the Base address to send the Http Requests.
                client.BaseAddress = new Uri("http://localhost:7071/");
                client.DefaultRequestHeaders.Accept.Clear();
                //Accept Headers that are in Json Format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Function1");
                //Used for a Get Request//Currently Working dont fuck about with it atm.
                //HttpResponseMessage response = await client.GetAsync("api/Function1");
                if (response.IsSuccessStatusCode)
                {
                    
                    string department = await response.Content.ReadAsStringAsync();
                    Object obj;
                        
                    obj = JsonConvert.DeserializeObject(department);
                    Debug.WriteLine(obj);


                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }

    }
}
