using bbxp.lib.HttpHandlers.Base;

namespace bbxp.lib.HttpHandlers
{
    public class AccountHttpHandler : BaseHttpHandler
    {
        public AccountHttpHandler(string baseAddress, string? token = null) : base(baseAddress, token) { }

        public async Task<string> LoginAsync(string username, string password) => await GetAsync<string>($"account?username={username}&password={password}");
    }
}