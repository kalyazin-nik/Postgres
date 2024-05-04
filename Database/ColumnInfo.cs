namespace Database;

/// <summary>
/// Информация о колонке в таблице базы данных.
/// </summary>
/// <param name="name">Имя колонки используемое в таблице.</param>
/// <param name="type">Тип данных для колонки. Если аргумент типа данных null, то будет использоваться тип данных 
/// определенный по типу данных свойства сущности.</param>
/// <param name="defaultValue">Значение по умолчанию. Если аргумент значения по умолчанию null, 
/// то значение по умолчанию использоваться не будет.</param>
/// <param name="isValueGeneratedNever">Параметр логического типа отвечающий на вопрос - Значение никогда не генерируется? 
/// По умолчанию false. Если true - то отменяет генерацию значения по умолчанию для PrimaryKey и устанавливает текущую колонку в качестве PrimaryKey. 
/// В таком случае, уникальность главного ключа таблицы, нужно обеспечить самостоятельно.</param>
internal readonly struct ColumnInfo(string name, string? type = null, string? defaultValue = null, bool isValueGeneratedNever = false)
{
    /// <summary>
    /// Имя колонки используемое в таблице.
    /// </summary>
    internal readonly string Name = name;

    /// <summary>
    /// Тип данных для колонки. Если аргумент типа данных null, то будет использоваться тип данных определенный по типу данных свойства сущности.
    /// </summary>
    internal readonly string? Type = type;

    /// <summary>
    /// Значение по умолчанию. Если аргумент значения по умолчанию null, то значение по умолчанию использоваться не будет.
    /// </summary>
    internal readonly string? DefaultValue = defaultValue;

    /// <summary>
    /// Параметр логического типа отвечающий на вопрос - Значение никогда не генерируется? По умолчанию false. Если true - 
    /// то отменяет генерацию значения по умолчанию для PrimaryKey и устанавливает текущую колонку в качестве PrimaryKey. 
    /// В таком случае, уникальность главного ключа таблицы, нужно обеспечить самостоятельно.
    /// </summary>
    internal readonly bool IsValueGeneratedNever = isValueGeneratedNever;
}
