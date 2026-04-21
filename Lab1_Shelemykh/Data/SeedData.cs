using Lab1_Shelemykh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh.Data;

public static class SeedData
{
    public static List<Book> GetBooks()
    {
        return new List<Book>
        {
            new() { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Genre = "Software", Price = 42.50m, StockQuantity = 8 },
            new() { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Genre = "Software", Price = 39.99m, StockQuantity = 7 },
            new() { Id = 3, Title = "Refactoring", Author = "Martin Fowler", Genre = "Software", Price = 47.25m, StockQuantity = 5 },
            new() { Id = 4, Title = "Domain-Driven Design", Author = "Eric Evans", Genre = "Architecture", Price = 54.90m, StockQuantity = 4 },
            new() { Id = 5, Title = "Head First Design Patterns", Author = "Eric Freeman", Genre = "Software", Price = 48.00m, StockQuantity = 6 },
            new() { Id = 6, Title = "C# in Depth", Author = "Jon Skeet", Genre = "Programming", Price = 44.40m, StockQuantity = 9 },
            new() { Id = 7, Title = "ASP.NET Core in Action", Author = "Andrew Lock", Genre = "Web", Price = 46.75m, StockQuantity = 5 },
            new() { Id = 8, Title = "Introduction to Algorithms", Author = "Thomas H. Cormen", Genre = "Computer Science", Price = 69.00m, StockQuantity = 3 }
        };
    }

    public static List<Customer> GetCustomers()
    {
        return new List<Customer>
        {
            new() { Id = 1, FullName = "Olena Kovalenko", Email = "olena@example.com", City = "Kyiv", IsActive = true },
            new() { Id = 2, FullName = "Andrii Melnyk", Email = "andrii@example.com", City = "Lviv", IsActive = true },
            new() { Id = 3, FullName = "Maksym Shevchenko", Email = "maksym@example.com", City = "Odesa", IsActive = true },
            new() { Id = 4, FullName = "Iryna Bondarenko", Email = "iryna@example.com", City = "Dnipro", IsActive = true }
        };
    }

    public static List<Order> GetOrders()
    {
        return new List<Order>
        {
            new()
            {
                Id = 1,
                CustomerId = 1,
                CreatedAt = new DateTime(2026, 4, 10, 12, 30, 0, DateTimeKind.Utc),
                Items = new List<OrderItem>
                {
                    new() { BookId = 1, BookTitle = "Clean Code", UnitPrice = 42.50m, Quantity = 1, LineTotal = 42.50m },
                    new() { BookId = 5, BookTitle = "Head First Design Patterns", UnitPrice = 48.00m, Quantity = 1, LineTotal = 48.00m }
                },
                TotalAmount = 90.50m
            },
            new()
            {
                Id = 2,
                CustomerId = 2,
                CreatedAt = new DateTime(2026, 4, 12, 9, 15, 0, DateTimeKind.Utc),
                Items = new List<OrderItem>
                {
                    new() { BookId = 2, BookTitle = "The Pragmatic Programmer", UnitPrice = 39.99m, Quantity = 1, LineTotal = 39.99m },
                    new() { BookId = 6, BookTitle = "C# in Depth", UnitPrice = 44.40m, Quantity = 1, LineTotal = 44.40m },
                    new() { BookId = 7, BookTitle = "ASP.NET Core in Action", UnitPrice = 46.75m, Quantity = 1, LineTotal = 46.75m }
                },
                TotalAmount = 131.14m
            },
            new()
            {
                Id = 3,
                CustomerId = 3,
                CreatedAt = new DateTime(2026, 4, 14, 16, 45, 0, DateTimeKind.Utc),
                Items = new List<OrderItem>
                {
                    new() { BookId = 3, BookTitle = "Refactoring", UnitPrice = 47.25m, Quantity = 1, LineTotal = 47.25m },
                    new() { BookId = 4, BookTitle = "Domain-Driven Design", UnitPrice = 54.90m, Quantity = 1, LineTotal = 54.90m }
                },
                TotalAmount = 102.15m
            }
        };
    }
}