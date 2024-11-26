using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Solar_system_Nudel.Classes
{
    internal class SolarSystemGame : GameWindow
    {
        // Fields for buffer and shader program
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _shaderProgram;

        // Shader source codes
        private const string VertexShaderSource = @"
        #version 330 core
        layout(location = 0) in vec2 aPosition;

        void main()
        {
            gl_Position = vec4(aPosition, 0.0, 1.0);
        }";

        private const string FragmentShaderSource = @"
        #version 330 core
        out vec4 FragColor;

        void main()
        {
            FragColor = vec4(1.0, 0.5, 0.2, 1.0);
        }";

        // Constructor
        public SolarSystemGame()
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(800, 600),
                Title = "Solar System Simulation"
            })
        {
        }

        // Load method for initial setup
        protected override void OnLoad()
        {
            base.OnLoad();

            // Define vertices of a triangle
            float[] vertices = {
                -0.5f, -0.5f,  // Bottom left
                 0.5f, -0.5f,  // Bottom right
                 0.0f,  0.5f   // Top center
            };

            // Generate Vertex Buffer Object (VBO)
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Generate Vertex Array Object (VAO)
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // Link the VBO to the VAO
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Compile vertex shader
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, VertexShaderSource);
            GL.CompileShader(vertexShader);
            CheckShaderCompileStatus(vertexShader);

            // Compile fragment shader
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, FragmentShaderSource);
            GL.CompileShader(fragmentShader);
            CheckShaderCompileStatus(fragmentShader);

            // Link shaders into a program
            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, vertexShader);
            GL.AttachShader(_shaderProgram, fragmentShader);
            GL.LinkProgram(_shaderProgram);
            CheckProgramLinkStatus(_shaderProgram);

            // Clean up shaders (they're linked to the program now)
            GL.DetachShader(_shaderProgram, vertexShader);
            GL.DetachShader(_shaderProgram, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Set the clear color
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        // Render frame method to draw the scene
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Clear the buffers

            // Use the shader program and bind the VAO
            GL.UseProgram(_shaderProgram);
            GL.BindVertexArray(_vertexArrayObject);

            // Draw the triangle
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            SwapBuffers(); // Swap the front and back buffers to display the rendered content
        }

        // Resize method to handle window resizing
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y); // Update the viewport to match the new window size
        }

        // Helper methods to check shader and program compilation status
        private void CheckShaderCompileStatus(int shader)
        {
            GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error compiling shader: {infoLog}");
            }
        }

        private void CheckProgramLinkStatus(int program)
        {
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(program);
                throw new Exception($"Error linking program: {infoLog}");
            }
        }
    }
}
