using Lab1_Shelemykh.Contracts;
using Lab1_Shelemykh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh.Repositories;

public class InMemoryStoreRepository : IBookRepository, ICustomerRepository, IOrderRepository
{
    private readonly List<Book> _books;
    private readonly List<Customer> _customers;
    private readonly List<Order> _orders;

    public InMemoryStoreRepository(
        List<Book> books,
        List<Customer> customers,
        List<Order> orders)
    {
        _books = books ?? throw new ArgumentNullException(nameof(books));
        _customers = customers ?? throw new ArgumentNullException(nameof(customers));
        _orders = orders ?? throw new ArgumentNullException(nameof(orders));
    }

    public IReadOnlyCollection<Book> GetAll()
    {
        return _books.AsReadOnly();
    }

    IReadOnlyCollection<Customer> ICustomerRepository.GetAll()
    {
        return _customers.AsReadOnly();
    }

    IReadOnlyCollection<Order> IOrderRepository.GetAll()
    {
        return _orders.AsReadOnly();
    }

    public Book? GetById(int id)
    {
        return _books.FirstOrDefault(x => x.Id == id);
    }

    Customer? ICustomerRepository.GetById(int id)
    {
        return _customers.FirstOrDefault(x => x.Id == id);
    }

    public void Update(Book book)
    {
        var index = _books.FindIndex(x => x.Id == book.Id);

        if (index < 0)
        {
            throw new InvalidOperationException($"Book with id {book.Id} was not found.");
        }

        _books[index] = book;
    }

    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public int GetNextId()
    {
        return _orders.Count == 0 ? 1 : _orders.Max(x => x.Id) + 1;
    }
}