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

namespace Tetris
{
    public static class DataHelper
    {
        private static Dictionary<string, string> playerData = new Dictionary<string, string>();
        private static List<Dictionary<string, string>> jsonFormatPlayerData = new List<Dictionary<string, string>>();
        private static string fileName = "playerData.json";
        public static List<int> scores = new List<int>();
        public static List<int> levels = new List<int>();
        

        public static void LoadJson()
        {
            if(!File.Exists(fileName))
            {
                CreateJson();
            }
            ReadFromJson();
        }

        public static void ReadFromJson()
        {
            using StreamReader r = new(fileName);
            string jsonScoreList = r.ReadToEnd();
            jsonFormatPlayerData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(File.ReadAllText(fileName));
            OrganizePlayerDataList();
        }

        public static void OrganizePlayerDataList()
        {

            scores.Clear();
            levels.Clear();

            for(int i = 0; i < jsonFormatPlayerData.Count; i++)
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
            playerData["score"] = Globals.Score.ToString();
            playerData["level"] = Globals.Level.ToString();

            if (File.Exists(fileName))
            {
                LoadJson();
            }

            else
            {
                CreateJson();
            }

            jsonFormatPlayerData.Add(playerData);
            string jsonData = System.Text.Json.JsonSerializer.Serialize(jsonFormatPlayerData);
            File.WriteAllText(fileName, jsonData);
        }

        public static async void CreateJson()
        {
                await using FileStream createStream = File.Create(fileName);
        }
    }
}
