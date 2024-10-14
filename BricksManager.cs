using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            for (int i = 0; i < 1d; i++)
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

            //{ 
            //    if (brickIndex != bricks.Count - 1) { brickIndex += 1; }
            //    else
            //    {
            //        CreateBriks();
            //    }
            ////    currentBrick = brickObjects[brickIndex]; ;
            //}
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

            if (currentBrick.alive) { currentBrick.Update(PlayField, Size); }
        }
    }

}
