using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Engine;

namespace Tetris.Miscellanea
{
    public static class Ranking
    {
        //710,470

        private static readonly SpriteFont font = Globals.Content.Load<SpriteFont>("File");
        private static Vector2 initialRankingpos = new(710, 470);
        private static Vector2[] rankingPositions = [new(initialRankingpos.X, initialRankingpos.Y), new(initialRankingpos.X, initialRankingpos.Y + 50), new(initialRankingpos.X, initialRankingpos.Y + 100)];


        public static void Draw()
        {
            if (DataHelper.ranking.Count > 0 && DataHelper.ranking.Count >= 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    Globals.SpriteBatch.DrawString(font, DataHelper.ranking[i].Score.ToString(), rankingPositions[i], Color.White);
                }
            }
        }
    }
}
