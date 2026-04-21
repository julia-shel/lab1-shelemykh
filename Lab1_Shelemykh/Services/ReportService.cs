using Lab1_Shelemykh.Contracts;
using Lab1_Shelemykh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh.Services;

public class ReportService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public ReportService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public int GetOrdersCount()
    {
        return _orderRepository.GetAll().Count;
    }

    public decimal GetTotalRevenue()
    {
        return _orderRepository.GetAll().Sum(x => x.TotalAmount);
    }

    public IReadOnlyCollection<(string Title, int Quantity)> GetTopSellingBooks(int top)
    {
        if (top <= 0)
        {
            return Array.Empty<(string Title, int Quantity)>();
        }

        return _orderRepository
            .GetAll()
            .SelectMany(x => x.Items)
            .GroupBy(x => x.BookTitle)
            .Select(g => (Title: g.Key, Quantity: g.Sum(x => x.Quantity)))
            .OrderByDescending(x => x.Quantity)
            .ThenBy(x => x.Title)
            .Take(top)
            .ToList();
    }

    public (Customer Customer, decimal TotalSpent)? GetTopCustomerBySpent()
    {
        var topCustomerData = _orderRepository
            .GetAll()
            .GroupBy(x => x.CustomerId)
            .Select(g => new
            {
                CustomerId = g.Key,
                TotalSpent = g.Sum(x => x.TotalAmount)
            })
            .OrderByDescending(x => x.TotalSpent)
            .FirstOrDefault();

        if (topCustomerData is null)
        {
            return null;
        }

        var customer = _customerRepository.GetById(topCustomerData.CustomerId);
        if (customer is null)
        {
            return null;
        }

        return (customer, topCustomerData.TotalSpent);
    }
}
