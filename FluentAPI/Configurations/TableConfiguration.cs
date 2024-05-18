using FluentAPI.Models;

namespace FluentAPI.Configurations;

/// <summary>
/// Конфигураци таблиц.
/// </summary>
internal static class TableConfiguration
{
    /// <summary>
    /// Словарь таблиц. Ключ - имя класса/сущности. Значение - то, как данная сущность, будет выглядеть в базе данных.
    /// </summary>
    private static readonly Dictionary<string, Table> _tables = new()
    {
        { "User", new Table("users", "telegram_bot") }
    };

    /// <summary>
    /// Словарь таблиц. Ключ - имя класса/сущности. Значение - то, как данная сущность, будет выглядеть в базе данных.
    /// </summary>
    internal static Dictionary<string, Table> Tables { get => _tables; }
}
