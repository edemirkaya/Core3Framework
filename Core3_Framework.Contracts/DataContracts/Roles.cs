using System.Collections.Generic;

namespace Core3_Framework.Contracts.DataContracts
{
    public class Roles
    {
        public Roles()
        {
            UserRole = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRoles> UserRole { get; set; }
    }
}
