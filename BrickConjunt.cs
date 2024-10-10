using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BrickConjunt
    {
        public List<Brick> bricks;
        private Random random;
        private int initialPos;
        private char bricktype;
        private char[] brickTypes = new char[] { 'O', 'I', 'S', 'Z', 'L', 'J', 'T' };
        public bool alive;
        public int _leftBound;
        public int _rightBound;
        public int _bottom_Bound;
        public int _topBound;
        public bool canMoveLeft;
        public bool canMoveRight;



        public BrickConjunt()
        {
            random = new Random();
            bricks = new List<Brick>();
            initialPos = random.Next(1, Globals.WindowSize.X / 32 - 4);
            CreateBricks(brickTypes[random.Next(brickTypes.Length)]);         
            (_leftBound, _rightBound, _topBound) = GetBounds();
            alive = true;
        }


        public void CreateBricks(char brickType)
        {
            if (brickType == 'O')
            {
                bricks.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, 1))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, 1))));
            }

            if (brickType == 'I')
            { 
                bricks.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos+1, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos+2, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos+3, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos+4, 0))));
            }

            if (brickType == 'L')
            {
                bricks.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, 1))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, 2))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 1, 0))));
            }

            if (brickType == 'J')
            {
                bricks.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, 1))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, 2))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos - 1, 0))));
            }

            if (brickType == 'T')
            {
                bricks.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos, 1))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 1, 1))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos - 1, 1))));
            }


            if (brickType == 'Z')
            {
                bricks.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos,0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos, 1))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 1, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos - 1, 1))));
            }

            if (brickType == 'S')
            {
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, 1))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos - 1, 0))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, 1))));
            }
        }

        public (int _leftBound, int _rightBound, int _topBound) GetBounds()
        {
            _leftBound = bricks[0].Rectangle.X;
            _rightBound = bricks[0].Rectangle.Right;
            _topBound = bricks[0].Rectangle.Top;
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Rectangle.X < _leftBound) { _leftBound = bricks[i].Rectangle.X;}
                if (bricks[i].Rectangle.Right > _rightBound) { _rightBound = bricks[i].Rectangle.Right; }
                if (bricks[i].Rectangle.Top < _topBound) { _topBound = bricks[i].Rectangle.Top; }
            }
            return (_leftBound, _rightBound, _topBound);
        }


        public void CheckFallColision(Square[,] PlayField, Point Size)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].coliding) { UpdateOcupiedFields(PlayField, Size);  alive = false;   }
            }
        }

        public void UpdateOcupiedFields(Square[,] PlayField, Point Size)
        {

            for (int i = 0; i < bricks.Count; i++)
            {
                PlayField[bricks[i].mapPos.x, bricks[i].mapPos.y].ocupied = true;
            }
        }

        public void CheckIfCanMove(Square[,] PlayField)
        {
            if (_topBound >= 32)
            {
                for (int i = 0; i < bricks.Count; i++)
                {
                    if (_leftBound > 0)
                    {
                        if (PlayField[_leftBound / 32 - 1, bricks[i].Rectangle.Y / 32].ocupied == true)
                        {
                            canMoveLeft = false;
                            break;
                        }

                        else
                        {
                            canMoveLeft = true;
                        }
                    }                 


                    if (_rightBound < Globals.WindowSize.X)
                    {
                        if (PlayField[_rightBound / 32, bricks[i].Rectangle.Y / 32].ocupied == true)
                        {
                            canMoveRight = false;
                            break;
                        }

                        else
                        {
                            canMoveRight = true;
                        }
                    }
                    
                    if (_leftBound == 0) { canMoveLeft = false; }
                    if (_rightBound == Globals.WindowSize.X) { canMoveRight = false; }
                }
            }          
        }

        public void Update(Square[,] PlayField, Point Size)
        {
            
            (_leftBound, _rightBound, _topBound) = GetBounds();
            CheckIfCanMove(PlayField);


            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Update(PlayField, canMoveLeft, canMoveRight);
            }
            CheckFallColision(PlayField, Size);
        }
















    }
}
