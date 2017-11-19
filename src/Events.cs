using System.Collections.Generic;
using System.Xml.Serialization;

namespace src
{
[XmlRoot(ElementName = "events")]
    public class Events
    {
        [XmlElement(ElementName = "location")]
        public Location Location { get; set; }
        [XmlElement(ElementName = "event")]
        public List<Event> Event { get; set; }
    }
}
