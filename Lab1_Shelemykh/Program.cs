using Lab1_Shelemykh.Data;
using Lab1_Shelemykh.Repositories;
using Lab1_Shelemykh.Services;
using Lab1_Shelemykh;

var repository = new InMemoryStoreRepository(
    SeedData.GetBooks(),
    SeedData.GetCustomers(),
    SeedData.GetOrders());

var orderService = new OrderService(repository, repository, repository);
var reportService = new ReportService(repository, repository);
var app = new App(repository, repository, repository, orderService, reportService);

app.Run();
