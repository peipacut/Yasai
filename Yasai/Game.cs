using System;
using System.Numerics;
using System.Reflection;
using Yasai.Extensions;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.YasaiSDL;
using Yasai.Input.Keyboard;
using Yasai.Input.Mouse;
using Yasai.Resources;

using static SDL2.SDL;
using static SDL2.SDL_ttf;

namespace Yasai
{
    /// <summary>
    /// main game instance, globally available information lives here
    /// </summary>
    public class Game : IGroup
    {
        public Window Window { get; private set; }
        public Renderer Renderer { get; private set; } 
        
        private bool _quit;

        public bool Loaded => Window != null && Renderer != null && _content != null;
        
        public bool Visible { get; set; } = true;
        public bool Enabled
        {
            get => throw new Exception("use Game.Quit() to quit the game");
            set => throw new Exception("use Game.Quit() to quit the game");
        }

        protected Group Children;
        
        private ContentCache _content;


        public Game(string[] args = null)
        { 
            if (SDL_Init(SDL_INIT_EVERYTHING) != 0) 
                Console.WriteLine($"error on startup: {SDL_GetError()}");
            TTF_Init();
            
            // everything else
            Children = new Group();
        }

        public void Run() => Run(60);

        public void Run(int refreshRate) =>
            Run($"Yasai running {Assembly.GetEntryAssembly()?.GetName().Name} @ {refreshRate}Hz", refreshRate);
        
        public void Run(string title, int refreshRate = 60) => Run(title, 1366, 768, refreshRate);
        
        public void Run(string title, int w, int h, int refreshRate)
        {
            Window = new Window(title, w, h, refreshRate);
            Renderer = new Renderer(Window);
            
            _content = new ContentCache(this);

            Start(_content);
            
            Children.Load(_content);
            Load(_content);
            
            while (!_quit)
            {
                while (SDL_PollEvent(out var e) != 0)
                    OnEvent(e);

                Update();
                
                Renderer.Clear();
                Draw(Renderer.GetPtr());
                Renderer.Present();
                Renderer.SetDrawColor(0,0,0,255);
            }
        }

        protected virtual void OnEvent(SDL_Event ev)
        {
            switch (ev.type) 
            {
                // program exit
                case (SDL_EventType.SDL_QUIT):
                    _quit = true;
                    break;
                
                #region input systems
                case (SDL_EventType.SDL_KEYUP):
                    KeyUp(ev.key.keysym.sym.ToYasaiKeyCode());
                    break;
                
                case (SDL_EventType.SDL_KEYDOWN):
                    KeyDown(ev.key.keysym.sym.ToYasaiKeyCode());
                    break;
                
                case (SDL_EventType.SDL_MOUSEBUTTONUP):
                    MouseUp(new MouseArgs((MouseButton) ev.button.button, new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                case (SDL_EventType.SDL_MOUSEBUTTONDOWN):
                    MouseDown(new MouseArgs((MouseButton) ev.button.button, new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                case (SDL_EventType.SDL_MOUSEWHEEL):
                    // TODO: mousewheel
                    break;
                
                case (SDL_EventType.SDL_MOUSEMOTION):
                    MouseMotion(new MouseArgs(new Vector2(ev.button.x, ev.button.y)));
                    break;
                
                #endregion
            }
        }

        public virtual void Start(ContentCache cache) => Children.Start(cache);
        
        public virtual void Load(ContentCache cache)
        { }

        public virtual void Update() => Children.Update();

        // it's probably more efficient to pass around pointers than actual objects
        // can change this later though if need be
        public virtual void Draw(IntPtr ren)
        {
            if (Visible)
                Children.Draw(ren);
            else
            {
#if DEBUG
                Console.WriteLine("Game is invisible. The use of Game.Visible is highly unadvised in the first place!");
#endif
            }
                
        }

        /// <summary>
        /// Quit the current game
        /// </summary>
        public void Quit()
        {
            _quit = false;
            Dispose();
        }

        public void Dispose()
        {
            Children.Dispose();
            _content.Dispose();
            SDL_Quit();
        }

        #region input
        public bool IgnoreHierachy => true;
        public virtual void MouseDown(MouseArgs args) => Children.MouseDown(args);
        public virtual void MouseUp(MouseArgs args) => Children.MouseUp(args);
        public virtual void MouseMotion(MouseArgs args) => Children.MouseMotion(args);
        public virtual void KeyUp(KeyCode key) => Children.KeyUp(key);
        public virtual void KeyDown(KeyCode key) => Children.KeyDown(key);
        #endregion
    }
}