using System.Drawing;
using WatchFace.Parser.Interfaces;
using static WatchFace.Parser.PreviewGenerator;

namespace WatchFace.Parser.Models.Elements
{
    public class ImageElement : CoordinatesElement, IDrawable
    {
        public ImageElement(Parameter parameter, Element parent, string name = null) : base(parameter, parent, name) { }

        public long ImageIndex { get; set; }

        public virtual void Draw(Graphics drawer, Bitmap[] resources, WatchState state)
        {
            Draw(drawer, resources);
        }

        public void Draw(Graphics drawer, Bitmap[] images)
        {
            drawer.DrawImage(images[ImageIndex], new Point((int) X, (int) Y));
        }

        public void DrawClockHand(Graphics drawer, Bitmap[] images, float angle, Point centerOffset)
        {
            drawer.TranslateTransform(CenterOffset.X + centerOffset.X, CenterOffset.Y + centerOffset.Y);
            drawer.RotateTransform(angle);
            //drawer.DrawImage(images[ImageIndex], new Point((int)X, (int)Y));
            drawer.DrawImage(images[ImageIndex], new Rectangle(-(int)X, -(int)Y,
                images[ImageIndex].Width, images[ImageIndex].Height));
            drawer.RotateTransform(-angle);
            drawer.TranslateTransform(-CenterOffset.X - centerOffset.X, -CenterOffset.Y - centerOffset.Y);
        }

        protected override Element CreateChildForParameter(Parameter parameter)
        {
            switch (parameter.Id)
            {
                case 3:
                    ImageIndex = parameter.Value;
                    return new ValueElement(parameter, this, nameof(ImageIndex));
                default:
                    return base.CreateChildForParameter(parameter);
            }
        }
    }
}