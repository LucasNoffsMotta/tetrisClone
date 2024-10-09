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
        public float fallSpeed;
        public float fallTrigger;
        public float fallCount;
        private float sideMoveTimer;
        private float sideMoveCount;
        private bool moveCounting;
        public bool alive;
        public bool coliding;
        public (int x, int y) mapPos;


        public Brick(Texture2D Texture, Vector2 Position) : base(Texture, Position)
        {
            this.Texture = Texture;
            this.Position = Position;
            fallSpeed = 4f;
            fallTrigger = 0;
            fallCount = 0.1f;
            sideMoveTimer = 0.5f;
            sideMoveCount = 0;
            Rectangle.X = (int)Position.X * 32; Rectangle.Y = (int)Position.Y * 32;
            alive = true;
            coliding = false;
        }


        public void FallTimer()
        {
            fallTrigger += fallCount;
            if (fallTrigger >= fallSpeed) { Rectangle.Y += 32; fallTrigger = 0; }
        }

        public void Fall()
        {
            FallTimer();

            if (InputManager.KeybordPressed.IsKeyDown(Keys.S))
            {
                fallCount = 0.5f;
            }

            else if (InputManager.KeybordPressed.IsKeyUp(Keys.S))
            {
                fallCount = 0.1f;
            }
        }


        public void MoveTimer()
        {
            if (moveCounting) { sideMoveCount += fallCount; }
            if (sideMoveCount >= sideMoveTimer) { sideMoveCount = 0; moveCounting = false; }
        }

        public void Move(Square[,] PlayField, bool canMoveLeft, bool canMoveRight)
        {
                    
            if (InputManager.KeybordPressed.IsKeyDown(Keys.A) && !moveCounting && fallCount != 0.5f)
            {
                if (Rectangle.X > 0 && Rectangle.Y > 0)
                {
                    if (canMoveLeft)
                    {
                        Rectangle.X -= 32;
                        moveCounting = true;
                    }
                }                
            }

            else if (InputManager.KeybordPressed.IsKeyDown(Keys.D) && !moveCounting && fallCount != 0.5f)
            {
                if (Rectangle.Right < Globals.WindowSize.X && Rectangle.Y > 0)
                {
                    if (canMoveRight)
                    {
                        Rectangle.X += 32;
                        moveCounting = true;
                    }
                }
            }
        }

        public void CheckFallColision(Square[,] PlayField )
        {
            if (Rectangle.Bottom >= Globals.WindowSize.Y) 
            {
                coliding = true;               
            }          
        }



        public void CheckRectColision(Square[,] PlayField)

        {
            if (Rectangle.Y > 32)
            {
                if (PlayField[Rectangle.X / 32, Rectangle.Y / 32 + 1].ocupied)
                {
                   
                    coliding = true;
                }
            }           
        }


        public void Update(Square[,] PlayField, bool canMoveLeft, bool canMoveRight)
        {
            
            if (!coliding) Fall();    
            else 
            {
                alive = false;           
            }

            
            CheckFallColision(PlayField);
            Move(PlayField, canMoveLeft, canMoveRight);
            MoveTimer();
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
