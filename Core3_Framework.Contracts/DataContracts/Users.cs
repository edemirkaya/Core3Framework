using System.Collections.Generic;

namespace Core3_Framework.Contracts.DataContracts
{
    public class Users
    {
        public Users()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
