using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiveOfDocuments.WebAPI.Settings
{
    public class ArchiveOfDocumentsSettings
    {
        public Uri ServiceUri { get; set; }
        public string ArchiveOfDocumentsDbContextConnectionString { get; set; }
        public string IdentityServerUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
