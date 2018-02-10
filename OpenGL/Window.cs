using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTest
{
    class Game : GameWindow
    {
        const int WINDOW_WIDTH = 1280;
        const int WINDOW_HEIGHT = 720;

        public Game() :
            base(WINDOW_WIDTH,
                WINDOW_HEIGHT,
                OpenTK.Graphics.GraphicsMode.Default,
                "Hello OpenTK!",
                GameWindowFlags.Default,
                DisplayDevice.Default,
                4,
                0,
                OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(System.Drawing.Color.CadetBlue);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SwapBuffers();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
    }
}