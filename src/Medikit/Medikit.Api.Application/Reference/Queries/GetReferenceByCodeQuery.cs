namespace Medikit.Api.Application.Reference.Queries
{
    public class GetReferenceByCodeQuery
    {
        public GetReferenceByCodeQuery(string code, string language = null)
        {
            Code = code;
            Language = language;
        }

        public string Code { get; set; }
        public string Language { get; set; }
    }
}
