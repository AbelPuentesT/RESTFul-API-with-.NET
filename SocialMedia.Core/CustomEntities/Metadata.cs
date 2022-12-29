using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.CustomEntities
{
    public class Metadata
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage {get; set; }
        public bool HasNextPage {get; set; }
        public string NextPageURL { get; set; }
        public string PreviousPageURL { get; set; }
    }
}
