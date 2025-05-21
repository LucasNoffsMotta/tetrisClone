using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Engine
{
    internal static class Globals
    {
        public static SpriteBatch SpriteBatch { get; set; }

        public static ContentManager Content { get; set; }
        public static float Time { get; set; }

        public static Point WindowSize { get; set; }

        public static Point PlayFieldSize { get; set; }

        public static Point PlayFieldStartPos { get; set; }

        public static bool GameOver { get; set; }

        public static int Score { get; set; }

        public static int Level { get; set; }

        public static int LinesCleaned { get; set; }

        public static void Update(GameTime gt)
        {
            Time = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}