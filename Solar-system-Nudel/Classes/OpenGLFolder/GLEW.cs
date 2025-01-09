using System;
using System.Runtime.InteropServices;

public static class GLEW
{

    public const uint GL_FRAMEBUFFER = 0x8D40;
    public const uint GL_DEPTH_ATTACHMENT = 0x8D00;
    public const uint GL_TEXTURE_2D = 0x0DE1;



    [DllImport("opengl32.dll", EntryPoint = "glBindTexture")]
    public static extern void glBindTexture(uint target, uint texture);

    private const string GLEW_DLL = "glew32.dll";

    // Import GLEW initialization function
    [DllImport(GLEW_DLL, EntryPoint = "glewInit")]
    public static extern int Init();

    // Import GLEW version check
    [DllImport(GLEW_DLL, EntryPoint = "glewIsSupported")]
    public static extern bool IsSupported(string version);

    // Import modern OpenGL functions
    [DllImport("opengl32.dll", EntryPoint = "glGenFramebuffers")]
    public static extern void glGenFramebuffers(int n, uint[] framebuffers);

    [DllImport("opengl32.dll", EntryPoint = "glBindFramebuffer")]
    public static extern void glBindFramebuffer(uint target, uint framebuffer);

    [DllImport("opengl32.dll", EntryPoint = "glGenTextures")]
    public static extern void glGenTextures(int n, uint[] textures);

    [DllImport("opengl32.dll", EntryPoint = "glFramebufferTexture2D")]
    public static extern void glFramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level);


    // Additional modern functions can be added here
}
