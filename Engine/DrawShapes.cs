using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Engine
{
    public static class DrawShapes
    {       
        public static Texture2D CreateVerticalLine(Vector2 startPos, Vector2 endPos, int tickness, Color color)
        {
            var distance = (int)Vector2.Distance(startPos, endPos);
            Texture2D texture = new Texture2D(Globals.SpriteBatch.GraphicsDevice, tickness, distance);

            var data = new Color[distance * tickness];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            texture.SetData(data);       
            return texture;
        }
        

    }
}
