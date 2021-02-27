using Resources.Models;
using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.AnalogDialFaceElements;

namespace WatchFace.Parser.Elements
{
    public class AnalogDialFace2
    {
        [ParameterId(3)]
        public ClockHand2 Hours { get; set; }

        [ParameterId(4)]
        public ClockHand2 Minutes { get; set; }

        [ParameterId(5)]
        public ClockHand2 Seconds { get; set; }
    }
}