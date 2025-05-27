using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tetris.Miscellanea;

namespace Tetris.Engine
{
    public static class LevelManager
    {
        public static Dictionary<int, float> speedDictionary = new Dictionary<int, float>();

        public static void UpdateLevel(Tetromines currentTetromine)
        {
            if (Globals.LinesCleaned % 10 == 0)
            {
                Globals.Level += 1;
                currentTetromine.UpdateFallingSpeed();
            }
        }

        public static Dictionary<int, float> SpeedData()
        {
            speedDictionary[0] = 0.8f;
            speedDictionary[1] = 0.71f;
            speedDictionary[2] = 1.26f;
            speedDictionary[3] = 1.45f;
            speedDictionary[4] = 1.71f;
            speedDictionary[5] = 2.08f;
            speedDictionary[6] = 2.66f;
            speedDictionary[7] = 3.69f;
            speedDictionary[8] = 6f;
            speedDictionary[9] = 8f;
            speedDictionary[10] = 9.6f;

            return speedDictionary;
        }



        public static void ReestartGame(Square[,] PlayField, BricksManager _bricksManager)
        {
            for (int x = 0; x < PlayField.GetLength(0); x++)
            {
                for (int y = 0; y < PlayField.GetLength(1); y++)
                {
                    PlayField[x, y].ocupied = false;
                }
            }

            _bricksManager.bricks.Clear();
            _bricksManager.brickObjects.Clear();
            _bricksManager.StartBricks();
            DataHelper.SaveRanking();
            DataHelper.LoadRanking();
            Globals.Score = 0;
            Globals.LinesCleaned = 0;
            Globals.Level = 0;
            Effects.EffectFinished = false;
            Globals.GameOver = false;
            UIScreens.GameStates["StartGame"] = false;
            UIScreens.GameStates["StartScreen"] = true;
        }

        public static void Update(Square[,] PlayField, BricksManager _bricksManager)
        {
            if (InputManager.KeybordPressed.IsKeyDown(Keys.Enter))
            {
                ReestartGame(PlayField, _bricksManager);
            }
        }
    }
}
