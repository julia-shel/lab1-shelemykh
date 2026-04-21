using Lab1_Shelemykh.Contracts;
using Lab1_Shelemykh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh.Services;

public class OrderService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderService(
        IBookRepository bookRepository,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository)
    {
        _bookRepository = bookRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    public Order CreateOrder(int customerId, IDictionary<int, int> bookQuantities)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer is null)
        {
            throw new InvalidOperationException($"Customer with id {customerId} was not found.");
        }

        if (!customer.IsActive)
        {
            throw new InvalidOperationException($"Customer with id {customerId} is inactive.");
        }

        if (bookQuantities.Count == 0)
        {
            throw new InvalidOperationException("Order must contain at least one item.");
        }

        var orderItems = new List<OrderItem>();

        foreach (var pair in bookQuantities)
        {
            var bookId = pair.Key;
            var quantity = pair.Value;

            if (quantity <= 0)
            {
                throw new InvalidOperationException($"Quantity for book id {bookId} must be greater than zero.");
            }

            var book = _bookRepository.GetById(bookId);
            if (book is null)
            {
                throw new InvalidOperationException($"Book with id {bookId} was not found.");
            }

            if (book.StockQuantity < quantity)
            {
                throw new InvalidOperationException(
                    $"Not enough stock for '{book.Title}'. Requested: {quantity}, available: {book.StockQuantity}.");
            }

            var lineTotal = book.Price * quantity;

            orderItems.Add(new OrderItem
            {
                BookId = book.Id,
                BookTitle = book.Title,
                UnitPrice = book.Price,
                Quantity = quantity,
                LineTotal = lineTotal
            });
        }

        foreach (var item in orderItems)
        {
            var book = _bookRepository.GetById(item.BookId)!;
            book.StockQuantity -= item.Quantity;
            _bookRepository.Update(book);
        }

        var order = new Order
        {
            Id = _orderRepository.GetNextId(),
            CustomerId = customerId,
            CreatedAt = DateTime.UtcNow,
            Items = orderItems,
            TotalAmount = orderItems.Sum(x => x.LineTotal)
        };

        _orderRepository.Add(order);

        return order;
    }
}