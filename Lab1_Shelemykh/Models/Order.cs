using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh.Models;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItem> Items { get; set; } = new();

    public decimal TotalAmount { get; set; }
}