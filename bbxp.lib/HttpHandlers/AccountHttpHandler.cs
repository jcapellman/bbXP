using bbxp.lib.HttpHandlers.Base;
using bbxp.lib.JSON;

namespace bbxp.lib.HttpHandlers
{
    public class AccountHttpHandler : BaseHttpHandler
    {
        public AccountHttpHandler(string baseAddress, string? token = null) : base(baseAddress, token) { }

        public async Task<string> LoginAsync(UserLoginRequestItem userLogin) => await PostReturnStringAsync("account", userLogin);
    }
}