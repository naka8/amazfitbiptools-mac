﻿using System.Drawing;
using WatchFace.Parser.Models.Elements.Common;

namespace WatchFace.Parser.Models.Elements.AnalogDial2
{
    public class SecondsClockHand2Element : ClockHand2Element
    {
        public SecondsClockHand2Element(Parameter parameter, Element parent, string name = null) :
            base(parameter, parent, name) { }

        public override void Draw(Graphics drawer, Bitmap[] resources, WatchState state)
        {
            Draw(drawer, resources, state.Time.Second, 60);
        }
    }
}