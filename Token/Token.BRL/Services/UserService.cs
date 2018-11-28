using System.Threading.Tasks;
using Token.BRL.Interfaces;
using Token.BRL.Model;
using Token.DAL.Entities;
using Token.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Token.BRL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<AspnetUsers> _userRepository;

        public UserService(IRepository<AspnetUsers> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUser(string login)
        {

//            var result = await _userRepository.GetAll()
//                .Include(y => y.AspnetUsersInRoles)

            //var result = await _userRepository.GetAll()
            //    .Include(y => y.SystemUserRole)
            //    .ThenInclude(z => z.RoleCodeNavigation)
            //    .Where(x => x.LoginId == login)
            //    .Select(a =>
            //        new User()
            //        {
            //            Guid = a.UserGuid,
            //            Login = a.LoginId,
            //            Name = a.FirstName + " " + a.LastName,
            //            Email = a.EmailAddr,
            //            Roles = a.SystemUserRole.Select(x => new Role()
            //            {
            //                Name = x.RoleCode,
            //                Category = x.RoleCodeNavigation.Category,
            //                Description = x.RoleCodeNavigation.RoleDesc
            //            })
            //        })
            //    .FirstOrDefaultAsync();

            return null;
        }

    }
}