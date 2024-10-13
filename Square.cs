using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System.Diagnostics.Eventing.Reader;

namespace Tetris
{
    public class Square
    {
        
        public Vector2 Position { get; set; }
        protected Texture2D Texture;
        public bool ocupied { get; set; }
        public Vector2 Origin { get; set; }
        public Rectangle Rectangle;

        public Square(Texture2D _texture, Vector2 _position)
        {
            ocupied = false;
            Position = _position;
            this.Texture = _texture;
            Origin = new(Texture.Width, Texture.Height);
            Rectangle = new((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public void Draw()
        {
            if (!ocupied)
            {
                //Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, Origin, 1f, SpriteEffects.None, 0f);
                Globals.SpriteBatch.Draw(Texture, Rectangle, Color.White);
            }
            else
            {          
                //Globals.SpriteBatch.Draw(Texture, Position, null, Color.Yellow, 0f, Origin, 1f, SpriteEffects.None, 0f);
                Globals.SpriteBatch.Draw(Texture, Rectangle, Color.Yellow);
            }
        }

    }
}
