Product Management API
Overview
This is a Product Management API designed to handle product-related operations, including creating, updating, retrieving, and deleting products. It uses ASP.NET Core and Entity Framework Core with a SQL Server database to manage product data. The project also follows the Controller-Service-Repository design pattern to ensure separation of concerns and maintainable code.

Features
CRUD Operations for Product Management:

POST /api/products: Create a new product.

GET /api/products: Retrieve all products.

GET /api/products/{id}: Retrieve a specific product by ID.

PUT /api/products/{id}: Update a product by ID.

DELETE /api/products/{id}: Delete a product by ID.

Stock Management:

PUT /api/products/decrement-stock/{id}/{quantity}: Decrement stock of a product.

PUT /api/products/add-to-stock/{id}/{quantity}: Increment stock of a product.

Auto-generated Unique Product IDs: Ensures each product has a unique product ID (Identity key starting from 1 and auto-increments) that is auto-generated even in a distributed environment.

Tech Stack
Backend: ASP.NET Core

Database: SQL Server

ORM: Entity Framework Core

Dependency Injection: Used for injecting services, repositories, and logging functionality.

Logging: Integrated using ILogger for debugging, error logging, and performance monitoring.

Project Structure
Controller Layer
ProductsController: Handles HTTP requests related to product management. It communicates with the ProductService to process business logic.

Service Layer
ProductService: Contains business logic for product operations, interacts with the repository layer.

Repository Layer
ProductRepository: Implements CRUD operations using Entity Framework Core for data access.

Models
Product: Entity representing the product structure, which includes fields like ProductId, Name, Price, Stock, etc.

Database
ProductDbContext: EF Core database context to manage connections and entity models.

Getting Started
Prerequisites
.NET 6.0 or higher

SQL Server (local or remote)

Setup
Clone the repository:

bash
Copy
Edit
git clone https://github.com/Manzoor2297/ProductManagementAPI.git
cd ProductManagementAPI
Restore NuGet packages:

bash
Copy
Edit
dotnet restore
Apply migrations to create the database:

bash
Copy
Edit
dotnet ef database update
Run the application:

bash
Copy
Edit
dotnet run

Endpoints
POST /api/products - Create a new product.

GET /api/products - Retrieve all products.

GET /api/products/{id} - Retrieve a product by ID.

PUT /api/products/{id} - Update product details.

DELETE /api/products/{id} - Delete a product.

PUT /api/products/decrement-stock/{id}/{quantity} - Decrease the stock of a product.

PUT /api/products/add-to-stock/{id}/{quantity} - Increase the stock of a product.

