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
        public float rotationTimer;
        public bool timerCounting;
        public float rotationTimerLimit;
        public Point[,] boundBox;
        public int boxSize;
        public List<Point> shapePosOnBox;
        public float fallSpeed;
        public float fallTrigger;
        public float fallCount;
        private float sideMoveTimer;
        private float sideMoveCount;
        private bool moveCounting;
        bool rotateClock = true;
        bool rotateCounter = true;

        public Tetromines()
        {
            random = new Random();
            bricks = new List<Brick>();
            shapePosOnBox = new List<Point>();
            initialPos = (int)random.Next(0, (Globals.WindowSize.X / 32) - 4);
            bricktype = brickTypes[random.Next(brickTypes.Length)];
            CreateBricks(bricktype);
            (_leftBound, _rightBound, _topBound) = GetBounds();
            alive = true;
            rotationTimerLimit = 2f;
            fallSpeed = 4f;
            fallTrigger = 0;
            fallCount = 0.1f;
            sideMoveTimer = 0.5f;
            sideMoveCount = 0;
            if (bricktype != 'O') { CreateBoundBox(); }          
        }

        public void CreateBoundBox()
        {
            
            int initialX = initialPos;
            int initialY = bricks[0].mapPos.y;

            if (bricktype != 'I') { boxSize = 3; }
            else  { boxSize = 4; }
            Debug.WriteLine(bricktype);
            Debug.WriteLine(boxSize);
            boundBox = new Point[boxSize, boxSize];

            for (int x = 0; x < boxSize; x++)
            {
                for (int y = 0; y < boxSize; y++)
                {                  
                    boundBox[x, y] = new(initialX, initialY);
                    initialY++;             
                }
                initialY -= boxSize;
                initialX++;                               
            }
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

        public void MoveDown()
        {
            fallTrigger += fallCount;

            if (fallTrigger >= fallSpeed)
            {
                for (int i = 0; i < bricks.Count; i++)
                {
                    bricks[i].Rectangle.Y += 32;                   
                }

                for (int i = 0; i < boxSize; i++)
                {
                    for (int j = 0; j < boxSize; j++)
                    {
                        boundBox[i, j].Y++;
                    }
                }
                fallTrigger = 0;
            }
        }

        public void Fall()
        {
            MoveDown();

            if (InputManager.KeybordPressed.IsKeyDown(Keys.S))
            {
                fallCount = 0.5f;
            }

            else if (InputManager.KeybordPressed.IsKeyUp(Keys.S))
            {
                fallCount = 0.1f;
            }
        }

        public void MoveTimerSides()
        {
            if (moveCounting) { sideMoveCount += fallCount; }
            if (sideMoveCount >= sideMoveTimer) { sideMoveCount = 0; moveCounting = false; }
        }

        public void MoveSides(Square[,] PlayField, bool canMoveLeft, bool canMoveRight)
        {
            if (InputManager.KeybordPressed.IsKeyDown(Keys.A) && !moveCounting && fallCount != 0.5f)
            {
                if (canMoveLeft)
                {
                    for (int i =0; i < bricks.Count; i++)
                    {
                        bricks[i].Rectangle.X -= 32;                        
                    }

                    for (int i = 0; i < boxSize; i++)
                    {
                        for (int j = 0; j < boxSize; j++)
                        {
                            boundBox[i, j].X--;                        
                        }
                    }
                    moveCounting = true;
                }
            }

            else if (InputManager.KeybordPressed.IsKeyDown(Keys.D) && !moveCounting && fallCount != 0.5f)
            {
                if (canMoveRight)
                {
                    for (int i = 0; i < bricks.Count; i++)
                    {
                        bricks[i].Rectangle.X += 32;                       
                    }

                    for (int i = 0; i < boxSize; i++)
                    {
                        for (int j = 0; j < boxSize; j++)
                        {
                            boundBox[i, j].X++;
                        }
                    }

                    moveCounting = true;
                }
            }
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
                }

                for (int i = 0; i < boxSize;i++)
                {
                    for( int k = 0; k < boxSize;k++)
                    {
                        boundBox[i, k].Y += 3;
                    }
                }
                canMoveDown = false;
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

        public void Rotate(Square[,] PlayField)
        {
            if (bricktype != 'O')
            {
                CheckRotationCondition(PlayField);

                if (boundBox[boxSize - 1, 0].X < Globals.WindowSize.X / 32 && (boundBox[0, 0].X >= 0))
                {
                    if (InputManager.KeybordPressed.IsKeyDown(Keys.C) && !timerCounting && rotateClock)
                    {                      
                        CreateObjects.ClockWiseRotate(bricks, boundBox, boxSize, PlayField);
                        timerCounting = true;
                    }
                
                    if (InputManager.KeybordPressed.IsKeyDown(Keys.Z) && !timerCounting && rotateCounter)
                    {                     
                        CreateObjects.CounterWiseRotate(bricks, boundBox, boxSize, PlayField);
                        timerCounting = true;
                    }
                }
            }               
        }

        public void CheckRotationCondition(Square[,] PlayField)
        {
            if (bricktype != 'O')
            {
                rotateClock = CreateObjects.CheckRotationCoordinates(bricks, "clock", rotateClock, boundBox, boxSize, PlayField);
                rotateCounter = CreateObjects.CheckRotationCoordinates(bricks, "counter", rotateClock, boundBox, boxSize, PlayField);
            }
        }

        public void Update(Square[,] PlayField, Point Size)
        {
            RotateTimerEngine();
            (_leftBound, _rightBound, _topBound) = GetBounds();
            Respawn(PlayField);
            CheckIfCanMove(PlayField);
            Fall();
            MoveTimerSides();
            MoveSides(PlayField, canMoveLeft, canMoveRight);
            Rotate(PlayField);
            Debug.WriteLine(rotateClock);
            Debug.WriteLine(rotateCounter);
            rotateClock = true;
            rotateCounter = true;


            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Update(PlayField, canMoveLeft, canMoveRight);
            }


            CheckFallColision(PlayField, Size);
          
        }
    }
}
