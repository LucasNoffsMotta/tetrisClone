using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.ExternalAPI
{
    internal class APIHelper
    {
        //Cria o cliente HTTP que sera usado na aplicacao ( 1 por app )
        public static HttpClient APIClient { get; set; }

       //Inicializa o cliente Http 
        public static void InitializeAPIClient()
        {
            APIClient = new HttpClient();
            APIClient.BaseAddress = new Uri(@"http://localhost:5278/playRecords");
            APIClient.DefaultRequestHeaders.Accept.Clear();
            //Ira procurar apenas por JSON
            APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
