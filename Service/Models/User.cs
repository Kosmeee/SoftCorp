using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Service.Models
{
    public class User
    {

        [XmlAttribute("Id")]
        public int Id {  get; set; }
        [XmlAttribute("Name")]
        public string? Name { get; set; }
        public StatusEnum Status
        {
            get => (StatusEnum)_statusId;
            set => _statusId = (int)value;
        }
        private int _statusId;

    }
}
