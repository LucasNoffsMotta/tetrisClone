using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.ExternalAPI.Models;

namespace Tetris.ExternalAPI
{
    public class APICallercs
    {
        private readonly string post = @"http://localhost:5135/ScoreBoard/Create";
        private readonly string get = @"http://localhost:5135/ScoreBoard";

        public async Task<string> GetScoresAsync()
        {
            APIHelper.InitializeAPIClient();

            string responseString = string.Empty;

            try
            {
                using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(new Uri(get)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            catch(Exception ex)
            {
                return string.Empty;
            }
          
            return responseString;
        }

        public async Task<bool> PostScoreAsync(Player obj)
        {
            APIHelper.InitializeAPIClient();

            using (HttpResponseMessage response = await APIHelper.APIClient.PostAsJsonAsync(new Uri(post), obj))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
