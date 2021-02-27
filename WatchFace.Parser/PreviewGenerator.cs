using System.Collections.Generic;
using System.Drawing;
using WatchFace.Parser.Interfaces;
using WatchFace.Parser.Models;

namespace WatchFace.Parser
{
    public class PreviewGenerator
    {
        public static class CenterOffset
        {
            public static int X = 354/2;
            public static int Y = 306/2;
        }

        public static IEnumerable<Image> CreateAnimation(List<Parameter> descriptor, Bitmap[] images,
            IEnumerable<WatchState> states)
        {
            var previewWatchFace = new Models.Elements.WatchFace(descriptor);
            foreach (var watchState in states)
            {
                using (var image = CreateFrame(previewWatchFace, images, watchState))
                {
                    yield return image;
                }
            }
        }

        public static Image CreateImage(IEnumerable<Parameter> descriptor, Bitmap[] images, WatchState state)
        {
            var previewWatchFace = new Models.Elements.WatchFace(descriptor);
            return CreateFrame(previewWatchFace, images, state);
        }

        private static Image CreateFrame(IDrawable watchFace, Bitmap[] resources, WatchState state)
        {
            var preview = new Bitmap(306, 354);
            var graphics = Graphics.FromImage(preview);
            watchFace.Draw(graphics, resources, state);
            return preview;
        }
    }
}