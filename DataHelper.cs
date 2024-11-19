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

namespace Tetris
{
    public static class DataHelper
    {
        private static List<Dictionary<string, string>> playersOnJson = new List<Dictionary<string, string>>();
        private static Dictionary<string, string> playerData = new Dictionary<string, string>();
        private static List<Dictionary<string, string>> readPlayerData = new List<Dictionary<string, string>>();
        private static string fileName = "playerData.json";
        

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
            readPlayerData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(File.ReadAllText(fileName));
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

            readPlayerData.Add(playerData);
            string jsonData = System.Text.Json.JsonSerializer.Serialize(readPlayerData);
            File.WriteAllText(fileName, jsonData);
        }

        public static async void CreateJson()
        {
                await using FileStream createStream = File.Create(fileName);
        }
    }
}
