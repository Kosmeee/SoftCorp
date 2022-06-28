namespace Service.Models
{
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Response")]
    public class Response
    {
        public bool Success { get; set; }
        public int ErrorId { get; set; }
        public string ErrorMsg { get; set; }
        public User User { get; set; }

    }
}
