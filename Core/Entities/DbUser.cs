﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DbUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public DbRole Role { get; set; }
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
