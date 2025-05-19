using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris
{
    public class BricksManager
    {
        public List<Brick> bricks;
        public List<Tetromines> brickObjects;
        public Tetromines nextTetromineDisplay;
        private Random random;
        private Tetromines currentBrick;
        private int brickIndex;
        //private char[] brickTypes = new char[] { 'I' };
        private char[] brickTypes = new char[] { 'I', 'L', 'J', 'T', 'O', 'S', 'Z' };
        private char bricktype;
        private char nextBrick;

        public BricksManager()
        {
            random = new Random();
            bricks = new List<Brick>();
            brickObjects = new List<Tetromines>();
            StartBricks();
        }


        public void StartBricks()
        {
            brickIndex = 0;
            bricktype = brickTypes[random.Next(brickTypes.Length)];
            nextBrick = brickTypes[random.Next(brickTypes.Length)];
            nextTetromineDisplay = new(nextBrick, true);
            CreateBriks();
        }


        public void CreateBriks()

        {
            bricktype = nextBrick;

            for (int i = 0; i < 1; i++)
            {
                brickObjects.Add(new(bricktype));
            }

            currentBrick = brickObjects[brickIndex];
            nextBrick = brickTypes[random.Next(brickTypes.Length)];
            nextTetromineDisplay = new(nextBrick, true);


            for (int i = 0; i < brickObjects[brickIndex].bricks.Count; i++)
            {
                bricks.Add(brickObjects[brickIndex].bricks[i]);
            }
        }


        public void ManageBricks()
        {
            if (!currentBrick.alive)
            {
                try
                {
                    brickIndex += 1;
                    currentBrick = brickObjects[brickIndex];
                }

                catch (System.ArgumentOutOfRangeException)
                {
                    CreateBriks();
                    currentBrick = brickObjects[brickIndex];
                }
            }
        }


        public void UpdateEmptyLineField(int line, Square[,] PlayField, Point Size)
        {
            for (int j = 0; j < Size.X; j++)
            {
                PlayField[j, line].ocupied = false;
            }
        }


        public void RemoveCompleteLine(int line, Square[,] PlayField)
        {
            for (int j = 0; j < bricks.Count; j++)
            {
                if (bricks[j].mapPos.y == line)
                {
                    bricks[j].Rectangle.Y = (line + 10) * 32;
                    bricks[j].UpdateMapPos();
                }
            }
        }

        public void MoveBricksDown(int line, Square[,] PlayField)
        {
            for (int j = 0; j < bricks.Count; j++)
            {
                if (bricks[j].mapPos.y < line && bricks[j].alive == false && !bricks[j].descended)
                {
                    PlayField[bricks[j].mapPos.x, bricks[j].mapPos.y].ocupied = false;
                    bricks[j].Rectangle.Y += 32;
                    bricks[j].UpdateMapPos();
                    bricks[j].descended = true;
                }
            }
        }

        public void ResetDescendCondition()
        {
            for (int j = 0; j < bricks.Count; j++)
            {
                if (bricks[j].descended)
                {
                    bricks[j].descended = false;
                }
            }
        }


        public void UpdateOcupiedFields(Square[,] PlayField)
        {
            for (int j = 0; j < bricks.Count; j++)
            {
                if (!bricks[j].alive && bricks[j].mapPos.y < 20)
                {
                    PlayField[bricks[j].mapPos.x, bricks[j].mapPos.y].ocupied = true;
                }
            }
        }

        public void CleanLine(int line, Square[,] PlayField, Point Size)
        {
            UpdateEmptyLineField(line, PlayField, Size);
            RemoveCompleteLine(line, PlayField);
            MoveBricksDown(line, PlayField);
            UpdateOcupiedFields(PlayField);
        }

        public void AddScore()
        {
            int addScore = 40 * (Globals.Level + 1);
            Globals.Score += addScore;
            Globals.LinesCleaned += 1;
            LevelManager.UpdateLevel(currentBrick);
        }


        public void CheckFullLines(Square[,] PlayField, Point Size)
        {
            int lineToBeCleaned;

            for (int i = 0; i < Globals.PlayFieldSize.Y / 32; i++)
            {
                if (PlayField[0, i].ocupied && PlayField[1, i].ocupied && PlayField[2, i].ocupied &&
                    PlayField[3, i].ocupied && PlayField[4, i].ocupied && PlayField[5, i].ocupied &&
                    PlayField[6, i].ocupied && PlayField[7, i].ocupied && PlayField[8, i].ocupied &&
                    PlayField[9, i].ocupied)

                {
                    lineToBeCleaned = i; AddScore(); CleanLine(lineToBeCleaned, PlayField, Size); ResetDescendCondition(); break;
                }

            }
        }


        public void Draw()
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Draw();
            }
        }


        public void DrawDisplayTetromine()
        {
            for (int i = 0; i < nextTetromineDisplay.bricks.Count; i++)
            {
                Globals.SpriteBatch.Draw(nextTetromineDisplay.bricks[i].Texture, nextTetromineDisplay.bricks[i].Rectangle, Color.White);
            }
        }

        public void Update(Square[,] PlayField, Point Size)
        {
            ManageBricks();
            CheckFullLines(PlayField, Size);


            if (currentBrick.alive) { currentBrick.Update(PlayField, Size); }
        }
    }
}
