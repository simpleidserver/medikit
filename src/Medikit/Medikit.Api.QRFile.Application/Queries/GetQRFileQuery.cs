using MediatR;

namespace Medikit.Api.QRFile.Application.Queries
{
    public class GetQRFileQuery : IRequest<string>
    {
        public string FileId { get; set; }
    }
}
