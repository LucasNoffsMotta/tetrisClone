﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Tetris.Miscellanea;


namespace Tetris.Engine
{
    public class GameManager
    {
        private readonly Map _map;
        public readonly BricksManager _bricksManager;
        private readonly Texture2D background;
        private readonly SpriteFont font;


        public GameManager()
        {
            _map = new Map();
            _bricksManager = new BricksManager();
            background = Globals.Content.Load<Texture2D>("background5");
            font = Globals.Content.Load<SpriteFont>("File");
            UIScreens.CreateGameStates();
            UIScreens.CreateMenuMainMenuButtons();
            UIScreens.CreateLevelMenuButtons();
            UIScreens.CreateOptionsButtons();
            UIScreens.CreateSoundBar();
            Globals.Score = 0;
            Globals.LinesCleaned = 0;
            Effects.PlaySoundtrack(UIScreens.musicIndex);
            DataHelper.LoadRanking();
        }

        public void Update(GameTime gt)
        {
            Globals.Update(gt);
            InputManager.Update();


            if (UIScreens.GameStates["StartGame"])
            {
                if (!Globals.GameOver && !UIScreens.GameStates["Paused"])
                {
                    _bricksManager.Update(_map.PlayField, _map.Size);

                    if (InputManager.KeybordPressed.IsKeyDown(Keys.Escape))
                    {
                        UIScreens.GameStates["StartGame"] = false;
                        UIScreens.GameStates["Paused"] = true;
                        UIScreens.GameStates["Options"] = true;
                    }
                }

                else
                {
                    Effects.GameOverEffect(_map.PlayField, _bricksManager.bricks);

                    if (Effects.EffectFinished)
                    {
                        LevelManager.Update(_map.PlayField, _bricksManager);
                    }
                }
            }

            else if (!UIScreens.GameStates["StartGame"])
            {
                UIScreens.Update();
            }
        }



        public void Draw()
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if (!UIScreens.GameStates["StartGame"])
            {
                UIScreens.Draw();
            }


            else if (UIScreens.GameStates["StartGame"])
            {
                _map.Draw();
                _bricksManager.Draw();
                Globals.SpriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                _bricksManager.DrawDisplayTetromine();
                Globals.SpriteBatch.DrawString(font, Globals.Score.ToString(), new(150, 160), Color.White);
                Globals.SpriteBatch.DrawString(font, Globals.LinesCleaned.ToString(), new(220, 530), Color.White);
                Globals.SpriteBatch.DrawString(font, Globals.Level.ToString(), new(240, 325), Color.White);
                Ranking.Draw();
            }



            if (Effects.EffectFinished)
            {
                Effects.DrawGameOver();
            }

            Globals.SpriteBatch.End();
        }
    }
}