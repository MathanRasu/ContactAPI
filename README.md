# Contact API Project

This is a RESTful API built with ASP.NET Core, used for managing contacts. It provides endpoints to perform CRUD (Create, Read, Update, Delete) operations on contacts, including searching and filtering functionalities. The API is designed to integrate seamlessly with a frontend interface or mobile applications.

## Features

- **Authentication**: Secure login using JWT (JSON Web Token).
- **CRUD Operations**: Add, update, delete, and retrieve contact information.
- **Filtering and Sorting**: Ability to search and sort contacts based on various fields like name, email, etc.
- **Logging**: Integrated logging with **Serilog** to monitor application behavior and events.
- **Error Handling**: Structured error responses for failed requests.
- **Cross-Origin Resource Sharing (CORS)**: Configured to allow requests from any origin.

## Tech Stack

- **Backend**: ASP.NET Core 6.0
- **Database**: SQL Server (with Entity Framework Core)
- **Authentication**: JWT (JSON Web Token)
- **Logging**: Serilog for centralized logging
- **Swagger**: For API documentation and testing
- **Cors**: To handle cross-origin requests

## Prerequisites

- .NET 6.0 SDK or later
- SQL Server (or any other relational database with appropriate configuration)
- Serilog configuration for logging (if you intend to use custom sinks)

## Setup and Installation

### 1. Clone the repository

```bash
git clone https://github.com/yourusername/contact-api.git
cd contact-api
