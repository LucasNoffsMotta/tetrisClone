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
        private static List<string> input = new List<string>();
        private static string[] regularKeys =
            Enumerable.Range('0', 10).Select(c => ((char)c).ToString())
            .Concat(Enumerable.Range('A', 26).Select(c => ((char)c).ToString()))
            .Concat(Enumerable.Range('a', 26).Select(c => ((char)c).ToString()))
            .ToArray();

        private void ParseInputedString(List<string> input, Keys tempKey)
        {
            if (regularKeys.Contains<string>(tempKey.ToString()))
            {
                GetInputIntoList(tempKey);
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
                    input.RemoveAt(input.Count - 1);
                    break;
            }
        }

        private void GetInputIntoList(Keys tempKey)
        {
            string tempKeyString = tempKey.ToString();
            input.Add(InputManager.KeybordPressed.CapsLock ? tempKeyString.ToLower() : tempKeyString.ToUpper());
        }

        public List<string> GetInputList(List<string> input)
        {
            Keys[] keys = InputManager.KeybordPressed.GetPressedKeys();
            if (keys.Length != 0) { ParseInputedString(input, keys[0]); }      
            return input;
        }
    }
}
