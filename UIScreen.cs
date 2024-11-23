using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class UIScreens
    {
        public static Texture2D startScreen;
        public static Texture2D menuUI;
        public static Texture2D levelChoiceUI;
        public static Dictionary<string, bool> GameStates = new Dictionary<string, bool>();
        public static Dictionary<string, Vector2> MainMenuButtons = new Dictionary<string, Vector2>();
        public static Dictionary<string, Vector2> LevelMenuButtons = new Dictionary<string, Vector2>();


        public static void CreateGameStates()
        {
            GameStates["StartScreen"] = true;
            GameStates["MenuScreen"] = false;
            GameStates["LevelSelectionScreen"] = false;
            GameStates["StartGame"] = false;
        }

        public static void CreateMenuMainMenuButtons()
        {
            MainMenuButtons["StartGame"] = new(200, 200);
            MainMenuButtons["MusicVolum1"] = new(200, 300);
            MainMenuButtons["MusicVolum2"] = new(200, 400);
            MainMenuButtons["MusicVolum3"] = new(200, 500);
            MainMenuButtons["MusicVolumOff"] = new(200, 600);
        }

        public static void CreateLevelMenuButtons()
        {
            Vector2 firstpos = new Vector2(200,200);

            for (int i = 0; i < 10; i++)
            {
                LevelMenuButtons[i.ToString()] = new(firstpos.X, firstpos.Y);
                firstpos.X += 20;
            }
        }


        public static void Draw()
        {
            if (GameStates["StartScreen"])
            {
                Globals.SpriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
            }

            else if (GameStates["MenuScreen"])
            {
                Globals.SpriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
            }

            else if (GameStates["LevelSelectionScreen"])
            {
                Globals.SpriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
            }
        }

        public static void Update()
        {
            if (GameStates["StartScreen"])
            {
                if (InputManager.KeybordPressed.IsKeyDown(Keys.Enter))
                {
                    GameStates["StartScreen"] = false;
                    GameStates["MenuScreen"] = true;
                }
            }

            else if (GameStates["MenuScreen"])
            {
                string clicked = "";
                foreach (KeyValuePair<string,Vector2> button in MainMenuButtons)
                {
                    Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 50, 50);

                    if (buttonPos.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y) && InputManager.MouseClicked)
                    {
                        clicked = button.Key;
                    }
                }

                switch (clicked)
                {
                    case "StartGame":
                        GameStates["MenuScreen"] = false;
                        GameStates["LevelSelectionScreen"] = true;
                        break;

                    case "MusicVolum1":
                        //Effects manager
                        break;

                    case "MusicVolum2":
                        //Effects manager
                        break;

                    case "MusicVolum3":
                        //Effects manager
                        break;
                    case "MusicVolumOff":
                        //Effects manager
                        break;

                }               
            }

            else if (GameStates["LevelSelectionScreen"])
            {
                string clicked = "";

                foreach (KeyValuePair<string, Vector2> button in LevelMenuButtons)
                {
                    Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 50, 50);

                    if (buttonPos.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y) && InputManager.MouseClicked)
                    {
                        Globals.Level = int.Parse(button.Key);
                        GameStates["LevelSelectionScreen"] = false;
                        GameStates["LevelSelectionScreen"] = true;
                        break;
                    }
                }             
            }
        }







    }
}
