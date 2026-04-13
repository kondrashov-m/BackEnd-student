namespace LoggingDemo.Services;

public interface IUserService
{
    void CreateUser(string name);
}

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;

    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }

    public void CreateUser(string name)
    {
        _logger.LogInformation("Создание пользователя: {UserName}", name);
        // здесь могла бы быть логика создания пользователя
        _logger.LogInformation("Пользователь {UserName} успешно создан", name);
    }
}
