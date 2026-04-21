using Lab1_Shelemykh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh.Contracts;

public interface IOrderRepository
{
    IReadOnlyCollection<Order> GetAll();

    void Add(Order order);

    int GetNextId();
}
