using System.Xml.Serialization;
using Service.EnumHelpers;

namespace Service.Models

{
    public enum StatusEnum
    {
        [XmlEnum]
        New = 1,
        [XmlEnum]
        Active = 2,
        [XmlEnum]
        Blocked = 3,
        [XmlEnum]
        Deleted = 4
    }

    public class Status : EnumBase<StatusEnum>
    {

    }
}
