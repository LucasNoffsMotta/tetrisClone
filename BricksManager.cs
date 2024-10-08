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
        private Random random;
        private Brick currentBrick;
        private int brickIndex;

        public BricksManager()
        {
            random = new Random();
            bricks = new List<Brick>();
            brickIndex = 0;
        }

        public void CreateBriks()

        {
            for (int i =0; i < 5; i++)
            {
                bricks.Add(new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(random.Next(0, Globals.WindowSize.X), -64)));
            }
            currentBrick = bricks[brickIndex];
        }


        public void ManageBricks()
        {
            if (!currentBrick.alive)
            { 
                if (brickIndex != bricks.Count - 1) { brickIndex += 1; }
                else
                {
                    CreateBriks();
                }
                currentBrick = bricks[brickIndex];
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
            for (int i = 0; i < bricks.Count; i++)
            {
                if (i != brickIndex) { currentBrick.CheckRectColision(bricks[i].Rectangle, PlayField); }           
            }
        }

        public void Update(Square[,] PlayField, Point Size)
        {
            ManageBricks();
            CheckCoilision(PlayField);
 
            if (currentBrick.alive) { currentBrick.Update(PlayField); }

            Debug.WriteLine($"First rect - X = {bricks[0].Rectangle.X.ToString()} // Y = {bricks[0].Rectangle.Y.ToString()} - Alive = {bricks[0].alive}");
            Debug.WriteLine($"Second rect - X = {bricks[1].Rectangle.X.ToString()} // Y = {bricks[1].Rectangle.Y.ToString()} - Alive = {bricks[1].alive}");
            Debug.WriteLine($"Third rect - X = {bricks[2].Rectangle.X.ToString()} // Y = {bricks[2].Rectangle.Y.ToString()} - Alive = {bricks[2].alive}");
            Debug.WriteLine("\n");

        }
    }

}
