using System;
using System.Collections.Generic;

namespace Token.BRL.Model
{

        public class User
        {
            public string Login { get; set; }           //NameIdentifier

            public Guid Guid { get; set; }          //WindowsUserClaim

            public string Email { get; set; }  //email

            public string Name { get; set; }        //name

            public IEnumerable<Role> Roles { get; set; }
        }

        public class Role
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public string Category { get; set; }
        }
}