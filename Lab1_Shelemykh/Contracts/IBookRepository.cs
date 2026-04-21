using Lab1_Shelemykh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_Shelemykh.Contracts;

public interface IBookRepository
{
    IReadOnlyCollection<Book> GetAll();

    Book? GetById(int id);

    void Update(Book book);
}