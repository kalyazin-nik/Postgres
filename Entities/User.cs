namespace Entities;

/// <summary>
/// Сущность пользователя телеграм бота. Потомок абстрактного класса BaseEntity. Наследование запрещено.
/// </summary>
/// <param name="chatID">Уникальный идентификатор чата пользователя.</param>
/// <param name="username">Прозвище пользователя.</param>
/// <param name="firstName">Имя пользователя.</param>
/// <param name="lastName">Фамилия пользователя.</param>
public sealed class User(long chatID, string username, string firstName, string lastName) : BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор чата пользователя.
    /// </summary>
    public long ChatID { get; set; } = chatID;

    /// <summary>
    /// Прозвище пользователя.
    /// </summary>
    public string Username { get; set; } = username;

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; } = firstName;

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; } = lastName;
}
