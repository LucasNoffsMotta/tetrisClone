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
        public bool descended, canMoveDownFloor, canMoveDownRect;



        public Brick(Texture2D Texture, Vector2 Position) : base(Texture, Position)
        {
            this.Texture = Texture;
            this.Position = Position;
            Rectangle.X = (int)(Position.X * 32) + Globals.PlayFieldStartPos.X; Rectangle.Y = (int)(Position.Y * 32) + Globals.PlayFieldStartPos.Y;
            alive = true;
            coliding = false;
            mapPos.x = (Rectangle.X - Globals.PlayFieldStartPos.X) / 32;
            mapPos.y = (Rectangle.Y - Globals.PlayFieldStartPos.Y) / 32;
            descended = false;
            canMoveDownFloor = true;
            canMoveDownRect = true;
        }

        public void CheckFallColision(Square[,] PlayField)
        {
            if (Rectangle.Bottom >= Globals.PlayFieldSize.Y + Globals.PlayFieldStartPos.Y)
            {                
                 canMoveDownFloor = false;                
            }

            if (Rectangle.Bottom < Globals.PlayFieldSize.Y + Globals.PlayFieldStartPos.Y)
            {
                canMoveDownFloor = true;
            }
        }

        public void CheckRectColision(Square[,] PlayField)

        {
            if (Rectangle.Y > 32)
            {
                if (PlayField[mapPos.x, mapPos.y + 1].ocupied)
                {
                    canMoveDownRect = false;
                }

                else
                {
                    canMoveDownRect = true;
                }
            }
        }


        public void CheckStopCondition(float fallTrigger, float fallSpeed)
        {
            if (!canMoveDownFloor || !canMoveDownRect && fallTrigger >= fallSpeed)
            {
                coliding = true;
                alive = false;
            }
        }

        public void UpdateMapPos()
        {
            mapPos.x = (Rectangle.X - Globals.PlayFieldStartPos.X) / 32;
            mapPos.y = (Rectangle.Y - Globals.PlayFieldStartPos.Y) / 32;
        }


        public void Update(Square[,] PlayField, bool canMoveLeft, bool canMoveRight, float fallTrigger, float fallSpeed)
        {
            CheckFallColision(PlayField);
            CheckRectColision(PlayField);
            CheckStopCondition(fallTrigger, fallSpeed);          
            UpdateMapPos();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}