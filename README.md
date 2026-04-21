# BookStoreConsole

A simple .NET console application for managing a small in-memory book store.

## About the project

BookStoreConsole is a console-based demo project that simulates a small bookstore workflow.

The application allows you to:

- view available books
- view customers
- create new orders
- see all existing orders
- generate simple sales reports

The project is intentionally structured in a clean and extensible way, even though it is a single console application.  
This makes it easier to maintain, refactor, and cover business logic with unit tests later.

---

## Features

- In-memory data storage
- Seeded demo data
- Book catalog with stock quantity
- Customer list
- Order creation with validation
- Automatic stock update after order creation
- Sales summary report
- Separation of UI, business logic, repository contracts, and models

---

## Project structure

```text
BookStoreConsole/
├─ BookStoreConsole.csproj
├─ Program.cs
├─ App.cs
├─ Models/
│  ├─ Book.cs
│  ├─ Customer.cs
│  ├─ Order.cs
│  └─ OrderItem.cs
├─ Contracts/
│  ├─ IBookRepository.cs
│  ├─ ICustomerRepository.cs
│  └─ IOrderRepository.cs
├─ Data/
│  └─ SeedData.cs
├─ Repositories/
│  └─ InMemoryStoreRepository.cs
└─ Services/
   ├─ OrderService.cs
   └─ ReportService.cs