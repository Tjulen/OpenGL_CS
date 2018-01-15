using System;



namespace OpenGL.Window
{
    class Window
    {
        const int WINDOW_HEIGHT = 600;
        const int WINDOW_WIDTH = 800;


        public static void Main(string[] args)
        {
            if (glfw3.Glfw.Init() == 0)
            {
                Environment.Exit(-1);
            }
            if (!Glfw.Init())
            {
                Environment.Exit(-1);
            }
            Glfw.WindowHint(target: WindowHint.ContextVersionMajor, hint: 3);
            Glfw.WindowHint(target: WindowHint.ContextVersionMinor, hint: 3);

            //Commenting this line avoids crashes and abilities to not initialize correctly

            //Glfw.WindowHint(target: WindowHint.OpenGLProfile, hint: 139272);
            Glfw.WindowHint(target: WindowHint.OpenGLForwardCompat, hint: 139270);
            OpenGL.Gl.view
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

            GlfwFramebufferSizeFun frameBufferSizeCallBack = new GlfwFramebufferSizeFun(Framebuffer_size_callback);

            Glfw.MakeContextCurrent(window);
            Glfw.SetFramebufferSizeCallback(window, frameBufferSizeCallBack);



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
        static void Framebuffer_size_callback(GlfwWindowPtr window, int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }
    }
}
