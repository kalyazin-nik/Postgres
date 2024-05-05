using Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

/// <summary>
/// Класс изменеия сущности для базы данных.
/// </summary>
/// <typeparam name="TEntity">Возможно использование данного класса только базовой сущностью (BaseEntity) и её наследниками,
/// определенными в, рядом лежащем, проекте Entities.</typeparam>
internal static class ConfigurationService<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Словарь таблиц. Ключ - имя класса/сущности. Значение - то, как данная сущность, будет именоваться в базе данных.
    /// </summary>
    private readonly static Dictionary<string, TableInfo> _tables = new()
    {
        { "User", new TableInfo("users", "telegram_bot") }
    };

    /// <summary>
    /// Словарь колонок. Указываются колонки для всех таблиц базы данных. 
    /// Ключ - имя свойства/колонки. Значение - то, какой будет данная колонка в таблице базы данных.
    /// </summary>
    private readonly static Dictionary<string, ColumnInfo> _columns = new()
    {
        { "ChatID", new ColumnInfo("chat_id", "bigint", isValueGeneratedNever: true) },
        { "Username", new ColumnInfo("username", "varchar(30)") },
        { "FirstName", new ColumnInfo("first_name", "varchar(30)") },
        { "LastName", new ColumnInfo("last_name", "varchar(30)") }
    };

    /// <summary>
    /// Асинхронный метод изменения сущности согласно конфигурации.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели FluentAPI.</param>
    /// <returns>Успешное выполнение задачи.</returns>
    internal static async Task ChangeEntityAsync(ModelBuilder modelBuilder)
    {
        await ChangeTableAsync(modelBuilder);
        await ChangeColumnAsync(modelBuilder);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Изменение сущности согласно конфигурации.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели FluentAPI.</param>
    internal static void ChangeEntity(ModelBuilder modelBuilder)
    {
        ChangeTable(modelBuilder);
        ChangeColumn(modelBuilder);
    }

    /// <summary>
    /// Асинхронный метод изменения таблицы.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели FluentAPI.</param>
    /// <returns>Успешное выполнение задачи.</returns>
    private static async Task ChangeTableAsync(ModelBuilder modelBuilder)
    {
        await Task.Run(() => ChangeTable(modelBuilder));
        await Task.CompletedTask;
    }

    /// <summary>
    /// Асинхронный метод изменения колонок.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели FluentAPI.</param>
    /// <returns>Успешное выполнение задачи.</returns>
    private static async Task ChangeColumnAsync(ModelBuilder modelBuilder)
    {
        await Task.Run(() => ChangeColumn(modelBuilder));
        await Task.CompletedTask;
    }

    /// <summary>
    /// Изменеие таблицы.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели FluentAPI.</param>
    private static void ChangeTable(ModelBuilder modelBuilder)
    {
        if (_tables.TryGetValue(typeof(TEntity).Name, out var tableInfo))
        {
            modelBuilder.Entity<TEntity>().ToTable(tableInfo.Name, schema: tableInfo.Schema);

            if (tableInfo.Constrants is not null)
            {
                foreach (var constraint in tableInfo.Constrants)
                {
                    modelBuilder.Entity<TEntity>().ToTable(t => t.HasCheckConstraint(constraint.Key, constraint.Value));
                }
            }
        }
    }

    /// <summary>
    /// Изменение колонок.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели FluentAPI.</param>
    private static void ChangeColumn(ModelBuilder modelBuilder)
    {
        foreach (var property in typeof(TEntity).GetProperties())
        {
            var propertyName = property.Name;

            if (_columns.TryGetValue(propertyName, out var columnInfo))
            {
                modelBuilder.Entity<TEntity>().Property(propertyName).HasColumnName(columnInfo.Name);

                if (columnInfo.Type is not null)
                {
                    modelBuilder.Entity<TEntity>().Property(propertyName).HasColumnType(columnInfo.Type);
                }

                if (columnInfo.DefaultValue is not null)
                {
                    modelBuilder.Entity<TEntity>().Property(propertyName).HasDefaultValueSql(columnInfo.DefaultValue);
                }

                if (columnInfo.IsValueGeneratedNever)
                {
                    modelBuilder.Entity<TEntity>().Property(propertyName).ValueGeneratedNever();
                    modelBuilder.Entity<TEntity>().HasKey(propertyName);
                }
            }
        }
    }
}
