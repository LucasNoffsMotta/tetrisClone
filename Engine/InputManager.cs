using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.MediaFoundation.DirectX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Engine
{
    public static class InputManager
    {
        public static Vector2 direction;
        public static KeyboardState KeybordPressed;
        public static Vector2 Direction => direction;
        public static bool Moving => direction != Vector2.Zero;

        private static MouseState _lastMouseState;
        public static bool MouseClicked { get; private set; }
        public static bool MouseRightClicked { get; private set; }

        public static Rectangle MouseRect { get; private set; }


        public static void Update()
        {
            direction = Vector2.Zero;


            KeybordPressed = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            MouseClicked = mouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released;
            MouseRightClicked = mouseState.RightButton == ButtonState.Pressed && _lastMouseState.RightButton == ButtonState.Released;
            MouseRect = new(mouseState.Position.X, mouseState.Position.Y, 1, 1);
            _lastMouseState = mouseState;
        }
    }
}
