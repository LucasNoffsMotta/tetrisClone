using System;
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
        private static Dictionary<string, string> playerData = new Dictionary<string, string>();
        private static List<Dictionary<string, string>> jsonFormatPlayerData = new List<Dictionary<string, string>>();
        private static string fileName = "playerData.json";
        public static List<int> scores = new List<int>();
        public static List<int> levels = new List<int>();
        //Novo
        public static List<ScoreData> ranking = new List<ScoreData>();



        public static void LoadJson()
        {
            if (!File.Exists(fileName))
            {
                CreateJson();
            }
            ReadFromJson();
        }

        public static void ReadFromJson()
        {
            using StreamReader r = new(fileName);
            string jsonScoreList = r.ReadToEnd();
            jsonFormatPlayerData.Clear();
            jsonFormatPlayerData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonScoreList);
            OrganizePlayerDataList();
        }

        public static void OrganizePlayerDataList()
        {
            scores.Clear();
            levels.Clear();

            for (int i = 0; i < jsonFormatPlayerData.Count; i++)
            {
                scores.Add(int.Parse(jsonFormatPlayerData[i]["score"]));
                levels.Add(int.Parse(jsonFormatPlayerData[i]["level"]));
            }

            scores.Sort();
            scores.Reverse();
            levels.Sort();
            levels.Reverse();
        }


        public static void SaveScore()
        {
            jsonFormatPlayerData.Clear();
            playerData["score"] = Globals.Score.ToString();
            playerData["level"] = Globals.Level.ToString();
            LoadJson();
            jsonFormatPlayerData.Add(playerData);
            string jsonData = System.Text.Json.JsonSerializer.Serialize(jsonFormatPlayerData);
            File.Delete(fileName);
            File.WriteAllText(fileName, jsonData);
        }

        public static async void SaveScoreOnAPI()
        {

            ScoreData scoreData = new ScoreData();
            scoreData.Score = Globals.Score.ToString();
            scoreData.Level = Globals.Level.ToString();
            APICallercs apiCaller = new APICallercs();
            await apiCaller.PostScoreAsync(scoreData);
        
        }

        public static void CreateJson()
        {
            playerData["score"] = "0";
            playerData["level"] = "0";

            for (int i = 0; i < 3; i++)
            {
                jsonFormatPlayerData.Add(playerData);
            }
            string jsonData = System.Text.Json.JsonSerializer.Serialize(jsonFormatPlayerData);
            File.WriteAllText(fileName, jsonData);
        }
    }
}
