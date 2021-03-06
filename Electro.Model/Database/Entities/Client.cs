using System;

namespace Electro.Model.Database.Entities
{
    public class Client
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
