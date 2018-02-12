using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTest
{
    class Game : GameWindow
    {
        readonly uint WINDOW_WIDTH;
        readonly uint WINDOW_HEIGHT;

        public Game(uint winWidth, uint winHeight) :
            base((int)winWidth,
                (int)winHeight,
                OpenTK.Graphics.GraphicsMode.Default,
                "Hello OpenTK!",
                GameWindowFlags.Default,
                DisplayDevice.Default,
                4,
                0,
                OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
            WINDOW_WIDTH = winWidth;
            WINDOW_HEIGHT = winHeight;
        }

        static int CompileShader(ShaderType type, ref string source)
        {
            int id = GL.CreateShader(type);
            GL.ShaderSource(id, source);
            GL.CompileShader(id);


            //error handling
            int result;
            GL.GetShader(id, ShaderParameter.CompileStatus, out result);
            //0 means false
            if(result == 0)
            {
                int lenght;
                GL.GetShader(id, ShaderParameter.InfoLogLength, out lenght);
                string message;
                GL.GetShaderInfoLog(id, lenght, out lenght, out message);
                Console.WriteLine("Failed to compile" + (type == ShaderType.VertexShader ? "vertex" : "fragment") + "shader!");
                Console.WriteLine(message);
                GL.DeleteShader(id);
                return 0;
            }

            return id;
        }
        static int CreateShader(string vertexShader, string fragmentShader)
        {
            int program = GL.CreateProgram();
            int vs = CompileShader(ShaderType.VertexShader, ref vertexShader);
            int fs = CompileShader(ShaderType.FragmentShader, ref fragmentShader);

            GL.AttachShader(program, vs);
            GL.AttachShader(program, fs);
            GL.LinkProgram(program);
            GL.ValidateProgram(program);

            GL.DeleteShader(vs);
            GL.DeleteShader(fs);

            return program;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(System.Drawing.Color.Black);

            float[] position =
            {
                -0.5f, -0.5f,
                 0.0f,  0.5f,
                 0.5f,  -0.5f,
            };
            uint buffer;
            GL.GenBuffers(1, out buffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData<float>(BufferTarget.ArrayBuffer, 6 * sizeof(float), position, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, IntPtr.Zero);

            string vertexShader =
                "#version 330 core\n" +
                "layout(location = 0) in vec4 position;\n" +
                "void main()\n" +
                "{\n" +
                "   gl_Position = position;\n" +
                "}\n";
            string fragmentShader =
                "#version 330 core\n" +
                "layout(location = 0) out vec4 color;\n" +
                "void main()\n" +
                "{\n" +
                "   color = vec4(0.0, 1.0, 0.0, 1.0);\n" +
                "}\n";
            int shader = CreateShader(vertexShader, fragmentShader);
            GL.UseProgram(shader);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //if weird things occur when resizing, it may be because of a part of code that I left out here, involving matrices
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}