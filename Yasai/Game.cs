using System;
using System.Drawing;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Graphics;
using Yasai.Graphics.Imaging;
using Yasai.Graphics.Shaders;
using Yasai.Structures.DI;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : GameBase
    {
        //protected Container Root;

        //private FontStore fontStore;

        private static readonly string DEFAULT_NAME = $"Yasai running {Assembly.GetEntryAssembly()?.GetName().Name}";

        #region constructors
        
        public Game(string title=null, int w=1366, int h=768, string[] args = null) 
            : this (title ?? DEFAULT_NAME, GameWindowSettings.Default, new NativeWindowSettings { Size = new Vector2i(w, h) }, args)
        { }
        
        public Game(string title, GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, string[] args = null) 
            : base (title, gameWindowSettings, nativeWindowSettings, args)
        {
            //Root = new Container();
            //Children.Add(Root);

            // register font store 
            //Dependencies.Register<FontStore>(fontStore = new FontStore(Dependencies, @"Assets/Fonts"));
        }
        #endregion

        /// <summary>
        /// Load framework related resources
        /// </summary>
        private void yasaiLoad()
        {
            // fonts
           //fontStore.LoadResource(@"OpenSans-Regular.ttf", SpriteFont.FontTiny, new FontArgs(14));
           //fontStore.LoadResource(@"LigatureSymbols.ttf", SpriteFont.SymbolFontTiny, new FontArgs(14));
        }

        private int vbo;
        private int vao;

        private Matrix4 model;

        private Shader shader;
        private int elementBufferObject;
        private readonly float[] vertices =
        {
            // Position         TextureTemp coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 0.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 1.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 1.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 0.0f  // top left
        };
        private readonly uint[] indices =
        {
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };

        public override void Load(DependencyContainer dependencies)
        {
            yasaiLoad();
            base.Load(dependencies);
            
            GL.ClearColor(Color.CornflowerBlue);
            
            GL.Enable(EnableCap.DepthTest);
            
            // vao
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            // vbo
            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            // ebo
            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            
            // shaders
            shader = new Shader(@"Assets/vert_shader.glsl", @"Assets/frag_shader.glsl");
            shader.Use();
            
            var vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            
            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            // textures
            tex = new Texture("Assets/tex.png");
            tex.Use();
        }

        private Texture tex;

        public override void Unload(DependencyContainer dependencies)
        {
            base.Unload(dependencies);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
            shader.Dispose();
        }

        private double time;
        public sealed override void Draw(FrameEventArgs args)
        {
            base.Draw(args);
            time += args.Time;

            model = Matrix4.Identity * 
                    Matrix4.CreateScale(100,100,0) *             // Scale
                    Matrix4.CreateTranslation(100, 100, 0)       // translation
                    ;
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindVertexArray(vao);
            
            tex.Use();
            shader.Use();

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", Projection);
            
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            
            Window.SwapBuffers();
        }
    }
}