using Microsoft.Research.SEAL;
using Newtonsoft.Json;
using Produce_WebApp.DataFlowController;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Produce_WebApp.Api
{
	public class CloudInterface
	{
        public DataController dataController;
        public CloudInterface()
        {
           
        }

        public async Task CallApi(Ciphertext input)
        {
            CallAsyncAPI(input)
                .Wait();
        }
        
        public async Task CallAsyncAPI(Ciphertext input)
        {
            //Create an instance of HttpClient to Post encrypted data to Cloud.
            using (var client = new HttpClient())
            {
                dataController = new DataController();
                //Send HTTP requests from here.  
                //Setting the Base address to send the Http Requests.
                client.BaseAddress = new Uri("http://localhost:7071/");
                client.DefaultRequestHeaders.Accept.Clear();
                //Accept Headers that are in Json Format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/Function1",input);
                //Used for a Get Request//Currently Working dont fuck about with it atm.
                //HttpResponseMessage response = await client.GetAsync("api/Function1");
                if (response.IsSuccessStatusCode)
                {
                    Ciphertext output;
                    string BodyContent = await response.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<Ciphertext>(BodyContent);
                    dataController.DecryptResult(output);
                    //Debug.WriteLine(output);


                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
        }

    }
}
