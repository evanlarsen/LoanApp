using System.Threading.Tasks;

namespace Merchant.Account
{
    public interface IAccountService
    {
        Task<bool> IsEmailUnique(string email);
    }
}
