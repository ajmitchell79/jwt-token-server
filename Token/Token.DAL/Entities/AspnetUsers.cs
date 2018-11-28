using System;
using System.Collections.Generic;

namespace Token.DAL.Entities
{
    public partial class AspnetUsers
    {
        public AspnetUsers()
        {
            AspnetUsersInRoles = new HashSet<AspnetUsersInRoles>();
        }

        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }

        public ICollection<AspnetUsersInRoles> AspnetUsersInRoles { get; set; }
    }
}
