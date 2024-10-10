using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
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
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos, 1))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("yellowTile"), new Vector2(initialPos + 1, 1))));
            }

            if (typeOfObject == 'I')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 1, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 2, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 3, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("blueSquare"), new Vector2(initialPos + 4, 0))));
            }

            if (typeOfObject == 'L')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, 1))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos, 2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("orangeTile"), new Vector2(initialPos + 1, 0))));
            }

            if (typeOfObject == 'J')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, 1))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos, 2))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("pinkTile"), new Vector2(initialPos - 1, 0))));
            }

            if (typeOfObject == 'T')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos, 1))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos + 1, 1))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("darkBluTile"), new Vector2(initialPos - 1, 1))));
            }

            if (typeOfObject == 'Z')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos, 1))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos + 1, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("greenTile"), new Vector2(initialPos - 1, 1))));
            }

            if (typeOfObject == 'S')
            {
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, 1))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos - 1, 0))));
                emptyList.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 1, 1))));
            }
            return emptyList;
        }
        //public static void RotatePositions(char typeOfObject)
        //{
        //    if (typeOfObject == 'I')
        //    {
        //        rotations.Add(new())
        //    }
        //}
    }
}
