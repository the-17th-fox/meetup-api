using Core.ViewModels.Users;

namespace Core.Interfaces.Services
{
    public interface IUsersService
    {
        public Task<TokensViewModel> LoginAsync(LoginViewModel loginViewModel);
        public Task LogoutAsync(Guid id);
    }
}
