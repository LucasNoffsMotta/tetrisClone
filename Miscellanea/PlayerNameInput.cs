using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.Engine;
using Tetris.ExternalAPI.Models;

namespace Tetris.Miscellanea
{
    //REFATORAR AQUI
    internal static class PlayerNameInputScreen
    {
        private readonly static SpriteFont font = Globals.Content.Load<SpriteFont>("NameInputFont");
        private static Texture2D nameInputScreen = Globals.Content.Load<Texture2D>("NameInputScreen");
        private static Texture2D enterButton = Globals.Content.Load<Texture2D>("NameInputScreenEnterButton");
        private static List<string> pressedKeys = new List<string>();
        private static KeybordInputParser keyParser = new KeybordInputParser();
        private static Vector2 initialNamePos = new Vector2(191, 390);
        private const int SPACE_BETWEEN_LETTERS = 15;
        private static Texture2D typingLine = DrawShapes.CreateVerticalLine(new Vector2(190,390), new Vector2(190, 408), 1, Color.Yellow);
        private static float typingLineBlinkCounter = 0f;
        private static float blinkColorMultiplier = 0f;
        public static string playername;

        private static Vector2 NewLetterPos(int index)
        {
            return new Vector2(initialNamePos.X + index * SPACE_BETWEEN_LETTERS, initialNamePos.Y);
        }

        private static void BlinkDrawLine()
        {
            typingLineBlinkCounter += 0.1f;

            if (typingLineBlinkCounter >= 3f)
            {
                blinkColorMultiplier = blinkColorMultiplier == 1 ? 0 : 1;
                typingLineBlinkCounter = 0f;
            }
        }

        public static void GetPlayerName()
        {
            playername = String.Concat(pressedKeys);           
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

            Globals.SpriteBatch.Draw(typingLine, NewLetterPos(pressedKeys.Count), Color.White*blinkColorMultiplier);
            BlinkDrawLine();      
        }
    }
}
