//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Tetris
//{
//    public class BrickConjunt
//    {
//        public List<Brick> bricks;
//        private Random random;
//        private int initialPos;
//        private char bricktype;
//        private char[] brickTypes = new char[] { 'O', 'I', 'S', 'Z', 'L', 'J', 'T' };
//        public bool isAlive;


//        public BrickConjunt()
//        {
//            random = new Random();
//            bricks = new List<Brick>();
//            //CreateBricks(brickTypes[random.Next(brickTypes.Length)]);
//            CreateBricks(brickTypes['O']);
//        }
       

//        public void CreateBricks(char brickType)
//        {
//            if (brickType == 'O')
//            {
//                initialPos = random.Next(random.Next(0, Globals.WindowSize.X));
//                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -64))));
//                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos, -96))));
//                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 32, -64))));
//                bricks.Add((new(Globals.Content.Load<Texture2D>("redSquare"), new Vector2(initialPos + 32, -96))));
//            }
//        }

//        public void MoveBricks(Square[,] PlayField)
//        {
//            if (InputManager.KeybordPressed.IsKeyDown(Keys.A) && !moveCounting)
//            {
//                if (Rectangle.X > 0 && Rectangle.Y > 0)
//                {
//                    if (PlayField[Rectangle.X / 32 - 1, Rectangle.Y / 32].ocupied == false)
//                    {
//                        Rectangle.X -= 32;
//                        moveCounting = true;
//                    }
//                }
//            }

//            else if (InputManager.KeybordPressed.IsKeyDown(Keys.D) && !moveCounting)
//            {
//                if (Rectangle.Right < Globals.WindowSize.X && Rectangle.Y > 0)
//                {
//                    if (PlayField[Rectangle.X / 32 + 1, Rectangle.Y / 32].ocupied == false)
//                    {
//                        Rectangle.X += 32;
//                        moveCounting = true;
//                    }
//                }
//            }
//        }









//    }
//}
