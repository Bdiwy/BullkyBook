# BullkyBook

A comprehensive ASP.NET Core MVC web application for managing books, categories, products, and users. Built with .NET 9.0 and Entity Framework Core.

## Features

- **Product Management**: Create, read, update, and delete products with inventory tracking
- **Category Management**: Organize products into categories with display ordering
- **User Management**: User authentication and management system
- **Database Integration**: SQL Server database with Entity Framework Core
- **Responsive Design**: Modern MVC architecture with Bootstrap-based UI
- **Pagination**: Efficient data browsing with X.PagedList integration

## Technologies Used

- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core MVC**: Web application framework
- **Entity Framework Core 9.0**: Object-relational mapping (ORM)
- **SQL Server**: Database (LocalDB for development)
- **X.PagedList.Mvc.Core**: Pagination support
- **Bootstrap**: Front-end framework (via wwwroot)

## Project Structure

```
BullkyBook/
├── Controllers/          # MVC Controllers
│   ├── AuthController.cs
│   ├── CategoryController.cs
│   ├── HomeController.cs
│   ├── ProductController.cs
│   └── UserController.cs
├── Models/              # Data models
│   ├── Category.cs
│   ├── Product.cs
│   ├── User.cs
│   └── ErrorViewModel.cs
├── Views/               # Razor views
├── DataBase/            # Database context
│   └── ApplicationDbContext.cs
├── Migrations/          # EF Core migrations
├── wwwroot/            # Static files (CSS, JS, images)
└── appsettings.json    # Configuration
```

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd BullkyBook
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update the database connection string in `appsettings.json` if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=bullkyBook;Trusted_Connection=True;"
   }
   ```

4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

6. Open your browser and navigate to `https://localhost:5001` (or the port shown in the console)

## Database Models

### Category
- **Id**: Primary key
- **Name**: Category name (required)
- **DisbplayOrder**: Display order for sorting
- **CreatedDateTime**: Timestamp
- **Products**: Related products collection

### Product
- **Id**: Primary key
- **Name**: Product name (required)
- **QtyInStock**: Quantity in stock
- **CategoryId**: Foreign key to Category
- **Category**: Navigation property
- **CreatedDateTime**: Timestamp

### User
- User authentication and management model

## Development

### Entity Framework Commands

```bash
# Add a new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Build and Test

```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Watch mode (auto-reload on changes)
dotnet watch run
```

## Configuration

The application uses `appsettings.json` for configuration:
- Database connection strings
- Logging levels
- Application-specific settings

For development-specific settings, use `appsettings.Development.json`.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is part of a learning/demo repository. Please check with the repository owner for licensing information.

## Contact

For questions or support, please open an issue in the repository.
