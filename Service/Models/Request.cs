using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Service.Models
{
    public class Request
    {
        [XmlElement(ElementName = "user")]
        public User User { get; set; }
    }
}
