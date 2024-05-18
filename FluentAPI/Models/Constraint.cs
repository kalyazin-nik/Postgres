namespace FluentAPI.Models;

/// <summary>
/// Ограничения накладываемые на таблицу в базе данных.
/// </summary>
/// <param name="name">Название ограничения.</param>
/// <param name="sql">Ограничение в формате SQL.</param>
internal readonly struct Constraint(string name, string sql)
{
    /// <summary>
    /// Название ограничения.
    /// </summary>
    internal readonly string Name = name;

    /// <summary>
    /// Ограничение в формате SQL.
    /// </summary>
    internal readonly string Sql = sql;
}
