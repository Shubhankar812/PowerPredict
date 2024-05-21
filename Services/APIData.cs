using Newtonsoft.Json;
using RemainingUsefulLife.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RemainingUsefulLife.Services
{
    public class APIData
    {
        public static async Task<string> FetchData(double f1, double f2, double f3)
        {
            var new_model = new Data()
            {
                f1 = f1,
                f2 = f2,
                f3 = f3
            };
            HttpClient client = new HttpClient();
          var json = JsonConvert.SerializeObject(new_model);
           var postData = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://remaining-battery-life-1.onrender.com" + "/predict", postData);
           var content = await response.Content.ReadAsStringAsync();
           
            if (response.IsSuccessStatusCode)
            {
                return content;
            }
           
            return "Error";
        }
    }
}
