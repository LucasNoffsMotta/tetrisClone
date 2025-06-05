using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Engine;

namespace Tetris.Miscellanea
{
    internal static class PlayerNameInputScreen
    {
        private readonly static SpriteFont font = Globals.Content.Load<SpriteFont>("NameInputFont");
        private static Texture2D nameInputScreen = Globals.Content.Load<Texture2D>("NameInputScreen");
        private static Texture2D enterButton = Globals.Content.Load<Texture2D>("NameInputScreenEnterButton");
        //Typing bar position: 191,395
        private static List<string> pressedKeys = new List<string>();
        private static KeybordInputParser keyParser = new KeybordInputParser();



        public static void Draw()
        {
            Globals.SpriteBatch.Draw(nameInputScreen, new Vector2(0, 0), Color.White);
            Globals.SpriteBatch.Draw(enterButton, new Vector2(0, 0), Color.White);
            pressedKeys = keyParser.GetInputList(pressedKeys);
          

            foreach(string input in pressedKeys)
            {
                Globals.SpriteBatch.DrawString(font, input, new Vector2(191, 395), Color.Yellow);
            }
        }
    }
}
