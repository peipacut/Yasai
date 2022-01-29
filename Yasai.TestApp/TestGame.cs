﻿using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Yasai.Audio;
using Yasai.Graphics;
using Yasai.Graphics.Shapes;
using Yasai.Resources.Stores;
using Yasai.Structures.DI;

namespace Yasai.TestApp
{
    public class TestGame : Game
    {
        private Box box;

        public TestGame()
        {
        }
        
        public override void Load(DependencyContainer dependencies)
        {
            base.Load(dependencies);
            Root.Add(box = new Box
            {
                Anchor = Anchor.Center,
                Origin = Anchor.Center,
                Size = new Vector2(200)
            });
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            box.Rotation += 0.01f;
        }
    }
}