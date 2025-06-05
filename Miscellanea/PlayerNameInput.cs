using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static List<string> pressedKeys = new List<string>();
        private static KeybordInputParser keyParser = new KeybordInputParser();
        private static Vector2 initialNamePos = new Vector2(191, 390);
        private const int SPACE_BETWEEN_LETTERS = 15;


        private static Vector2 NewLetterPos(int index)
        {
            return new Vector2(initialNamePos.X + index * SPACE_BETWEEN_LETTERS, initialNamePos.Y);
        }

        public static void Draw()
        {
            Globals.SpriteBatch.Draw(nameInputScreen, new Vector2(0, 0), Color.White);
            Globals.SpriteBatch.Draw(enterButton, new Vector2(0, 0), Color.White);
            pressedKeys = keyParser.GetInputList(pressedKeys);

            for(int i = 0; i < pressedKeys.Count; i++)
            {
                Globals.SpriteBatch.DrawString(font, pressedKeys[i], NewLetterPos(i), Color.Yellow);
            }      
        }
    }
}
