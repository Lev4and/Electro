using Microsoft.AspNetCore.Identity;
using System;

namespace Electro.Model.Database.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual Client Client { get; set; }
    }
}
