using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveOfDocuments.DataAccess.Entities
{
    [Table("AccessLogs")]
    public class AccessLogEntity:BaseEntity
    {
        public int IDUser { get; set; }
        public UserEntity User { get; set; }

        public int IDDocument { get; set; }
        public DocumentEntity Document { get; set; }
        public DateTime AccessDate { get; set; }
        public string ActionType { get; set; }
    }
}
