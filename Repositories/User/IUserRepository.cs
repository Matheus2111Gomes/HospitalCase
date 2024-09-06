using Microsoft.Data.SqlClient;

public interface IUserRepository
{
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task AddUserAsync(UserModel user);
}

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        var query = "SELECT Id, Username, PasswordHash, Role, ForeignId FROM Users WHERE Username = @Username";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserModel
                        {
                            Id = reader.GetGuid(0),
                            Username = reader.GetString(1),
                            PasswordHash = reader.GetString(2),
                            Role = reader.GetString(3),
                            ForeignId = reader.IsDBNull(4) ? (Guid?)null : reader.GetGuid(4)
                        };
                    }
                }
            }
        }

        return null;
    }

    public async Task AddUserAsync(UserModel user)
    {
        var query = "INSERT INTO Users (Id, Username, PasswordHash, Role, ForeignId) VALUES (@Id, @Username, @PasswordHash, @Role, @ForeignId)";

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@ForeignId", (object?)user.ForeignId ?? DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}