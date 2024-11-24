using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class UIScreens
    {
        public static Texture2D startScreen =  Globals.Content.Load<Texture2D>("startScreen");
        public static Texture2D mainMenuUI = Globals.Content.Load<Texture2D>("mainMenuScreen");
        public static Texture2D hoverSurface = Globals.Content.Load<Texture2D>("hoverSurface");
        public static Texture2D levelScreen = Globals.Content.Load<Texture2D>("levelScreen");
        public static Texture2D optionsScreen = Globals.Content.Load<Texture2D>("optionsScreen");
        public static Texture2D levelChoiceUI;
        public static Dictionary<string, bool> GameStates = new Dictionary<string, bool>();
        public static Dictionary<string, Vector2> MainMenuButtons = new Dictionary<string, Vector2>();
        public static Dictionary<string, Vector2> LevelMenuButtons = new Dictionary<string, Vector2>();
        public static Dictionary<string, Vector2> optionsButtons = new Dictionary<string, Vector2>();
        public static SpriteFont menuFont = Globals.Content.Load<SpriteFont>("SecondFont");
        public static SpriteFont optionsFont = Globals.Content.Load<SpriteFont>("optionsFont");
        private static Color startMenucolor = Color.DarkTurquoise;
        private static float menuBlinkCounter = 0;
        private static int colorMultiplicator = 1;
        private static Vector2[] numberStringArray = new Vector2[10];




        public static void CreateGameStates()
        {
            GameStates["StartScreen"] = true;
            GameStates["MenuScreen"] = false;
            GameStates["Options"] = false;
            GameStates["LevelSelectionScreen"] = false;
            GameStates["StartGame"] = false;
        }

        public static void CreateMenuMainMenuButtons()
        {
            MainMenuButtons["StartGame"] = new(365, 231);
            MainMenuButtons["Options"] = new(365, 331);
            MainMenuButtons["Exit"] = new(365, 431);

        }

        public static void CreateLevelMenuButtons()
        {
            Vector2 firstpos = new Vector2(214,464);
            Vector2 numberStringPos = new Vector2(240, 470);

            for (int i = 0; i < 10; i++)
            {
                if (i == 5)
                {
                    firstpos = new Vector2(214, 464);
                    numberStringPos = new Vector2(240, 470);
                    firstpos.Y += 77;
                    numberStringPos.Y += 80;
                }

                LevelMenuButtons[i.ToString()] = new(firstpos.X, firstpos.Y);
                numberStringArray[i] = numberStringPos;
                firstpos.X += 86;
                numberStringPos.X += 85;           
            }
        }

        public static void CreateOptionsButtons()
        {
            optionsButtons["plusVolume"] = new(507, 200);
            optionsButtons["minusVolume"] = new(418, 200);
            optionsButtons["backMusic"] = new(418, 355);
            optionsButtons["forwardMusic"] = new(506, 355);
            optionsButtons["Back"] = new(427,507);

        }


        public static void Draw()
        {
            
            if (GameStates["StartScreen"])
            {
                Globals.SpriteBatch.Draw(startScreen, new Vector2(0, 0), Color.White);
                menuBlinkCounter += 0.05f;
                Globals.SpriteBatch.DrawString(menuFont, "Press Enter to Start", new Vector2(280, 470), Color.DarkTurquoise * colorMultiplicator);
                if (menuBlinkCounter > 1f)
                {
                    menuBlinkCounter = 0f;
                    switch (colorMultiplicator)
                    {
                        case 0:
                            colorMultiplicator = 1;
                            break;
                        case 1:
                            colorMultiplicator = 0;
                            break;                            
                    }
                }             
            }

            else if (GameStates["MenuScreen"])
            {
                Globals.SpriteBatch.Draw(mainMenuUI, new Vector2(0, 0), Color.White);

                foreach (KeyValuePair<string, Vector2> button in MainMenuButtons)
                {
                    Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 240, 65);

                    if (buttonPos.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y))
                    {
                        Globals.SpriteBatch.Draw(hoverSurface, buttonPos, Color.White);
                    }
                }
            }

            else if (GameStates["Options"])
            {
                Globals.SpriteBatch.Draw(optionsScreen, new Vector2(0, 0), Color.White);

                foreach (KeyValuePair<string, Vector2> button in optionsButtons)
                {
                    //Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 20, 20);

                    switch (button.Key)
                    {
                        case "plusVolume":
                            Globals.SpriteBatch.DrawString(optionsFont, "+", button.Value, Color.Aquamarine);
                            break;

                        case "minusVolume":
                            Globals.SpriteBatch.DrawString(optionsFont, "-", button.Value, Color.Aquamarine);
                            break;

                        case "backMusic":
                            Globals.SpriteBatch.DrawString(optionsFont, "<", button.Value, Color.Aquamarine);
                            break;

                        case "forwardMusic":
                            Globals.SpriteBatch.DrawString(optionsFont, ">", button.Value, Color.Aquamarine);
                            break;
                    }                   
                }



            }

            else if (GameStates["LevelSelectionScreen"])
            {
                Globals.SpriteBatch.Draw(levelScreen, new Vector2(0, 0), Color.White);

                for (int i = 0; i < 10; i ++)
                {
                 
                    Rectangle hoverLevelButton = new Rectangle ((int)LevelMenuButtons[i.ToString()].X, (int)LevelMenuButtons[i.ToString()].Y, 75,75);

                    if (hoverLevelButton.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y))
                    {
                        Globals.SpriteBatch.DrawString(menuFont, i.ToString(), numberStringArray[i], Color.Yellow);
                    }
                    else
                    {
                        Globals.SpriteBatch.DrawString(menuFont, i.ToString(), numberStringArray[i], Color.Red);
                    }
                    
                }

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
                    Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 300, 50);

                    if (buttonPos.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y) && InputManager.MouseClicked)
                    {
                        clicked = button.Key;
                    }
                }
                Debug.WriteLine(clicked);

                switch (clicked)
                {
                    case "StartGame":
                        GameStates["MenuScreen"] = false;
                        GameStates["LevelSelectionScreen"] = true;
                        break;

                    case "Options":
                        GameStates["MenuScreen"] = false;
                        GameStates["Options"] = true;                       
                        break;

                    case "Exit":
                        GameStates["MenuScreen"] = false;
                        GameStates["StartScreen"] = true;
                        break;
                }               
            }

            else if (GameStates["Options"])
            {
                string clicked = "";
                foreach (KeyValuePair<string, Vector2> button in optionsButtons)
                {
                    Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 30, 30);

                    if (buttonPos.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y) && InputManager.MouseClicked)
                    {
                        clicked = button.Key;
                    }
                }

                switch (clicked)
                {
                    case "PlusVolume":
                        MediaPlayer.Volume += 0.1f;                        
                        break;

                    case "MinusVolume":
                        MediaPlayer.Volume -= 0.1f;
                        break;

                    case "Back":
                        GameStates["Options"] = false;
                        GameStates["MenuScreen"] = true;
                        break;
                }
                Debug.WriteLine(clicked);
                Debug.WriteLine(MediaPlayer.Volume.ToString());
            }

            else if (GameStates["LevelSelectionScreen"])
            {
                string clicked = "";

                foreach (KeyValuePair<string, Vector2> button in LevelMenuButtons)
                {
                    Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 75, 75);

                    if (buttonPos.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y) && InputManager.MouseClicked)
                    {
                        Globals.Level = int.Parse(button.Key);
                        GameStates["LevelSelectionScreen"] = false;
                        GameStates["StartGame"] = true;
                        break;
                    }
                }             
            }
        }







    }
}
