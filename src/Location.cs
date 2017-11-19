using System.Xml.Serialization;

namespace src
{
    [XmlRoot(ElementName = "location")]
    public class Location
    {
        [XmlElement(ElementName = "city")]
        public string City { get; set; }
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
    }
}