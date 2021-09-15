﻿using System;
using OpenTK.Mathematics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Graphics.Primitives;
using Yasai.Input.Mouse;
using Yasai.Resources;

namespace Yasai.Tests.Scenarios
{
    public class MouseTest : Scenario
    {
        public MouseTest()
        {
            Add(new MouseInput(true, true)
            {
                Position = new Vector2(150,100)
            });
            
            Add(new MouseInput(false)
            {
                Position = new Vector2(200,200)
            });
            
            Add(new MouseInput(false)
            {
                Position = new Vector2(210,200)
            });
            
            Add(new MouseInput(false)
            {
                Position = new Vector2(220,200)
            });
        }
    }

    sealed class MouseInput : Group
    {
        public bool IgnoreHierachy { get; }
        
        private Box box;
        private bool noisy;
        
        public MouseInput (bool ignoreHierachy, bool noisy = false)
        {
            IgnoreHierachy = ignoreHierachy;
            this.noisy = noisy;
        }

        public override void Load(ContentStore cs)
        {
            Add(box = new Box()
            {
                Position = Position,
                Size = new Vector2(200),
                Fill = false
            });
            
            base.Load(cs);
        }
        
        public override void MouseDown(MouseArgs args)
        {
            base.MouseDown(args);
            
            if (args.Button == MouseButton.Left)
                box.Fill = true;
        }

        public override void MouseUp(MouseArgs args)
        {
            base.MouseUp(args);
            
            box.Fill = false;
        }

        public override void MouseMotion(MouseArgs args)
        {
            base.MouseMotion(args);
            
            if (noisy)
                Console.WriteLine(args.Position);
        }
    }
}