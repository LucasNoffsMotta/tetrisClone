using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Tetris
{
    public static class CreateObjects
    {
        public static List<Point> rotations = new List<Point>();

        public static List<Brick> ObjectsFactory(char typeOfObject, int initialPos, List<Brick> emptyList)
        {
            if (typeOfObject == 'O')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, -3))));

            }

            if (typeOfObject == 'I')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 2, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 3, -2))));

                emptyList[0].boxPosition.X = 0; emptyList[0].boxPosition.Y = 1;
                emptyList[1].boxPosition.X = 1; emptyList[1].boxPosition.Y = 1;
                emptyList[2].boxPosition.X = 2; emptyList[2].boxPosition.Y = 1;
                emptyList[3].boxPosition.X = 3; emptyList[3].boxPosition.Y = 1;
            }

            if (typeOfObject == 'L')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 2, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 2, -3))));

                emptyList[0].boxPosition.X = 0; emptyList[0].boxPosition.Y = 1;
                emptyList[1].boxPosition.X = 1; emptyList[1].boxPosition.Y = 1;
                emptyList[2].boxPosition.X = 2; emptyList[2].boxPosition.Y = 1;
                emptyList[3].boxPosition.X = 2; emptyList[3].boxPosition.Y = 0;
            }


            if (typeOfObject == 'J')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos + 2, -2))));

                emptyList[0].boxPosition.X = 0; emptyList[0].boxPosition.Y = 0;
                emptyList[1].boxPosition.X = 0; emptyList[1].boxPosition.Y = 1;
                emptyList[2].boxPosition.X = 1; emptyList[2].boxPosition.Y = 1;
                emptyList[3].boxPosition.X = 2; emptyList[3].boxPosition.Y = 1;
            }


            if (typeOfObject == 'T')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 1, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 2, -2))));

                emptyList[0].boxPosition.X = 0; emptyList[0].boxPosition.Y = 1;
                emptyList[1].boxPosition.X = 1; emptyList[1].boxPosition.Y = 1;
                emptyList[2].boxPosition.X = 1; emptyList[2].boxPosition.Y = 0;
                emptyList[3].boxPosition.X = 2; emptyList[3].boxPosition.Y = 1;
            }

            if (typeOfObject == 'Z')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 1, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 2, -3))));

                emptyList[0].boxPosition.X = 0; emptyList[0].boxPosition.Y = 1;
                emptyList[1].boxPosition.X = 1; emptyList[1].boxPosition.Y = 1;
                emptyList[2].boxPosition.X = 1; emptyList[2].boxPosition.Y = 0;
                emptyList[3].boxPosition.X = 2; emptyList[3].boxPosition.Y = 0;
            }

            if (typeOfObject == 'S')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 2, -2))));

                emptyList[0].boxPosition.X = 0; emptyList[0].boxPosition.Y = 0;
                emptyList[1].boxPosition.X = 1; emptyList[1].boxPosition.Y = 0;
                emptyList[2].boxPosition.X = 1; emptyList[2].boxPosition.Y = 1;
                emptyList[3].boxPosition.X = 2; emptyList[3].boxPosition.Y = 1;
            }
            return emptyList;
        }


        public static bool CheckRotationCoordinates(List<Brick> bricks, string orientation, bool canRotate, Point[,] boundBox, int boxSize, Square[,] PlayField)
        {
            int clockx2 = 0;
            int clocky2 = 0;
            int clocktestX = 0;
            int clocktestY = 0;

            for (int i = 0; i < bricks.Count; i++)
            {

                if (orientation == "clock")
                {
                    Debug.WriteLine("Testing clock");
                    clockx2 = ((boxSize - 1) - bricks[i].boxPosition.Y);
                    clocky2 = (bricks[i].boxPosition.X);
                }

                else
                {
                    Debug.WriteLine("Testing counter");
                    clockx2 = (bricks[i].boxPosition.Y);
                    clocky2 = ((boxSize - 1) - bricks[i].boxPosition.X);
                }
               

                clocktestX = boundBox[clockx2, clocky2].X;
                clocktestY = boundBox[clockx2, clocky2].Y;

                if (boundBox[0,0].X > 0 && boundBox[boxSize - 1, 0].X < Globals.WindowSize.X)
                {
                    if (PlayField[clocktestX, clocktestY].ocupied)
                    {
                        canRotate = false; break;
                    }
                }
            }
            return canRotate;
        }

        public static void ClockWiseRotate(List<Brick> bricks, Point[,] boundBox, int boxSize, Square[,] PlayField)
        {
            int x2;
            int y2;

            for (int i = 0; i < bricks.Count; i++)
            {
                x2 = ((boxSize - 1) - bricks[i].boxPosition.Y);
                y2 = (bricks[i].boxPosition.X);

                bricks[i].boxPosition.X = x2;
                bricks[i].boxPosition.Y = y2;
            }

            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Rectangle.X = boundBox[bricks[i].boxPosition.X, bricks[i].boxPosition.Y].X * 32;
                bricks[i].Rectangle.Y = boundBox[bricks[i].boxPosition.X, bricks[i].boxPosition.Y].Y * 32;
            }
        }


        public static void CounterWiseRotate(List<Brick> bricks, Point[,] boundBox, int boxSize, Square[,] PlayField)
        {
            int x2;
            int y2;
       
            for (int i = 0; i < bricks.Count; i++)
            {
                x2 = (bricks[i].boxPosition.Y);
                y2 = ((boxSize - 1) - bricks[i].boxPosition.X);

                bricks[i].boxPosition.X = x2;
                bricks[i].boxPosition.Y = y2;
            }

            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Rectangle.X = boundBox[bricks[i].boxPosition.X, bricks[i].boxPosition.Y].X * 32;
                bricks[i].Rectangle.Y = boundBox[bricks[i].boxPosition.X, bricks[i].boxPosition.Y].Y * 32;
            }
        }
    }
}



