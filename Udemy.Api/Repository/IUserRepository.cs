using Udemy.Api.Data.VO;
using Udemy.Api.Models;

namespace Udemy.Api.Repository
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);
        User ValidateCredentials(string username);
        bool RevokeToken(string username);
        User RefreshUserInfo(User user);
    }
}
