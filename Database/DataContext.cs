using Entities;
using FluentAPI;
using Microsoft.EntityFrameworkCore;

namespace Database;

/// <summary>
/// Класс контекста базы данных.
/// </summary>
/// <param name="options">Опция подключения к базе данных.</param>
public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица базы данных.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    /// Настройка модели из типа сущности.
    /// </summary>
    /// <param name="modelBuilder">Конструктор модели нотации FluentAPI.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EntityBuilder<User>.ChangeEntity(modelBuilder);
    }
}
