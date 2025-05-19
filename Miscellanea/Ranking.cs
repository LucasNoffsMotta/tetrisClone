using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Engine;
using Tetris.Miscellanea;

namespace Tetris
{
    public static class Ranking
    {
        //710,470

        private static readonly SpriteFont font = Globals.Content.Load<SpriteFont>("File");
        private static Vector2 initialRankingpos = new(710, 470);
        private static Vector2 [] rankingPositions = [new(initialRankingpos.X, initialRankingpos.Y), new(initialRankingpos.X, initialRankingpos.Y + 50), new(initialRankingpos.X, initialRankingpos.Y + 100)];
        

        public static void Draw()
        {
            if (DataHelper.scores.Count > 0 && DataHelper.scores.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    Globals.SpriteBatch.DrawString(font, DataHelper.scores[i].ToString(), rankingPositions[i], Color.White);
                }
            }           
        }
    }
}
