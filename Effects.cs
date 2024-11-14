using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Tetris
{
    public static class Effects
    {
        public static Texture2D gameOvertexture;
        public static Texture2D effectSquare;
        private static float nextRowCount = 0;
        private static float addToNextRouwCount = 0.3f;
        private static float nextRowCountMax = 1f;
        public static bool EffectFinished = false;
        private static int TemporaryCount = 0;
        private static SpriteFont font = Globals.Content.Load<SpriteFont>("File");

        public static void GameOverEffect(Square[,] PlayField, List<Brick> bricks)
        {
            Debug.WriteLine("Function called");
            gameOvertexture = Globals.Content.Load<Texture2D>("BackGroundTile2");

            nextRowCount += addToNextRouwCount;


            if ((int)nextRowCount < (int)bricks.Count)
            {
                bricks[(int)nextRowCount].Texture = gameOvertexture;
            }

  
            else if (nextRowCount >= bricks.Count) { nextRowCount = 0; TemporaryCount++;  EffectFinished = true; }
        }

        public static void DrawGameOver()
        {
            Globals.SpriteBatch.DrawString(font, "Game Over", new Vector2(415,320), Microsoft.Xna.Framework.Color.Red);
        }
    }
}
