namespace Database;

/// <summary>
/// Информация о таблице в базе данных.
/// </summary>
/// <param name="name">Имя таблицы используемое в базе данных.</param>
/// <param name="schema">Имя схемы в которой будет находиться таблица. 
/// Если аргумент схемы null, то будет использоваться по умолчанию - public.</param>
/// <param name="constraints">Ограничения накладываемые на таблицу. Если аргумент ограничений null, то ограничения не будут наложены. 
/// Ключом указывается имя ограничения, значением - описание в формате SQL запроса.</param>
internal readonly struct TableInfo(string name, string schema = null!, Dictionary<string, string>? constraints = null!)
{
    /// <summary>
    /// Имя таблицы используемое в базе данных.
    /// </summary>
    internal readonly string Name = name;

    /// <summary>
    /// Имя схемы в которой будет находиться таблица. Если аргумент схемы null, то будет использоваться по умолчанию - public.
    /// </summary>
    internal readonly string? Schema = schema;

    /// <summary>
    /// Ограничения накладываемые на таблицу. Если аргумент ограничений null, то ограничения не будут наложены.
    /// Ключом указывается имя ограничения, значением - описание в формате SQL запроса.
    /// </summary>
    internal readonly Dictionary<string, string>? Constrants = constraints;
}
