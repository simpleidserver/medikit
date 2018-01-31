using System.Xml.Serialization;

namespace Medikit.EHealth.Services.Recipe.Response
{
    public class RecipeStatusResponse
    {
        [XmlElement(ElementName = "code")]
        public int Code { get; set; }
    }
}
