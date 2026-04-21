using Lab1_Shelemykh.Contracts;
using Lab1_Shelemykh.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh;
public class App
{
    private readonly IBookRepository _bookRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly OrderService _orderService;
    private readonly ReportService _reportService;

    public App(
        IBookRepository bookRepository,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        OrderService orderService,
        ReportService reportService)
    {
        _bookRepository = bookRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _orderService = orderService;
        _reportService = reportService;
    }

    public void Run()
    {
        while (true)
        {
            PrintMenu();

            var input = Console.ReadLine();

            Console.WriteLine();

            switch (input)
            {
                case "1":
                    ShowBooks();
                    break;
                case "2":
                    ShowCustomers();
                    break;
                case "3":
                    CreateOrder();
                    break;
                case "4":
                    ShowOrders();
                    break;
                case "5":
                    ShowSalesSummary();
                    break;
                case "0":
                    Console.WriteLine("Goodbye.");
                    return;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("=== Book Store ===");
        Console.WriteLine("1 - Show books");
        Console.WriteLine("2 - Show customers");
        Console.WriteLine("3 - Create order");
        Console.WriteLine("4 - Show orders");
        Console.WriteLine("5 - Show sales summary");
        Console.WriteLine("0 - Exit");
        Console.Write("Choose an option: ");
    }

    private void ShowBooks()
    {
        var books = _bookRepository.GetAll().OrderBy(x => x.Id);

        Console.WriteLine("=== Books ===");

        foreach (var book in books)
        {
            Console.WriteLine(
                $"Id: {book.Id} | {book.Title} | Author: {book.Author} | Genre: {book.Genre} | Price: {book.Price:C} | Stock: {book.StockQuantity}");
        }
    }

    private void ShowCustomers()
    {
        var customers = _customerRepository.GetAll().OrderBy(x => x.Id);

        Console.WriteLine("=== Customers ===");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"Id: {customer.Id} | {customer.FullName} | {customer.Email} | {customer.City} | Active: {customer.IsActive}");
        }
    }

    private void CreateOrder()
    {
        try
        {
            Console.Write("Enter customer id: ");
            var customerIdInput = Console.ReadLine();

            if (!int.TryParse(customerIdInput, out var customerId))
            {
                Console.WriteLine("Invalid customer id.");
                return;
            }

            Console.WriteLine();
            ShowBooks();

            Console.WriteLine();
            Console.WriteLine("Enter order items in format: bookId quantity");
            Console.WriteLine("Example: 2 3");
            Console.WriteLine("Submit an empty line to finish.");

            var items = new Dictionary<int, int>();

            while (true)
            {
                Console.Write("Item: ");
                var line = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid format. Use: bookId quantity");
                    continue;
                }

                if (!int.TryParse(parts[0], out var bookId) || !int.TryParse(parts[1], out var quantity))
                {
                    Console.WriteLine("Both values must be numbers.");
                    continue;
                }

                if (items.ContainsKey(bookId))
                {
                    items[bookId] += quantity;
                }
                else
                {
                    items.Add(bookId, quantity);
                }
            }

            var order = _orderService.CreateOrder(customerId, items);

            Console.WriteLine();
            Console.WriteLine($"Order #{order.Id} created successfully.");
            Console.WriteLine($"Created at: {order.CreatedAt:u}");
            Console.WriteLine($"Total amount: {order.TotalAmount:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Order creation failed: {ex.Message}");
        }
    }

    private void ShowOrders()
    {
        var orders = _orderRepository.GetAll().OrderBy(x => x.Id);

        Console.WriteLine("=== Orders ===");

        foreach (var order in orders)
        {
            var customer = _customerRepository.GetById(order.CustomerId);
            var customerName = customer?.FullName ?? "Unknown customer";

            Console.WriteLine(
                $"Order #{order.Id} | Customer: {customerName} | Date: {order.CreatedAt:u} | Total: {order.TotalAmount:C}");

            foreach (var item in order.Items)
            {
                Console.WriteLine(
                    $"   - {item.BookTitle} | {item.Quantity} x {item.UnitPrice:C} = {item.LineTotal:C}");
            }
        }
    }

    private void ShowSalesSummary()
    {
        Console.WriteLine("=== Sales Summary ===");
        Console.WriteLine($"Orders count: {_reportService.GetOrdersCount()}");
        Console.WriteLine($"Total revenue: {_reportService.GetTotalRevenue():C}");

        Console.WriteLine();
        Console.WriteLine("Top selling books:");

        var topBooks = _reportService.GetTopSellingBooks(3);
        foreach (var book in topBooks)
        {
            Console.WriteLine($"- {book.Title}: {book.Quantity} sold");
        }

        Console.WriteLine();
        var topCustomer = _reportService.GetTopCustomerBySpent();

        if (topCustomer is null)
        {
            Console.WriteLine("Top customer: no data");
            return;
        }

        Console.WriteLine(
            $"Top customer: {topCustomer.Value.Customer.FullName} | Total spent: {topCustomer.Value.TotalSpent:C}");
    }
}

