using System;
using System.Xml.Serialization;

namespace src
{
    [XmlRoot(ElementName = "event")]
    public class Event
    {
        [XmlElement(ElementName = "date")]
        public string DateAsString { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "type_code")]
        public string Type_code { get; set; }
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        [XmlIgnore]
        public DateTime Date
        {
            get
            {
                return DateTime.Parse(DateAsString);
            }
            set { DateAsString = value.ToShortDateString(); }
        }
    }
}