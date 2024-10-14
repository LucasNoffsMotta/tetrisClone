using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace Tetris
{
    public class Brick : Square
    {
        public Texture2D Texture;
        public Vector2 Position;
        public int _minBound_X, _maxBound_X, _minBound_Y, _maxBound_Y;
        public bool alive;
        public bool coliding;
        public (int x, int y) mapPos;
        public Point boxPosition;


        public Brick(Texture2D Texture, Vector2 Position) : base(Texture, Position)
        {
            this.Texture = Texture;
            this.Position = Position;
            Rectangle.X = (int)Position.X * 32; Rectangle.Y = (int)Position.Y * 32;
            alive = true;
            coliding = false;
            mapPos.x = Rectangle.X / 32;
            mapPos.y = Rectangle.Y / 32;
        } 

        public void CheckFallColision(Square[,] PlayField )
        {
            if (Rectangle.Bottom >= Globals.WindowSize.Y) 
            {
                coliding = true;
                alive = false;
            }          
        }



        public void CheckRectColision(Square[,] PlayField)

        {
            if (Rectangle.Y > 32)
            {
                if (PlayField[Rectangle.X / 32, Rectangle.Y / 32 + 1].ocupied)
                {                
                    coliding = true;
                    alive = false;
                }
            }           
        }


        public void Update(Square[,] PlayField, bool canMoveLeft, bool canMoveRight)
        {
            
            CheckFallColision(PlayField);        
            mapPos.x = Rectangle.X / 32;
            mapPos.y = Rectangle.Y / 32;
           
        }

        public void Draw()
        {
            //Globals.SpriteBatch.Draw(Texture, Position, null, Color.White, 0f, Origin, 1f, SpriteEffects.None, 0f);
            
            Globals.SpriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
