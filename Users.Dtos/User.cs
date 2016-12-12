using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Users.Dtos
{
    [DataContract]
    public class User
    {
        [DataMember]    
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public virtual Role Role { get; set; }
    }
}
