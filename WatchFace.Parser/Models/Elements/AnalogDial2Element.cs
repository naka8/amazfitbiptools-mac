using WatchFace.Parser.Models.Elements.AnalogDial;
using WatchFace.Parser.Models.Elements.AnalogDial2;

namespace WatchFace.Parser.Models.Elements
{
    public class AnalogDial2Element : ContainerElement
    {
        public AnalogDial2Element(Parameter parameter, Element parent = null, string name = null) :
            base(parameter, parent, name) { }

        public HoursClockHand2Element Hours { get; set; }
        public MinutesClockHand2Element Minutes { get; set; }
        public SecondsClockHand2Element Seconds { get; set; }
 
        protected override Element CreateChildForParameter(Parameter parameter)
        {
            switch (parameter.Id)
            {
                case 3:
                    Hours = new HoursClockHand2Element(parameter, this, nameof(Hours));
                    return Hours;
                case 4:
                    Minutes = new MinutesClockHand2Element(parameter, this, nameof(Minutes));
                    return Minutes;
                case 5:
                    Seconds = new SecondsClockHand2Element(parameter, this, nameof(Seconds));
                    return Seconds;
                default:
                    return base.CreateChildForParameter(parameter);
            }
        }
    }
}