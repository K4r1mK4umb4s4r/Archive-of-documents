using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveOfDocuments.DataAccess.Entities
{
    [Table("documents")]
    public class DocumentEntity:BaseEntity
    {
        public int IDUser { get; set; }
        public UserEntity User { get; set; }
        public string Title { get; set; }
        public DateTime AddDate { get; set; }
        public string Content { get; set; }
        public ICollection<AccessLogEntity> AccessLogs { get; set; }
    }
}
