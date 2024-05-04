using Entities;
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
    /// <param name="modelBuilder">Конструктор модели FluentAPI.</param>
    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        await ConfigurationService<User>.ChangeEntityAsync(modelBuilder);
    }
}
