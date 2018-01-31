using System.Xml.Serialization;

namespace Medikit.EHealth.Services.DICS.Response
{
    public class DICSAtc
    {
        [XmlAttribute(AttributeName = "Code")]
        public string Code { get; set; }
        [XmlText]
        public string Content { get; set; }
    }
}
