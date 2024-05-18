using FluentAPI.Models;

namespace FluentAPI.Configurations;

/// <summary>
/// Конфигурация колонок.
/// </summary>
internal static class ColumnConfiguration
{
    /// <summary>
    /// Словарь колонок. Указываются колонки для всех таблиц базы данных. 
    /// Ключ - имя свойства/колонки. Значение - то, какой будет данная колонка в таблице базы данных.
    /// </summary>
    private static readonly Dictionary<string, Column> _columns = new()
    {
        { "ChatID", new Column("chat_id", "bigint", isValueGeneratedNever: true) },
        { "Username", new Column("username", "varchar(30)") },
        { "FirstName", new Column("first_name", "varchar(30)") },
        { "LastName", new Column("last_name", "varchar(30)") }
    };

    /// <summary>
    /// Словарь колонок. Ключ - имя свойства/колонки. Значение - то, какой будет данная колонка в таблице базы данных.
    /// </summary>
    internal static Dictionary<string, Column> Columns { get => _columns; }
}
