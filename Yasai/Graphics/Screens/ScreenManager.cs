﻿using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Yasai.Structures.DI;

namespace Yasai.Graphics.Screens
{
    public class ScreenManager : Drawable
    {
        public Screen CurrentScreen { get; private set; }

        public event EventHandler OnScreenChange;

        public override Vector2 Size
        {
            get => base.Size;
            set
            {
                base.Size = value;
                CurrentScreen.Size = value;
            }
        }

        public ScreenManager(Screen s)
        {
            CurrentScreen = s;
        }

        public ScreenManager() : this (new Screen()) { }

        private DependencyContainer dependencies;
        
        public override void Load(DependencyContainer container)
        {
            base.Load(container);
            CurrentScreen.Load(container);
            dependencies = container;

            var window = container.Resolve<GameWindow>();
            Size = window.Size;
            
            Loaded = true;
        }

        // appears to be a memory leak when pushing new screens
        // this still seems insignificant ..
        public void PushScreen(Screen s)
        {
            if (Loaded)
            {
                s.Load(dependencies);
            }

            s.Size = Size;

            CurrentScreen.Dispose();

            CurrentScreen = s;
            OnScreenChange?.Invoke(this, new ScreenArgs(s));
        }

        public override void Update(FrameEventArgs args)
        {
            base.Update(args);
            
            if (Enabled)
                CurrentScreen.Update(args);
        }

        public sealed override void Draw()
        {
            base.Draw();
            
            if (Enabled && Visible)
                CurrentScreen.Draw();
        }
        
        // input

        public override void GlobalMouseMove(MouseMoveEventArgs args)
        {
            base.GlobalMouseMove(args);
            CurrentScreen.GlobalMouseMove(args);
        }

        public override void GlobalMousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
        {
            base.GlobalMousePress(position, buttonArgs);
            CurrentScreen.GlobalMousePress(position, buttonArgs);
        }

        public override bool MouseMove(MouseMoveEventArgs args)
        {
            CurrentScreen.MouseMove(args);
            return base.MouseMove(args);
        }

        public override bool MousePress(Vector2 position, MouseButtonEventArgs buttonArgs)
        {
            CurrentScreen.MousePress(position, buttonArgs);
            return base.MousePress(position, buttonArgs);
        }

        public override bool MouseScroll(Vector2 position, MouseWheelEventArgs args)
        {
            CurrentScreen.MouseScroll(position, args);
            return base.MouseScroll(position, args);
        }

        public override void KeyDown(KeyboardKeyEventArgs args)
        {
            CurrentScreen.KeyDown(args);
            base.KeyDown(args);
        }

        public override void KeyUp(KeyboardKeyEventArgs args)
        {
            CurrentScreen.KeyUp(args);
            base.KeyUp(args);
        }

        // screenmanagers can only handle one screen at a time
        // thus, they all ignore the hierarchy
        //public override void MouseDown(MouseArgs args)    => CurrentScreen.MouseDown(args);
        //public override void MouseUp(MouseArgs args)      => CurrentScreen.MouseUp(args);
        //public override void MouseMotion(MouseArgs args)  => CurrentScreen.MouseMotion(args);
        //public override void KeyUp(KeyArgs key)           => CurrentScreen.KeyUp(key);
        //public override void KeyDown(KeyArgs key)         => CurrentScreen.KeyDown(key);
    }
}