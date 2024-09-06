public interface IAuthenticationService
{
    Task<UserModel?> AuthenticateUserAsync(string username, string password);
    Task<string> HashPasswordAsync(string password);
    Task<bool> VerifyPasswordAsync(string password, string hashedPassword);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModel?> AuthenticateUserAsync(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user != null && await VerifyPasswordAsync(password, user.PasswordHash))
        {
            return user;
        }
        return null;
    }

    public async Task<string> HashPasswordAsync(string password)
    {
        //WorkInProgress
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public async Task<bool> VerifyPasswordAsync(string password, string hashedPassword)
    {
        //WorkInProgress
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}