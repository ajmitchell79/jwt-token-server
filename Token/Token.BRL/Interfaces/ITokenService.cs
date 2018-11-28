using System.Threading.Tasks;
using Token.BRL.Model.TokenServer.BRL.Model;

namespace Token.BRL.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateAccessToken(string fullUsername);
    }
}