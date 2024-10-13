using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Tetromines
    {
        public List<Brick> bricks;
        private Random random;
        private int initialPos;
        private char bricktype;
        private char[] brickTypes = new char[] { 'O', 'I', 'S', 'Z', 'L', 'J', 'T' };
        public bool alive;
        public int _leftBound;
        public int _rightBound;
        public int _bottomBound;
        public int _topBound;
        public bool canMoveLeft;
        public bool canMoveRight;
        public bool canMoveDown;
        public Point Origin;
        public int rotationStates;
        public float rotationTimer;
        public bool timerCounting;
        public float rotationTimerLimit;
        public List<Point> boundBox;


        public Tetromines()
        {
            random = new Random();
            bricks = new List<Brick>();
            initialPos = random.Next(1, (Globals.WindowSize.X/32)  - 4);
            CreateBricks(brickTypes[random.Next(brickTypes.Length)]);
            //bricktype = brickTypes[1];
            //CreateBricks(brickTypes[1]);
            Origin = new(bricks[2].mapPos.x, bricks[2].mapPos.y);
            (_leftBound, _rightBound, _topBound) = GetBounds();
            alive = true;
            rotationStates = 0;
            rotationTimerLimit = 2f;
            boundBox = new List<Point>();
            Debug.WriteLine($"{bricks[0].mapPos.x} / {bricks[0].mapPos.y}");
            Debug.WriteLine($"{bricks[1].mapPos.x} / {bricks[1].mapPos.y}");
            Debug.WriteLine($"{bricks[2].mapPos.x} / {bricks[2].mapPos.y}");
            Debug.WriteLine($"{bricks[3].mapPos.x} / {bricks[3].mapPos.y}");
        }

 
        public void CreateBricks(char brickType)
        {
            bricks = CreateObjects.ObjectsFactory(brickType, initialPos, bricks);             
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


        public void Respawn(Square[,] PlayField)
        {
            for (int i = 0; i < bricks.Count;i++)
            {
                if (bricks[i].mapPos.y < 0 && PlayField[bricks[i].mapPos.x, 3].ocupied == false)
                {
                    canMoveDown = true;
                }

                else if (bricks[i].mapPos.y < 0 && PlayField[bricks[i].mapPos.x, 3].ocupied == true)
                {
                    canMoveDown = false;
                    break;
                }
            }


            if (canMoveDown)
            {
                for (int i = 0; i < bricks.Count;i++)
                {
                    bricks[i].Rectangle.Y += 3 * 32;
                    canMoveDown = false;
                }
            }         
        }



        public void CheckIfCanMove(Square[,] PlayField)
        { 
            for (int i = 0; i < bricks.Count; i++)
            {
                
                if (bricks[i].mapPos.y > 0)
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

        public void RotateTimerEngine()
        {
            if (timerCounting) { rotationTimer += 0.1f; }
            if (rotationTimer >= rotationTimerLimit) { timerCounting = false; rotationTimer = 0; }
        }


        public void Rotate()
        {
            if (InputManager.KeybordPressed.IsKeyDown(Keys.Z) && !timerCounting)
            {
                timerCounting = true;
                bricks = CreateObjects.ObjectRotate(bricks);
                //CreateObjects.RotatePositions(bricktype, Origin, rotationStates);
                //bricks[0].Rectangle.X = CreateObjects.rotations[0].X; bricks[0].Rectangle.Y = CreateObjects.rotations[0].Y;
                //bricks[1].Rectangle.X = CreateObjects.rotations[1].X; bricks[1].Rectangle.Y = CreateObjects.rotations[1].Y;
                //bricks[2].Rectangle.X = CreateObjects.rotations[2].X; bricks[2].Rectangle.Y = CreateObjects.rotations[2].Y;
                //bricks[3].Rectangle.X = CreateObjects.rotations[3].X; bricks[3].Rectangle.Y = CreateObjects.rotations[3].Y;
                //CreateObjects.rotations.Clear();
          
                rotationStates++;
                if (rotationStates > 3) { rotationStates = 0; }
            }           
        }


        public void Update(Square[,] PlayField, Point Size)
        {
            RotateTimerEngine();
            (_leftBound, _rightBound, _topBound) = GetBounds();
            Origin = new (bricks[2].mapPos.x, bricks[2].mapPos.y);
            Respawn(PlayField);
            CheckIfCanMove(PlayField);
            Rotate();
            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Update(PlayField, canMoveLeft, canMoveRight);
            }
            CheckFallColision(PlayField, Size);
        }
    }
}
