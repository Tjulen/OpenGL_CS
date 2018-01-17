using System;
using Pencil.Gaming;


namespace OpenGL.Window
{
    class Window
    {
        const int WINDOW_HEIGHT = 600;
        const int WINDOW_WIDTH = 800;


        public static void Main(string[] args)
        {

            if (Glfw.Init() == false)
            {
                Console.WriteLine("ERROR: GLFW couldn't initialize, shutting down.");
                Environment.Exit(1);
            }


            Glfw.WindowHint(WindowHint.ContextVersionMajor, 3);
            Glfw.WindowHint(WindowHint.ContextVersionMinor, 3);
            Glfw.WindowHint(WindowHint.OpenGLForwardCompat, 1);

            GlfwWindowPtr window = Glfw.CreateWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Hello", GlfwMonitorPtr.Null, GlfwWindowPtr.Null);

            if (window.Equals(null))
            {
                Console.WriteLine("ERROR: Window not initialized correctly, shutting down.");
                Environment.Exit(1);
            }
            Glfw.MakeContextCurrent(window);

            //function pointer pointing to FrameBufferSizeCallBack, and it is passed to SetFramBufferSizeCallback
            //so that when glfw window is resized it uses this funtion pointer to access the function for "handling"
            //what happens when the window is resized.
            GlfwFramebufferSizeFun frameBufferSizeFun = new GlfwFramebufferSizeFun(FrameBufferSizeCallBack);
            Glfw.SetFramebufferSizeCallback(window, frameBufferSizeFun);


            float[] triangle =
            {
                -0.5f, -0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                0.0f,  0.5f, 0.0f
            };

            uint vbo;
            vbo = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            Gl.BufferData(BufferTarget.ArrayBuffer, System.Runtime.InteropServices.Marshal.SizeOf(triangle), BufferUsage.StaticDraw);




            while (!Glfw.WindowShouldClose(window))
            {
                //input
                ProcesInput(window);


                Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
                Gl.Clear(ClearBufferMask.ColorBufferBit);
                Gl.Clear(ClearBufferMask.DepthBufferBit);


                //check and call events and swap buffers
                Glfw.PollEvents();
                Glfw.SwapBuffers(window);
            }


            Glfw.Terminate();
        }

        static void ProcesInput(GlfwWindowPtr window)
        {
            if (Glfw.GetKey(window, Key.Escape))
            {
                Glfw.SetWindowShouldClose(window, true);
            }
        }
        static void FrameBufferSizeCallBack(GlfwWindowPtr window, int width, int height)
        {
            Gl.Viewport(0, 0, width, height);
        }
    }
}
