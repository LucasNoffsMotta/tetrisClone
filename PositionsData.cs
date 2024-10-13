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
        //public static Dictionary<string, List<Point>> rotationValues = new Dictionary<string, List<Point>>();
        

        public static List<Brick> ObjectsFactory(char typeOfObject, int initialPos, List<Brick> emptyList)
        {
            if (typeOfObject == 'O')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, -2))));
            }

            if (typeOfObject == 'I')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 2, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 3, -2))));
                
            }

            if (typeOfObject == 'L')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 2, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 2, -3))));
            }

            if (typeOfObject == 'J')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos + 2, -2))));
            }

            if (typeOfObject == 'T')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 2, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 2, -2))));
            }

            if (typeOfObject == 'Z')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 1,-3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 2, -3))));
            }

            if (typeOfObject == 'S')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, -3))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, -2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 2, -2))));
            }
            return emptyList;
        }

        public static List<Brick> ObjectRotate(List<Brick> bricks)
        {
            int x2;
            int y2;

            for (int i = 0; i < bricks.Count; i++)
            {
                x2 = (bricks[i].Rectangle.Y);
                y2 = (4 * 32 - (Globals.WindowSize.X - bricks[i].Rectangle.X));
                bricks[i].Rectangle.X = x2;
                bricks[i].Rectangle.Y = y2;
            }

            return bricks;

        }


        public static void RotatePositions(char typeOfObject, Point origin, int rotationStates)
        {
     
            if (typeOfObject == 'I')
            {

                if (rotationStates == 0)
                {
                    rotations.Add(new((origin.X) * 32, (origin.Y - 2) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y - 1) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y + 1) * 32));
                }

                if (rotationStates == 1)
                {
                    rotations.Add(new((origin.X - 2) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X - 1) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X + 1) * 32, (origin.Y) * 32));
                }

                if (rotationStates == 2)
                {
                    rotations.Add(new((origin.X) * 32, (origin.Y - 2) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y - 1) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y + 1) * 32));
                }

                if (rotationStates == 3)
                {
                    rotations.Add(new((origin.X - 2) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X - 1) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X) * 32, (origin.Y) * 32));
                    rotations.Add(new((origin.X + 1) * 32, (origin.Y) * 32));
                }


            }
        }
    }
}
