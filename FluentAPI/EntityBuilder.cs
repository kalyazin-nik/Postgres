using Entities;
using FluentAPI.Configurations;
using FluentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FluentAPI;

/// <summary>
/// Класс формирования сущности для базы данных.
/// </summary>
/// <typeparam name="TEntity">Возможно использование данного класса только базовой сущностью (BaseEntity) и её наследниками,
/// определенными в рядом находящемся проекте Entities.</typeparam>
public static class EntityBuilder<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Словарь таблиц. Ключ - имя класса/сущности. Значение - то, как данная сущность, будет выглядеть в базе данных.
    /// </summary>
    private static Dictionary<string, Table> Tables { get => TableConfiguration.Tables; }

    /// <summary>
    /// Словарь колонок. Ключ - имя свойства/колонки. Значение - то, какой будет данная колонка в таблице базы данных.
    /// </summary>
    private static Dictionary<string, Column> Columns { get => ColumnConfiguration.Columns; }

    /// <summary>
    /// Изменение сущности согласно конфигурации.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели нотации FluentAPI.</param>
    public static void ChangeEntity(ModelBuilder modelBuilder)
    {
        ChangeTable(modelBuilder);
        ChangeColumns(modelBuilder);
    }

    /// <summary>
    /// Изменеие таблицы.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели нотации FluentAPI.</param>
    private static void ChangeTable(ModelBuilder modelBuilder)
    {
        if (Tables.TryGetValue(typeof(TEntity).Name, out var table))
        {
            modelBuilder.Entity<TEntity>().ToTable(table.Name, schema: table.Schema);

            if (table.Constraints is not null)
                foreach (var constraint in table.Constraints)
                    modelBuilder.Entity<TEntity>().ToTable(t => t.HasCheckConstraint(constraint.Name, constraint.Sql));
        }
    }

    /// <summary>
    /// Изменение колонок.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели нотации FluentAPI.</param>
    private static void ChangeColumns(ModelBuilder modelBuilder)
    {
        foreach (var property in typeof(TEntity).GetProperties())
        {
            var propertyName = property.Name;

            if (Columns.TryGetValue(propertyName, out var column))
            {
                modelBuilder.Entity<TEntity>().Property(propertyName).HasColumnName(column.Name);

                if (column.Type is not null)
                    modelBuilder.Entity<TEntity>().Property(propertyName).HasColumnType(column.Type);

                if (column.DefaultValueSql is not null)
                    modelBuilder.Entity<TEntity>().Property(propertyName).HasDefaultValueSql(column.DefaultValueSql);

                if (column.IsValueGeneratedNever)
                {
                    modelBuilder.Entity<TEntity>().Property(propertyName).ValueGeneratedNever();
                    modelBuilder.Entity<TEntity>().HasKey(propertyName);
                    modelBuilder.Entity<TEntity>().HasIndex(propertyName);
                }
            }
        }
    }
}
