using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Tetris.Engine;


namespace Tetris.Miscellanea
{
    public static class Effects
    {
        public static Texture2D gameOvertexture;
        public static Texture2D effectSquare;
        private static float nextRowCount = 0;
        private static float addToNextRouwCount = 0.5f;
        private static float nextRowCountMax = 1f;
        public static bool EffectFinished = false;
        private static int TemporaryCount = 0;
        private static SpriteFont font = Globals.Content.Load<SpriteFont>("File");


        public static List<Song> songs = new List<Song> {LoadMusic("tetris-theme-korobeiniki-rearranged-arr-for-strings-185592"),
            LoadMusic("cossack-dance-edm-russian-tetris-electronika-151723"),
            LoadMusic("rasputin-russia-tetris-game-cossack-puzzle-soundtrack-mystery-148250"),
            LoadMusic("tetris-theme-korobeiniki-rearranged-arr-for-music-box-184978")};

        public static float musicVolume = 1f;

        private static Song LoadMusic(string musicName)
        {
            Song music;
            return music = Globals.Content.Load<Song>(musicName);
        }

        public static void GameOverEffect(Square[,] PlayField, List<Brick> bricks)
        {
            gameOvertexture = Globals.Content.Load<Texture2D>("BackGroundTile2");

            nextRowCount += addToNextRouwCount;


            if ((int)nextRowCount < bricks.Count && bricks[(int)nextRowCount].mapPos.y <= 20)
            {
                bricks[(int)nextRowCount].Texture = gameOvertexture;
            }


            else if (nextRowCount >= bricks.Count) { nextRowCount = 0; TemporaryCount++; EffectFinished = true; }
        }

        public static void PlaySoundtrack(int musicIndex)
        {
            MediaPlayer.Play(songs[musicIndex]);
        }

        public static void PauseMusic()
        {
            MediaPlayer.Pause();
            Debug.WriteLine("Pause music");
        }


        public static void DrawGameOver()
        {
            Globals.SpriteBatch.DrawString(font, "Game Over", new Vector2(415, 320), Microsoft.Xna.Framework.Color.Red);
        }
    }
}
