using System;

using Pencil.Gaming;
using OpenTK.Graphics.OpenGL;

namespace OpenGL.Window
{
    class Window
    {
        const int WINDOW_HEIGHT = 600;
        const int WINDOW_WIDTH = 800;


        public static void Main(string[] args)
        {
            if (!Glfw.Init())
            {
                Environment.Exit(-1);
            }
            Glfw.WindowHint(target: WindowHint.ContextVersionMajor, hint: 3);
            Glfw.WindowHint(target: WindowHint.ContextVersionMinor, hint: 3);

            //Commenting this line avoids crashes and abilities to not initialize correctly
            //Glfw.WindowHint(target: WindowHint.OpenGLProfile, hint: 139272);
            Glfw.WindowHint(target: WindowHint.OpenGLForwardCompat, hint: 139270);

            //Create window with given parameters
            GlfwWindowPtr window = Glfw.CreateWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Hello OpenGL", GlfwMonitorPtr.Null, GlfwWindowPtr.Null);

            //-----------------------------------------------------------------------------------------------------------------------
            //Checks whether the window was successfully initialized, and if not throws an exception
            if (window.Equals(GlfwWindowPtr.Null))
            {
                Console.WriteLine("Failed to initialize");
                Console.WriteLine("Closing...");
                Glfw.Terminate();
                Console.ReadKey();
                Environment.Exit(-1);
            }

            Glfw.MakeContextCurrent(window);
            //GlfwFramebufferSizeFun(window, WINDOW_WIDTH, WINDOW_HEIGHT);
            //Glfw.SetFramebufferSizeCallback(window, GlfwFramebufferSizeFun(window, WINDOW_WIDTH, WINDOW_HEIGHT));





            uint VBO = 0;





            ShapesTest shapes = new ShapesTest();



            //-----------------------------------------------------------------------------------------------------------------------
            //Main loop
            while (!Glfw.WindowShouldClose(window))
            {
                ProcessInput(window);

                Glfw.SwapBuffers(window);
                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }

        //-----------------------------------------------------------------------------------------------------------------------
        static void ProcessInput(GlfwWindowPtr inWindow)
        {
            if (Glfw.GetKey(inWindow, Key.Escape))
            {
                Glfw.SetWindowShouldClose(inWindow, true);
            }
        }

        //static GlfwFramebufferSizeFun Framebuffer_size_callback(int inWidth, int inHeight)
        //{
        //    new GlfwFramebufferSizeFun buffer = GlfwFramebufferSizeFun(window, WINDOW_WIDTH, WINDOW_HEIGHT);
        //    GL.Viewport(0, 0, inWidth, inHeight);
        //}
    }
}
