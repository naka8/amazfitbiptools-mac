using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using WatchFace.Parser.Interfaces;
using static WatchFace.Parser.PreviewGenerator;

namespace WatchFace.Parser.Models.Elements.Common
{
    public abstract class ClockHand2Element : CompositeElement, IDrawable
    {
        protected ClockHand2Element(Parameter parameter, Element parent, string name = null) :
            base(parameter, parent, name)
        { }

        public Color color { get; set; }
        public CoordinatesElement Center{ get; set; }
        public long ImageIndex{ get; set; }

        public abstract void Draw(Graphics drawer, Bitmap[] resources, WatchState state);

        public void Draw(Graphics drawer, Bitmap[] resources, double value, double total)
        {
            var angle = value * 360 / total;
            //clockHand?.Draw(drawer, resources);
            if (ImageIndex > 0 )
            {
                DrawClockHand(drawer, resources, (float)angle, new Point((int)Center.X, (int)Center.Y));
            }
        }

        private void DrawClockHand(Graphics drawer, Bitmap[] images, float angle, Point centerOffset)
        {
            var image = images[ImageIndex];
            drawer.TranslateTransform(+ centerOffset.X, + centerOffset.Y);
            drawer.RotateTransform(angle);
            //drawer.DrawImage(images[ImageIndex], new Point((int)X, (int)Y));
            drawer.DrawImage(image, new Rectangle((int)-image.Width/2, (int)-image.Height+30, image.Width, image.Height));
            drawer.RotateTransform(-angle);
            drawer.TranslateTransform(- centerOffset.X, - centerOffset.Y);
        }

        private Point RotatePoint(CoordinatesElement element, double degrees)
        {
            var radians = degrees / 180 * Math.PI;
            var x = element.X * Math.Cos(radians) + element.Y * Math.Sin(radians);
            var y = element.X * Math.Sin(radians) - element.Y * Math.Cos(radians);
            return new Point((int)Math.Floor(x + Center.X), (int)Math.Floor(y + Center.Y));
        }

        protected override Element CreateChildForParameter(Parameter parameter)
        {
            switch (parameter.Id)
            {

                case 1:
                    ImageIndex = parameter.Value;
                    return new ValueElement(parameter, this, nameof(ImageIndex));
                case 2:
                    color = Color.FromArgb(0xff, Color.FromArgb((int)parameter.Value));
                    return new ValueElement(parameter, this);
                case 3:
                    Center = new CoordinatesElement(parameter, this);
                    return Center;
                default:
                    return base.CreateChildForParameter(parameter);
            }
        }
    }
}