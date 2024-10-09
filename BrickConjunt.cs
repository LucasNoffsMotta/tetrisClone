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
            //CreateBricks(brickTypes[random.Next(brickTypes.Length)]);
            CreateBricks(brickTypes[0]);
            (_leftBound, _rightBound, _topBound) = GetBounds();
            alive = true;
        }


        public void CreateBricks(char brickType)
        {
            if (brickType == 'O')
            {
                //initialPos = random.Next(random.Next(0, Globals.WindowSize.X));
                initialPos = 0;
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -2))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -3))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, -2))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, -3))));
            }

            if (brickType == 'I')
            {
                //initialPos = random.Next(random.Next(0, Globals.WindowSize.X));
                initialPos = 0;
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, - (32 * 2)))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, - (32 * 3)))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, - (32 * 4)))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, - (32 * 5)))));
            }

            if (brickType == 'L')
            {
                //initialPos = random.Next(random.Next(0, Globals.WindowSize.X));
                initialPos = 0;
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -(32 * 2)))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -(32 * 3)))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -(32 * 4)))));
                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 32, -(32 * 4)))));
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
            //for (int playfieldX = 0; playfieldX < Size.X; playfieldX++)
            //{
            //    for (int playfieldY = 0; playfieldY < Size.Y; playfieldY++)
            //    {
            //        for (int brick = 0; brick < bricks.Count; brick++)
            //        {
            //            if (PlayField[playfieldX, playfieldY].Rectangle.X == bricks[brick].Rectangle.X
            //                && PlayField[playfieldX, playfieldY].Rectangle.Y == bricks[brick].Rectangle.Y)
            //            {
            //                PlayField[playfieldX, playfieldY].ocupied = true;
            //            }
            //        }
            //    }
            //}



            for (int i = 0; i < bricks.Count; i++)
            {
                PlayField[bricks[i].mapPos.x, bricks[i].mapPos.y].ocupied = true;
                Debug.WriteLine($"Brick {i} X: {bricks[i].mapPos.x}");
                Debug.WriteLine($"Brick {i} Y: {bricks[i].mapPos.y}");
                Debug.WriteLine("\n");
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
                        if (PlayField[_rightBound / 32 + 1, bricks[i].Rectangle.Y / 32].ocupied == true)
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
