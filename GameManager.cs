
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Tetris
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
            _bricksManager.CreateBriks();
            background = Globals.Content.Load<Texture2D>("background3");
            font = Globals.Content.Load<SpriteFont>("File");
            Globals.Score = 0;
            Globals.LinesCleaned = 0;
        }

        public void Update(GameTime gt)
        {
            Globals.Update(gt);
            InputManager.Update();
            _bricksManager.Update(_map.PlayField, _map.Size);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _map.Draw();
            _bricksManager.Draw();
            Globals.SpriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            _bricksManager.DrawDisplayTetromine();
            Globals.SpriteBatch.DrawString(font, Globals.Score.ToString(), new(220, 160), Color.White);
            Globals.SpriteBatch.DrawString(font, Globals.LinesCleaned.ToString(), new(220, 530), Color.White);
            Globals.SpriteBatch.End();
        }
    }
}