using System.Collections.Generic;
using Newtonsoft.Json;
using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;
using WatchFace.Parser.JsonConverters;

namespace WatchFace.Parser.Elements.AnalogDialFaceElements
{
    public class ClockHand2
    {
        [ParameterId(1)]
        [ParameterImageIndex]
        public long ImageIndex { get; set; }

        [JsonConverter(typeof(ColorJsonConverter))]
        [ParameterId(2)]
        public long Color { get; set; }

        [ParameterId(3)]
        public Coordinates Center { get; set; }

    }
}