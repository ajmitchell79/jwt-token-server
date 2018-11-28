using System.Threading.Tasks;
using Token.BRL.Model;

namespace Token.BRL.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(string login);
    }
}