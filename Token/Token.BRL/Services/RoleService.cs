using Token.BRL.Interfaces;
using Token.DAL.Entities;
using Token.DAL.Repositories;

namespace Token.BRL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<AspnetRoles> _roleRepository;

        public RoleService(IRepository<AspnetRoles> roleRepository)
        {
            _roleRepository = roleRepository;
        }
    }
}