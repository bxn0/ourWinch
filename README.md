# Dummy Credentials for login 
# For Admin:
## mobil: [94719352]  pass: [Icardi1990!]
# For Ansatt:
## mobil: [94717177]  pass: [Icardi1990!]

# Demonstration of the app:
![IGLAND WINCH](https://github.com/bxn0/ourWinch/assets/82652466/ee33a86f-190b-4c80-8056-2b5fe61da9a5)

<pre>
  ## OurWinch-Nøsted-App (GitHub Repository Root) 

│   ├── ## Root         
│   │   ├── # CSS         
│   │   │   ├── **Layout.css** 
│   │   ├── **Image**         

│   ├── ## Controllers      
│   │   ├── **AccountControllers** 
│   │   ├── **CheckListControllers** 
│   │   ├── **DashboardControllers** 

│   ├── ##Data              
│   │   ├── **AppDbContext**   

│   ├── ##Migrations        

│   ├── ##Models        
│   │   ├── **AccountModel**  
│   │   ├── **ChecklistModel**
│   │   ├── **DashboardModel**
│   │   ├── **ErrorViewModel**

│   ├── ##Services         

│   ├── ##Views           
│   │   ├── **Account**       
│   │   ├── **Dashboard**     
│   │   ├── **Electro**       
│   │   ├── **FunksjonsTest** 
│   │   ├── **Hydrolisk**     
│   │   ├── **Mechanical**
│   │   ├── **Roles**         
│   │   ├── **ServiceOrder**  
│   │   ├── **ServiceSkjema** 
│   │   ├── **Trykk**         

│   ├── ##Shared         
│   │   ├── **Layout.cshtml** 
│   │   ├── **appsettings.json**

│   ├── ##DockerFile         

│   ├── ##Program.cs
</pre>



# Project Title and Description: Digitization of Service Orders

## Project Objective

The main goal of this project is to digitize the repair and maintenance processes of Nøsted & AS company's forestry winches. This digitization covers all stages from receiving customer service requests to effectively managing the repair and maintenance processes. The project aims to provide a fast, user-friendly, and integrated solution by optimizing existing manual processes.

## Use Scenarios

### * Receiving Customer Service Requests

Customers can submit service requests related to forestry winches by calling with the relevant information. Automatic categorization based on the nature of the request is done by the receptionist and recorded.

### * Management of Repair and Maintenance Processes

Repair or maintenance requests are digitally displayed by mechanical teams. Mechanical teams accept requests, update the status through the system, and if necessary, add additional information or create a new service order.

### * Reporting and Improvement Suggestions

The system generates reports based on completed repairs, maintenance, and customer feedback. Warranty Service conducts analysis under the headings of Repair and Maintenance. Improvement suggestions, such as (OK, Should Be Changed, Defective), are added by mechanical teams and presented to customers.
##  * User-Friendly Interface and Mobile Access

A user-friendly interface for both office administrators and field mechanic teams. Easily accessible from PCs and smartphones. Responsive design.

# Security and Authorization

Appropriate authorization levels for each user type are created by the admin. Strong encryption methods for the security of sensitive data. Tokens, etc.

## Developable and Expandable Structure

The project can be developed and expanded to meet future needs. Being open source allows customization according to the company's specific requirements.

 *** Nøsted.(2023,August 18). Prosjektbeskrivelse Nøsted & AS.pdf.
https://uia.instructure.com/courses/14002/files/2264630?module_item_id=514890

# Technological Structure

## Programming Language and Version
1. Programming language: C#
   - Version: .NET 6.0

## Used Technologies and Libraries

- **ASP.NET Core Framework:**
  - Framework used for .NET Core-based web applications.
- **Entity Framework:**
  - ORM (Object-Relational Mapping) tool used for managing database operations in an object-oriented manner.
- **AspNetCoreHero.ToastNotification:**
  - Library used for notifications.
- **Mailjet.Api:**
  - API library used for integration with the Mailjet service.
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore:**
  - Integration for user management with ASP.NET Core Identity.
- **Microsoft.AspNetCore.Identity.UI:**
  - Integration for user interface elements with ASP.NET Core Identity.
- **Microsoft.Data.SqlClient:**
  - SQL Client library used for communicating with SQL Server databases.
- **Microsoft.EntityFrameworkCore.Design:**
  - Reference for Entity Framework design tools.
- **Microsoft.EntityFrameworkCore.SqlServer:**
  - Integration of Entity Framework with SQL Server.
- **Microsoft.EntityFrameworkCore.Tools:**
  - Tools and design-time components used by Entity Framework.
- **Microsoft.VisualStudio.Azure.Containers.Tools.Targets:**
  - Visual Studio tools for Azure Container Service.
- **Microsoft.VisualStudio.Web.CodeGeneration.Design:**
  - Design-time tools for generating web application code.
- **Newtonsoft.Json:**
  - Popular library used for JSON operations.
- **Swashbuckle.AspNetCore:**
  - Library used for creating Swagger documentation and documenting APIs.

## Database and Connection Methods

- Database: SQL Server
- Connection Method: via Entity Framework

## Project Configuration File Examples

- .NET 6.0 targeting, user secrets, Docker configurations, etc.

## Project Dependencies

- List of external dependencies, including packages and versions.


# Architectural Structure / MVC Architectural Pattern

This code block follows the MVC (Model-View-Controller) architectural pattern. This pattern separates the application into three main components, providing a modular and easily maintainable structure.

## * Model

Includes data models such as the AppDbContext class and Identity-related classes (ApplicationUser, IdentityRole, etc.). Database operations and user authorization processes are handled in this layer.
 <pre> 
 ## Models 

  │   ├── **AccountModel**        
  │   ├── **ChecklistModel**      
  │   ├── **DashboardModel**      
  │   ├── **ErrorViewModel**      

</pre>

## * View

Includes elements related to the user interface, such as Razor pages in the Views folder and static files (CSS, JavaScript, etc.) in the wwww root folder.
 <pre>
 ## Views 

  │   ├── **Account**          
  │   ├── **Dashboard**        
  │   ├── **Electro**         
  │   ├── **FunksjonsTest**   
  │   ├── **Hydrolisk**       
  │   ├── **Mechanical**      
  │   ├── **Roles**           
  │   ├── **ServiceOrder**    
  │   ├── **ServiceSkjema**   
  │   ├── **Trykk**  
  │   ├── **User**          

      </pre>    


## * Controller

Controller classes like AccountController receive HTTP requests, initiate processes, and redirect to the appropriate view to display results.
 <pre>
## Controllers

   │   ├── **AccountControllers**    
   │   ├── **CheckListControllers** 
   │   ├── **DashboardControllers** 

</pre>

# Main Components and Interactions of the Application

## Identity Service

The Identity service added with the AddIdentity method handles user management and authorization processes, including operations like user registration, login, and role management.

# Migration for Adding Identity Tables

This migration script adds the necessary tables for ASP.NET Core Identity. It includes tables for roles, users, role claims, user claims, user logins, user roles, and user tokens.

## Tables Created

- `AspNetRoles`: Stores roles information.
- `AspNetUsers`: Stores users information.
- `AspNetRoleClaims`: Stores role claims.
- `AspNetUserClaims`: Stores user claims.
- `AspNetUserLogins`: Stores user logins.
- `AspNetUserRoles`: Stores user roles.
- `AspNetUserTokens`: Stores user tokens.

## How to Apply the Migration

To apply this migration, run the following command in the Package Manager Console:

bash - Update-Database

## How to Rollback the Migration

If needed, you can rollback the migration by running the following command:

### Remove-Migration

# Database Connection

Entity Framework Core, added with the AddDbContext method on AppDbContext, manages database operations. Access, query, and update operations on the database are performed through this layer.

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Represents the database context for the application, derived from IdentityDbContext for user management.
/// </summary>
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the context.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Constructor logic.
    }

    /// <summary>
    /// Gets or sets the DbSet for ServiceOrders.
    /// </summary>
    public DbSet<ServiceOrder> ServiceOrders { get; set; }

   

    /// <summary>
    /// Gets or sets the DbSet for ApplicationUser.
    /// </summary>
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
}

## Swagger and Notyf Service

Swagger is used to generate and view API documentation (AddSwaggerGen, UseSwagger, UseSwaggerUI). The Notyf service is used for managing notifications (AddNotyf).

## Email Sending Service

The MailJetEmailSender class is used to perform email sending operations. This class implements the IEmailSender interface.

## ServiceSkjemaService

The ServiceSkjemaService class represents a custom service, likely performing tasks such as business logic, data processing, or interaction with another service. This service is added with the AddScoped method, creating a scope for each HTTP request.

Interactions between these main components occur through HTTP requests and are handled by controllers. Controllers execute business logic, call services if necessary, and redirect results to views. This modular structure allows each component to have clear responsibilities.

# Project Folder Structure

The directory structure below represents the organization of a C#/.NET application project. Each subdirectory and file has a specific responsibility.

## OurWinch-Nøsted-App (GitHub Repository Root)
<pre>
Root
│
├── CSS
│   ├── Layout.css
├── Image
├── Controllers
│   ├── AccountControllers
│   │   ├── (files related to user accounts and authentication processes)
│   ├── CheckListControllers
│   │   ├── (files managing checklist-related operations)
│   ├── DashboardControllers
│   │   ├── (files related to the overview and dashboard operations)
├── Data
│   ├── AppDbContext
│   │   ├── (files related to database connection and model definitions)
├── Migrations
│   ├── (migration files tracking database changes)
├── Models
│   ├── AccountModel
│   │   ├── (files related to user accounts and authentication)
│   ├── ChecklistModel
│   │   ├── (files related to checklist operations)
│   ├── DashboardModel
│   │   ├── (files related to overview and dashboard operations)
│   ├── ErrorViewModel
│   │   ├── (files used for error display)
├── Services
│   ├── (files containing service classes used within the application)
├── Views
│   ├── (files representing different sections of the user interface)
├── Shared
│   ├── Layout.cshtml
│   │   ├── (commonly used view components)
│   ├── appsettings.json
│   │   ├── (the application's configuration file)
├── DockerFile
│   ├── (Dockerfile used to create the Docker container for the application)
├── Program.cs
│   ├── (the entry point file for the application)
</pre>

This directory structure logically organizes different parts of the application, making it easier to maintain, extend, and understand. Each subdirectory and file has a specific responsibility, ensuring clean and organized project management.

# Dependencies and Installation:

We have listed the dependencies and packages used in our project to specify the essential libraries and tools needed to run the project.

# Used Dependencies

- Microsoft.AspNetCore.Identity.EntityFrame
- Microsoft.AspNetCore.Identity.UI
- Microsoft.Data.SqlClient
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore
- Microsoft.VisualStudio.Azure.Cont
- Microsoft.VisualStudio.Web.CodeGeneration

![A![image-2](https://github.com/bxn0/ourWinch/assets/112567741/e69f99a7-d7d9-4ee6-ada5-42fac301f90e)

# Installation Instructions

To start the project and install dependencies, follow the step-by-step instructions below.

## 1. Clone the Project:
bash
git clone https://github.com/username/project-name.git
cd project-name

## 2. Install Dependencies: 

dotnet restore

## 3. Install Entity Framework Tool

dotnet tool install --global dotnet-ef

## 4. Create Migrations:

dotnet ef migrations add InitialCreate
 ## 5. Update Database: 

 dotnet ef database update

## 6. Run the Project:
dotnet run

### Configuration File

Create an appsettings.json file with the following content:

{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Your-Database-Connection-String"
  },
  "MailJet": {
    "ApiKey": "Your-MailJet-ApiKey",
    "SecretKey": "Your-MailJet-SecretKey"
  }
}

## Descriptions:

# LogLevel: Determines the logging levels.
# AllowedHosts: Specifies the allowed hosts.
# ConnectionStrings: Contains the database connection string.
# MailJet: Contains MailJet API keys.


## Environment Variables and Configuration Management


Program.cs file, explain the configuration processes and environment variables.

// ...

// App Configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is missing.");
}

// ...

// Add the DbContext to the DI container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.CommandTimeout(30); // Set the timeout for database commands
    })
    .LogTo(Console.WriteLine); // Add this line after the .UseSqlServer call
});

// ...

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));
    app.UseDeveloperExceptionPage();  // Add this line.
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ...


## Descriptions:

# GetConnectionString: Gets the database connection string.
# AddDbContext: Adds the DbContext to the DI container.
# UseSqlServer: Used for database interaction with SQL Server.
# LogTo: Sets logging settings.


## Security and Authorization
## Authentication and Authorization Mechanisms
This section covers various account management and authorization processes at Nøsted & AS.

# User Registration (Register)
The registration page allows users to sign up for the system. Users with the admin role can access this page.

<!-- Show registration page if the user has the admin role -->
@if(isAdmin) {

}

# Login
The login page allows users to log into the system. It includes mechanisms for username and password authentication.

<!-- Login form -->
<form asp-action="Login" asp-controller="Account" method="post">

</form>

## Role-Based Control (Roles Controller)
Controller containing role management pages. Accessible only by users with specific user roles.

// Example of a method within the controller class
[Authorize(Policy = "OnlySuperAdminChecker")]
public IActionResult Index()

## CSRF Protection

The application includes measures to protect against Cross-Site Request Forgery (CSRF) attacks. These measures involve adding unique tokens (CSRF tokens) to user sessions and requests to enhance security and prevent unauthorized requests.

For example, CSRF tokens are placed inside forms such as registration forms or login forms to ensure the security of requests.


<form asp-controller="Account" asp-action="Register" method="post" role="form">
    <input type="hidden" asp-for="__RequestVerificationToken" />
    <button type="submit" class="btn btn-success" id="registerButton">Register</button>
</form>



# XSS Protection

The application provides protection against Cross-Site Scripting (XSS) attacks by appropriately filtering or escaping user inputs and outputs, preventing potentially dangerous content within the application.

<p>@Html.Raw(Model.UserInput)</p>

## Error Tracking and Logging
## Logging Strategy
In this project, the built-in ILogger interface provided by ASP.NET Core is used for logging operations. Logging is an essential tool for understanding the application's state, detecting errors, and monitoring performance.

## Used Logging Tools
ILogger: The ILogger interface of ASP.NET Core is used for logging operations within the application, simplifying and managing logging processes.
## Logging Strategy
Logging in the ServiceSkjemaController is based on the following strategy:

 Logging Levels: Logging levels are used to log events at different levels of importance. Levels such as Information, Warning, and Error can be utilized.
Logging Messages: Logging messages are carefully crafted to express the event in a clear and understandable way. Well-formulated messages enhance the understanding of information within the log.
## Example Logging Code

//_logger.LogInformation("GetServiceSkjema called, ID: {ID}", id);


# Contributors

## Sep 10, 2023 – Nov 22, 2023


![image (6)](https://github.com/bxn0/ourWinch/assets/112567741/f52a14ef-8ad4-4c78-9aae-2284116c3990)



## yildirimsinop
 ### Pull Request #1: 103 commits, 27,825 ++, 16,975 --
Sep 17 Oct 08 Oct 29 Nov 19
##  bxn0
### Pull Request #2: 46 commits, 79,819 ++, 767 --
Sep 17 Oct 08 Oct 29 Nov 19
## Bunyamin54
### Pull Request #3: 44 commits, 7,971 ++, 7,177 --
Sep 17 Oct 08 Oct 29 Nov 19
## Orhanyil
#### Pull Request #4: 28 commits, 698 ++, 263 --
Sep 17 Oct 08 Oct 29 Nov 19
## sametdemirezen
### Pull Request #5: 25 commits, 2,851 ++, 1,924 --
Sep 17 Oct 08 Oct 29 Nov 19
## aslnthir
### Pull Request #6: 11 commits, 243 ++, 121 --
Sep 17 Oct 08 Oct 29 Nov 19


## Traffic
 
![image-1](https://github.com/bxn0/ourWinch/assets/112567741/2108d592-3e0b-4685-85aa-cd5f2c07d304)

```
