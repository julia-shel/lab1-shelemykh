using Lab1_Shelemykh.Data;
using Lab1_Shelemykh.Repositories;
using Lab1_Shelemykh.Services;
using Lab1_Shelemykh;

// This console application simulates a small bookstore management system.
// It loads predefined books, customers, and orders into memory when the app starts.
// The user can browse data, create new orders, and view simple sales reports.
// Business logic is separated into services, while repositories handle in-memory data access.
var repository = new InMemoryStoreRepository(
    SeedData.GetBooks(),
    SeedData.GetCustomers(),
    SeedData.GetOrders());

var orderService = new OrderService(repository, repository, repository);
var reportService = new ReportService(repository, repository);
var app = new App(repository, repository, repository, orderService, reportService);

app.Run();
