# Employee Management System

This project is a small Employee Management System built using ASP.NET Core Web API for the backend and Blazor WebAssembly for the frontend. The application enables CRUD operations for employees and positions, along with bulk upload functionality using JSON files.

### API Endpoints

- Implemented using a controller-based API architecture.
- Input validation is performed using data annotations.

#### Employee Endpoints

- **GET /Employees**: Retrieves all employees.
- **GET /Employees/{id}**: Retrieves a specific employee by ID.
- **POST /Employees**: Creates a new employee.
- **PUT /Employees/{id}**: Updates an existing employee.
- **DELETE /Employees/{id}**: Deletes an employee by ID.
- **POST /Employees/Upload**: Bulk uploads employees using a JSON file.

#### Position Endpoints

- **GET /Positions**: Retrieves all positions.
- **POST /Positions/Upload**: Bulk uploads positions using a JSON file.

### Frontend

- The frontend is implemented using Blazor WebAssembly.
- It interacts with the backend API to manage employees and positions.
- The frontend features pages for adding, updating, and listing employees, as well as bulk uploading employees and positions.

### Database and Entity Framework

- The project uses **Entity Framework Core** with a **Code-First** approach, allowing you to define the database schema using C# classes. This approach also enables migrations to evolve the database schema as the model changes.
- The project uses an in-memory database for quick development and testing purposes.
- For production, the only change needed is to set up a proper database (e.g., SQL Server, PostgreSQL) and connect to it via a proper connection string in the `appsettings.json` or `Program.cs` file.

### Error Handling

- Global exception handling is implemented using middleware in the backend, which logs exceptions to help in troubleshooting issues.

### Logging

- Logging is configured using Serilog to capture and store log information, which helps in tracking the application's behavior and troubleshooting issues.

## Setup Instructions

### Step 1: Clone the Repository

Clone the repository and navigate into the project directory.

### Step 2: Open the Solution in Visual Studio

1. Open the `EmployeeManager.sln` solution file in Visual Studio.

### Step 3: Configure Multiple Startup Projects

1. In Visual Studio, right-click on the solution in the Solution Explorer and select **Properties**.
2. Under **Common Properties**, select **Startup Project**.
3. Choose **Multiple startup projects**.
4. Set the **API** and **BlazorFrontend** projects to **Start**.

### Step 4: Start the Application

1. Press `F5` or click the **Start** button to run both the API and Blazor frontend projects.
