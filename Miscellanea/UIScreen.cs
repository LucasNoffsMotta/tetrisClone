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
        public static Texture2D soudBarSquare = Globals.Content.Load<Texture2D>("soundBarSquare");
        public static Texture2D levelChoiceUI;
        public static Dictionary<string, bool> GameStates = new Dictionary<string, bool>();
        public static Dictionary<string, Vector2> MainMenuButtons = new Dictionary<string, Vector2>();
        public static Dictionary<string, Vector2> LevelMenuButtons = new Dictionary<string, Vector2>();
        public static Dictionary<string, Vector2> optionsButtons = new Dictionary<string, Vector2>();
        public static SpriteFont menuFont = Globals.Content.Load<SpriteFont>("SecondFont");
        public static SpriteFont optionsFont = Globals.Content.Load<SpriteFont>("optionsFont");
        public static SpriteFont musicNameFont = Globals.Content.Load<SpriteFont>("musicNameFont");
        private static Color startMenucolor = Color.DarkTurquoise;
        private static float menuBlinkCounter = 0;
        private static int colorMultiplicator = 1;
        private static Vector2[] levelsNumberStringArray = new Vector2[10];
        private static List<Texture2D> soundBar = new List<Texture2D>();
        private static Vector2 soundBarinitialpos = new(435, 200);
        private static List<Vector2> soundBarPos = new List<Vector2>();
        private static List<string> musicNames = new List<string> { "Music 1", "Music 2", "Music 3", "Music 4"};
        public static int musicIndex = 0;  



        public static void CreateGameStates()
        {
            GameStates["StartScreen"] = true;
            GameStates["MenuScreen"] = false;
            GameStates["Options"] = false;
            GameStates["LevelSelectionScreen"] = false;
            GameStates["StartGame"] = false;
            GameStates["Paused"] = false;
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
                levelsNumberStringArray[i] = numberStringPos;
                firstpos.X += 86;
                numberStringPos.X += 85;           
            }
        }

        public static void CreateOptionsButtons()
        {
            optionsButtons["plusVolume"] = new(600, 190);
            optionsButtons["minusVolume"] = new(410, 190);
            optionsButtons["backMusic"] = new(418, 345);
            optionsButtons["forwardMusic"] = new(600, 345);
            optionsButtons["Back"] = new(427,507);
        }


        public static void CreateSoundBar()
        {
            for (int i = 0; i < 10; i++)
            {
                soundBar.Add(soudBarSquare);
                soundBarPos.Add(soundBarinitialpos);
                soundBarinitialpos.X += 15;
            }
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
                int volumeDisplay = Convert.ToInt32(MediaPlayer.Volume * 10);
                Color color = Color.Aquamarine;

                foreach (KeyValuePair<string, Vector2> button in optionsButtons)
                {
                    Rectangle buttonPos = new Rectangle((int)button.Value.X, (int)button.Value.Y, 30, 30);
                    if(buttonPos.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y))
                    {
                        color = Color.Yellow;
                    }

                    else
                    {
                        color = Color.Aquamarine;
                    }
                   
                    switch (button.Key)
                    {
                        case "plusVolume":
                            Globals.SpriteBatch.DrawString(optionsFont, "+", button.Value, color);
                            break;

                        case "minusVolume":
                            Globals.SpriteBatch.DrawString(optionsFont, "-", button.Value, color);
                            break;

                        case "backMusic":
                            Globals.SpriteBatch.DrawString(optionsFont, "<", button.Value, color);
                            break;

                        case "forwardMusic":
                            Globals.SpriteBatch.DrawString(optionsFont, ">", button.Value, color);
                            break;
                    }                   
                }

                for (int i = 0; i < volumeDisplay; i++)
                {
                    Globals.SpriteBatch.Draw(soundBar[i], soundBarPos[i], Color.White);
                }

                Globals.SpriteBatch.DrawString(musicNameFont, musicNames[musicIndex], new(480, 349), Color.LightGreen);
            }

            else if (GameStates["LevelSelectionScreen"])
            {
                Globals.SpriteBatch.Draw(levelScreen, new Vector2(0, 0), Color.White);

                for (int i = 0; i < 10; i ++)
                {
                 
                    Rectangle hoverLevelButton = new Rectangle ((int)LevelMenuButtons[i.ToString()].X, (int)LevelMenuButtons[i.ToString()].Y, 75,75);

                    if (hoverLevelButton.Contains(InputManager.MouseRect.X, InputManager.MouseRect.Y))
                    {
                        Globals.SpriteBatch.DrawString(menuFont, i.ToString(), levelsNumberStringArray[i], Color.Yellow);
                    }

                    else
                    {
                        Globals.SpriteBatch.DrawString(menuFont, i.ToString(), levelsNumberStringArray[i], Color.Red);
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
                    case "plusVolume":
                        MediaPlayer.Volume += 0.1f;                        
                        break;

                    case "minusVolume":
                        MediaPlayer.Volume -= 0.1f;
                        break;

                    case "backMusic":
                        if (musicIndex > 0)
                        {
                            musicIndex--;
                        }
                        Effects.PlaySoundtrack(musicIndex);
                        break;

                    case "forwardMusic":
                        if (musicIndex < musicNames.Count - 1)
                        {
                            musicIndex++;
                        }
                        Effects.PlaySoundtrack(musicIndex);
                        break;

                    case "Back":
                        GameStates["Options"] = false;

                        if (!GameStates["Paused"])
                        {
                            GameStates["MenuScreen"] = true;
                        }

                        else
                        {
                            GameStates["Paused"] = false;
                            GameStates["StartGame"] = true;
                        }
                        break;
                }
            }

            else if (GameStates["LevelSelectionScreen"])
            {

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
