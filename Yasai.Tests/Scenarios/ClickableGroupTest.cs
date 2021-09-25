﻿using System;
using System.Numerics;
using Yasai.Graphics.Layout.Groups;
using Yasai.Input.Mouse;

namespace Yasai.Tests.Scenarios
{
    [TestScenario]
    public class ClickableGroupTest : Scenario
    {
        ClickableGroup g;

        public ClickableGroupTest()
        {
            Add(g = new ClickableGroup()
            {
                Fill = true,
                Position = new Vector2(400),
                Size = new Vector2(200)
            });
            g.OnRelease += GOnOnRelease;
        }

        private void GOnOnRelease(object? sender, EventArgs e)
        {
            MouseArgs args = (MouseArgs) e;
            Console.WriteLine(args.Button.ToString());
        }
    }
}