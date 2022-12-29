using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilters filters, string actionUrl);
    }
}