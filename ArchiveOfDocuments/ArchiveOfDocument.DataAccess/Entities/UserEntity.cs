using ArchiveOfDocuments.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveOfDocuments.DataAccess.Entities
{
    [Table("users")]

    public class UserEntity : IdentityUser<int>, IBaseEntity
    {
        public Guid ExternalId { get; set; }
        public DateTime ModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
        public string Login { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<DocumentEntity> Documents { get; set; }
        public ICollection<AccessLogEntity> AccessLogs { get; set; }
        public class UserRoleEntity : IdentityRole<int> { }

    }
}
