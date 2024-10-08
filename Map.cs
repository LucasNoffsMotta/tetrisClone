using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;



namespace Tetris
{
    public class Map
    {
        public readonly Point Size = new Point(20, 20);
        public Square[,] PlayField;
        public Point MapSize { get; private set; }
        public Point SquareSize { get; private set; }
        public Vector2 MapToScreen(int x, int y) => new(x * SquareSize.X, y * SquareSize.Y);
        public (int x, int y) ScreenToMap(Vector2 pos) => ((int)pos.X / SquareSize.X, (int)pos.Y / SquareSize.Y);

        public Map()
        {
            SquareSize = new(32, 32);
            PlayField = new Square[Size.X, Size.Y];
            MapSize = new(Size.X * SquareSize.X, Size.Y * SquareSize.Y);

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    PlayField[x, y] = new(Globals.Content.Load<Texture2D>("BackGroundTile"), MapToScreen(x, y));
                }
            }
        }

        public void Draw()
        {
            for (int x = 0; x < Size.X; x++)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    PlayField[x, y].Draw();
                }
            }
        }
    }
}
