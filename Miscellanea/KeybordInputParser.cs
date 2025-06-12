using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Tetris.Engine;

namespace Tetris.Miscellanea
{
    internal class KeybordInputParser
    {
        private static string[] regularKeys =
            Enumerable.Range('0', 10).Select(c => ((char)c).ToString())
            .Concat(Enumerable.Range('A', 26).Select(c => ((char)c).ToString()))
            .Concat(Enumerable.Range('a', 26).Select(c => ((char)c).ToString()))
            .ToArray();

        private const float pressingInterval = 4f;
        private float timeCountSinceLastPressed = 0;
        private Keys lastPressed = new Keys();

        private void ParseInputedString(List<string> input, Keys? tempKey)
        {
            if (regularKeys.Contains<string>(tempKey.ToString()))
            {
                GetInputIntoList(input, tempKey);
            }

            else
            {
                ApplySpecialInput(input, tempKey.ToString());
            }
        }

        private void ApplySpecialInput(List<string> input, string key)
        {
            switch (key)
            {
                case "Space":
                    input.Add(" ");
                    break;
                case "Back":
                    if (input.Count > 0) { input.RemoveAt(input.Count - 1); };
                    break;
            }
        }

        private void GetInputIntoList(List<string> input, Keys? tempKey)
        {
            string tempKeyString = tempKey.ToString();
            input.Add(InputManager.KeybordPressed.CapsLock ? tempKeyString.ToUpper() : tempKeyString.ToLower());
        }

        //Interval only exists if the same key is being pressed
        //TODO: Improve this method
        private Keys? GetInputWithInterval()
        {
            Keys[] currentKeys = InputManager.KeybordPressed.GetPressedKeys(); 
            
            if (currentKeys.Length > 0)
            {
                Keys beingPressed = currentKeys[0];

                if (lastPressed != null && lastPressed == beingPressed)
                {
                    timeCountSinceLastPressed += 0.5f;

                    if (timeCountSinceLastPressed >= pressingInterval)
                    {
                        timeCountSinceLastPressed = 0;
                        return lastPressed;
                    }
                }

                else
                {
                    lastPressed = beingPressed;
                }
            }
            return null;
        }

        public List<string> GetInputList(List<string> input)
        {
            Keys? key = GetInputWithInterval();
            ParseInputedString(input, key);     
            return input;
        }
    }
}
