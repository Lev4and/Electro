using Electro.Model.Database.Repositories.Abstract;

namespace Electro.Model.Database
{
    public class DataManager
    {
        public IRolesRepository Roles { get; private set; }

        public IUsersRepository Users { get; private set; }

        public DataManager(IRolesRepository roles, IUsersRepository users)
        {
            Roles = roles;
            Users = users;
        }
    }
}
