using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.ExternalAPI
{
    public class APICallercs
    {
        public async Task<string> GetScoresAsync()
        {
            APIHelper.InitializeAPIClient();
            string responseString = string.Empty;

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(APIHelper.APIClient.BaseAddress))
            {
                if (response.IsSuccessStatusCode)
                {
                     responseString = await response.Content.ReadAsStringAsync();
                }
            }

            return responseString;
        }

        public async Task<bool> PostScoreAsync(ScoreData obj)
        {
            APIHelper.InitializeAPIClient();

            using (HttpResponseMessage response = await APIHelper.APIClient.PostAsJsonAsync(APIHelper.APIClient.BaseAddress, obj))
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
