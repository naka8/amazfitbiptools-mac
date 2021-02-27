using System;
using WatchFace.Parser.Attributes;

namespace WatchFace.Parser.Elements.DateElements
{
    public class Year
    {
        [ParameterId(1)]
        public OneLineYear Separate { get; set; }

    }
}
