using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;



namespace Tetris
{
    public class Tetromines
    {
        public List<Brick> bricks;
        private Random random;
        private int initialPos;
        private char bricktype;
        public bool alive, canMoveLeft, canMoveRight, canRespawn, canMoveDown, timerCounting;
        public int _leftBound, _rightBound, _bottomBound, boxSize, dropScore;
        public float rotationTimer, rotationTimerLimit, fallSpeed, fallTrigger, fallCount, sideMoveTimer, sideMoveCount;
        public Point[,] boundBox;
        public List<Point> shapePosOnBox;
        private bool moveCounting, softDropScoring;
        bool rotateClock = true, rotateCounter = true;

        public Tetromines(char _bricktype, bool display = false)

        {
            bricktype = _bricktype;
            random = new Random();
            bricks = new List<Brick>();
            shapePosOnBox = new List<Point>();

            if (!display)
            {
                initialPos = (int)random.Next(0, (Globals.PlayFieldSize.X / 32) - 4);
                CreateBricks(bricktype);
                (_leftBound, _rightBound) = GetBounds();
                alive = true;
                rotationTimerLimit = 2f;
                fallSpeed = 4f;
                fallTrigger = 0;
                fallCount = 0.1f;
                sideMoveTimer = 0.5f;
                sideMoveCount = 0;
                softDropScoring = false;
                dropScore = 0;
                canMoveDown = true;
                if (bricktype != 'O') { CreateBoundBox(); }
            }

            else
            {
                initialPos = 12;
                initialPos = 12;
                CreateBricks(bricktype);

                for (int i = 0; i < bricks.Count; i++)
                {
                    bricks[i].Rectangle.Y += 202;
                }
            }
        }

        public void CreateBoundBox()
        {
            int initialX = initialPos;
            int initialY = bricks[0].mapPos.y;

            if (bricktype != 'I') { boxSize = 3; }
            else { boxSize = 4; }
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

        public (int _leftBound, int _rightBound) GetBounds()
        {
            _leftBound = bricks[0].mapPos.x;
            _rightBound = bricks[0].mapPos.x + 1;

            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].mapPos.x < _leftBound) { _leftBound = bricks[i].mapPos.x; }
                if (bricks[i].mapPos.x + 1 > _rightBound) { _rightBound = bricks[i].mapPos.x + 1; }
            }

            return (_leftBound, _rightBound);
        }

        public void DropScore()
        {
            if (!softDropScoring)
            {
                dropScore = 0;
            }
        }

        public void CheckIfCanMoveDown()
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].canMoveDownFloor == false || bricks[i].canMoveDownRect == false) { canMoveDown = false; break; }
                else if (bricks[i].canMoveDownFloor && bricks[i].canMoveDownRect == true) { canMoveDown = true; }
            }
        }


        public void MoveDown()
        {
            fallTrigger += fallCount;

            if (fallTrigger >= fallSpeed && canMoveDown)
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
                if (softDropScoring) { dropScore += 1; }
                fallTrigger = 0;
            }
        }

        public void Fall(Square[,] PlayField, Point Size)
        {
            CheckFallColision(PlayField, Size);
            MoveDown();
            DropScore();

            if (InputManager.KeybordPressed.IsKeyDown(Keys.Down))
            {
                fallCount = 0.5f;
                softDropScoring = true;
            }

            else if (InputManager.KeybordPressed.IsKeyUp(Keys.Down))
            {
                fallCount = 0.1f;
                softDropScoring = false;
            }
        }

        public void MoveTimerSides()
        {
            if (moveCounting) { sideMoveCount += fallCount; }
            if (sideMoveCount >= sideMoveTimer) { sideMoveCount = 0; moveCounting = false; }
        }

        public void MoveSides(Square[,] PlayField, Point Size)
        {
            
            if (InputManager.KeybordPressed.IsKeyDown(Keys.Left) && !moveCounting && fallCount != 0.5f)
            {
                CheckIfCanMove(PlayField);
                if (canMoveLeft)
                {
                    for (int i = 0; i < bricks.Count; i++)
                    {
                        bricks[i].Rectangle.X -= 32;
                        bricks[i].UpdateMapPos();
                    }

                    for (int i = 0; i < boxSize; i++)
                    {
                        for (int j = 0; j < boxSize; j++)
                        {
                            boundBox[i, j].X--;
                        }
                    }
                    moveCounting = true;
                    Debug.WriteLine("MOVED LEFT");
                }
            }

            else if (InputManager.KeybordPressed.IsKeyDown(Keys.Right) && !moveCounting && fallCount != 0.5f)
            {
                CheckIfCanMove(PlayField);
                if (canMoveRight)
                {
                    for (int i = 0; i < bricks.Count; i++)
                    {
                        bricks[i].Rectangle.X += 32;
                        bricks[i].UpdateMapPos();
                    }

                    for (int i = 0; i < boxSize; i++)
                    {
                        for (int j = 0; j < boxSize; j++)
                        {
                            boundBox[i, j].X++;
                        }
                    }
                    Debug.WriteLine("MOVED RIGHT");
                    moveCounting = true;
                }
            }
            (_leftBound, _rightBound) = GetBounds();
        }

        public void SumDropScore()
        {
            if (!alive) { Globals.Score += dropScore; }
        }

        public void KillPieces()
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].alive = false;
            }
        }

        public void CheckFallColision(Square[,] PlayField, Point Size)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].coliding)
                {
                    KillPieces();
                    UpdateOcupiedFields(PlayField, Size);
                    alive = false;
                }
            }
            SumDropScore();
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
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].mapPos.y < 0 && PlayField[bricks[i].mapPos.x, 1].ocupied == false)
                {
                    canRespawn = true;
                }

                else if (bricks[i].mapPos.y < 0 && PlayField[bricks[i].mapPos.x, 1].ocupied == true)
                {
                    canRespawn = false;
                    Globals.GameOver = true;
                    break;
                }
            }

            if (canRespawn)
            {
                for (int i = 0; i < bricks.Count; i++)
                {
                    bricks[i].Rectangle.Y += 32;
                }

                for (int i = 0; i < boxSize; i++)
                {
                    for (int k = 0; k < boxSize; k++)
                    {
                        boundBox[i, k].Y += 1;
                    }
                }
                canRespawn = false;
            }
        }

        public void CheckIfCanMove(Square[,] PlayField)
        {
            Debug.WriteLine($"PLayField analysed Position X left : {_leftBound - 1}");
            Debug.WriteLine($"PLayField analysed Position X right: {_rightBound}");


            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].mapPos.y > Globals.PlayFieldStartPos.Y / 32)
                {
                    if (_leftBound > 0)
                    {
                        if (PlayField[_leftBound - 1, bricks[i].mapPos.y].ocupied == true && _leftBound == bricks[i].mapPos.x)
                        {
                            canMoveLeft = false;
                            Debug.WriteLine("Cant move left");
                            break;
                        }

                        else if (PlayField[_leftBound - 1, bricks[i].mapPos.y].ocupied == false && _leftBound == bricks[i].mapPos.x)
                        {
                            canMoveLeft = true;
                        }
                    }

                    if (_rightBound <= 10)
                    {
                        if (PlayField[_rightBound, bricks[i].mapPos.y].ocupied == true && _rightBound == bricks[i].mapPos.x + 1)
                        {
                            canMoveRight = false;
                            Debug.WriteLine("Cant move right");
                            break;
                        }

                        else if (PlayField[_rightBound, bricks[i].mapPos.y].ocupied == false && _rightBound == bricks[i].mapPos.x + 1)
                        {
                            canMoveRight = true;
                        }
                    }
             

                    if (_leftBound <= 0) { canMoveLeft = false; }
                    if (_rightBound >= 10) { canMoveRight = false; }
                }
            }
        }

        public void RotateTimerEngine()
        {
            if (timerCounting) { rotationTimer += 0.1f; }
            if (rotationTimer >= rotationTimerLimit) { timerCounting = false; rotationTimer = 0; }
        }



        public void Rotate(Square[,] PlayField, Point Size)
        {
            if (bricktype != 'O')
            {
                  
                if (boundBox[boxSize - 1, 0].X < 10 && (boundBox[0, 0].X >= 0) && boundBox[0, boxSize - 1].Y < 20 && boundBox[0,0].Y >= 0)
                {
                    CheckRotationCondition(PlayField);

                    if (InputManager.KeybordPressed.IsKeyDown(Keys.C) && !timerCounting && rotateClock)
                    {
                        {
                            CreateObjects.ClockWiseRotate(bricks, boundBox, boxSize, PlayField);
                            timerCounting = true;
                        }                                         
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

            rotateClock = CreateObjects.CheckRotationCoordinates(bricks, "clock", boundBox, boxSize, PlayField);
            rotateCounter = CreateObjects.CheckRotationCoordinates(bricks, "counter", boundBox, boxSize, PlayField);
        }


        public void Update(Square[,] PlayField, Point Size)
        {
            Respawn(PlayField);
            RotateTimerEngine();
            Rotate(PlayField, Size);
            MoveTimerSides();
            MoveSides(PlayField, Size);
            //(_leftBound, _rightBound) = GetBounds();
            CheckIfCanMoveDown();
            Fall(PlayField, Size);


            //CHECK FUNCTIONS

            Debug.WriteLine($"Leftbound pos: {_leftBound}, Rightbound pos: {_rightBound}");
            Debug.WriteLine($"Original rightbound: {_rightBound}");

            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Update(PlayField, canMoveLeft, canMoveRight, fallTrigger, fallSpeed);
            }

            CheckFallColision(PlayField, Size);


        }
    }
}