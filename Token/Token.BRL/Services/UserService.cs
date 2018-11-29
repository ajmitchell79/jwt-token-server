using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Token.BRL.Interfaces;
using Token.BRL.Model;
//using Token.DAL.Entities;
using Token.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Token.DAL.Entities;

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

            var result = await _userRepository.GetAll()
                .Include(y => y.AspnetUsersInRoles)
                .ThenInclude(z=> z.Role)
                .Where(x=>x.UserName == login)
                .Select(a =>
                    new User()
                    {
                        Login = a.UserName,
                        Email = a.UserName + "@test.com",
                        Guid = a.UserId,
                        Name = a.MobileAlias,
                        Roles = a.AspnetUsersInRoles.Select(x=> new Role()
                            {
                                Id = x.Role.RoleId.ToString(),
                                Name = x.Role.RoleName,
                                Description = x.Role.Description
                            })
                    })
                .FirstOrDefaultAsync();
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

            return result;

        }

    }
}