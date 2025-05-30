﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SharpDX.XAudio2;
using System.IO;
using System.Diagnostics;
using SharpDX.MediaFoundation.DirectX;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using Tetris.Engine;
using Tetris.ExternalAPI;

namespace Tetris.Miscellanea
{
    public static class DataHelper
    {
        //Novo
        public static List<ScoreData> ranking = new List<ScoreData>();

        public static async void LoadRanking()
        {
            
            APICallercs apICallercs = new APICallercs();
            string response = await apICallercs.GetScoresAsync();

            if(response != "")
            {
                ranking = JsonConvert.DeserializeObject<List<ScoreData>>(response);
            }

            else
            {
                for(int i = 0; i <=2; i++)
                {
                    ranking.Add(new ScoreData() { Score = "Offline" });
                }            
            }
        }

        public static async void SaveRanking()
        {
            ScoreData scoreData = new ScoreData();
            scoreData.Score = Globals.Score.ToString();
            scoreData.Level = Globals.Level.ToString();
            APICallercs apiCaller = new APICallercs();
            await apiCaller.PostScoreAsync(scoreData);   
        }
    }
}
