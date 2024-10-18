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
        private List<Brick> bricks;
        private List<Tetromines> brickObjects;
        private Random random;
        private Tetromines currentBrick;
        private int brickIndex;

        public BricksManager()
        {
            random = new Random();
            bricks = new List<Brick>();
            brickObjects = new List<Tetromines>();
            brickIndex = 0;
        }


        public void CreateBriks()

        {
            for (int i = 0; i < 1; i++)
            {
                brickObjects.Add(new());
            }

            for (int i = 0; i < brickObjects.Count; i++)
            {
                for (int j = 0; j < brickObjects[i].bricks.Count; j++)
                {
                    bricks.Add(brickObjects[i].bricks[j]);
                }
            }          
            currentBrick = brickObjects[brickIndex];
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

                catch ( System.ArgumentOutOfRangeException )
                {
                    CreateBriks();
                    currentBrick = brickObjects[brickIndex];
                }
            }
        }

        public void CleanLine(int line, Square[,] PlayField, Point Size)
        {
            for (int j = 0; j < Size.X; j++)
            {
                PlayField[j, line].ocupied = false;
            }

            for (int j = 0; j < bricks.Count; j++)
            {
                if (bricks[j].mapPos.y == line)
                {
                    bricks[j].Texture = Globals.Content.Load<Texture2D>("BackGroundTile");
                }
            }

            //for (int k = 0; k < bricks.Count; k++)
            //{
            //    if (bricks[k].mapPos.y < line)
            //    {
            //        bricks[k].Rectangle.Y += 32;
            //        break;
            //    }
            //}
        }


        public void CheckFullLines(Square[,] PlayField, Point Size)
        {
            int lineToBeCleaned;

            for (int i = 0; i < Size.Y; i++)
            {
                if (PlayField[0, i].ocupied && PlayField[1, i].ocupied && PlayField[2, i].ocupied &&
                    PlayField[3, i].ocupied && PlayField[4, i].ocupied && PlayField[5, i].ocupied &&
                    PlayField[6, i].ocupied && PlayField[7, i].ocupied && PlayField[8, i].ocupied &&
                    PlayField[9, i].ocupied)
                {
                    lineToBeCleaned = i; CleanLine(lineToBeCleaned, PlayField, Size); break;                      
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


        public void CheckCoilision(Square[,] PlayField)
        {
            for (int i = 0; i < brickObjects.Count; i++)
            {
                for (int j = 0; j < brickObjects[i].bricks.Count; j++)
                {
                    brickObjects[i].bricks[j].CheckRectColision(PlayField);
                }
            }
        }


        public void Update(Square[,] PlayField, Point Size)
        {
            ManageBricks();
            CheckCoilision(PlayField);
            CheckFullLines(PlayField, Size);    

            if (currentBrick.alive) { currentBrick.Update(PlayField, Size); }
        }
    }

}
