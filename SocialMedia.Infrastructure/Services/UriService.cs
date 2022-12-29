using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetPostPaginationUri(PostQueryFilters filters, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
