using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameManager
    {
        private readonly Map _map;
        private readonly BricksManager _bricksManager;


        public GameManager()
        {
            _map = new Map();        
            _bricksManager = new BricksManager();
            _bricksManager.CreateBriks();

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
            Globals.SpriteBatch.End();
        }

    }
}
