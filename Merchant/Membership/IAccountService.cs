using System.Threading.Tasks;

namespace Merchant.Membership
{
    public interface IAccountService
    {
        Task<bool> IsEmailAlreadyTaken(string email);
    }
}
