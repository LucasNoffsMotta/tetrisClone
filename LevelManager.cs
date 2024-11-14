using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class LevelManager
    {

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
            Globals.Score = 0;
            Globals.LinesCleaned = 0;
            Effects.EffectFinished = false;
            Globals.GameOver = false;
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
