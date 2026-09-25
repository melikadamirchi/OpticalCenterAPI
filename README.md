# Optical Center API

A RESTful ASP.NET Core Web API for managing patients, doctors, and their relationships in an optical center.

## Technologies

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Features

- Patient CRUD operations
- Doctor CRUD operations
- Patient-Doctor many-to-many relationship
- DTOs for API requests and responses
- Input validation
- Composite primary key for patient-doctor relationships
- Entity Framework Core with SQL Server
- RESTful HTTP status codes
- Swagger / OpenAPI documentation

## API Endpoints

### Patients

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Patients` | Get all patients |
| GET | `/api/Patients/{id}` | Get a patient by ID |
| POST | `/api/Patients` | Create a new patient |
| PUT | `/api/Patients/{id}` | Update a patient |
| DELETE | `/api/Patients/{id}` | Delete a patient |

### Doctors

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Doctors` | Get all doctors |
| GET | `/api/Doctors/{id}` | Get a doctor by ID |
| POST | `/api/Doctors` | Create a new doctor |
| PUT | `/api/Doctors/{id}` | Update a doctor |
| DELETE | `/api/Doctors/{id}` | Delete a doctor |

### Patient-Doctor Relationships

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/PatientDoctors` | Get all patient-doctor relationships |
| GET | `/api/PatientDoctors/{patientId}/{doctorId}` | Get a specific relationship |
| POST | `/api/PatientDoctors` | Connect a patient with a doctor |
| DELETE | `/api/PatientDoctors/{patientId}/{doctorId}` | Remove a patient-doctor relationship |

## Architecture

The project follows a simple layered structure:

- **Controllers** — Handle HTTP requests and API endpoints.
- **DTOs** — Define the data sent to and returned by the API.
- **Models** — Represent the database entities.
- **Data** — Contains the Entity Framework Core `DbContext` and database configuration.
- **SQL Server** — Stores application data.



## Database Relationships

The API uses a many-to-many relationship between patients and doctors.

A patient can be connected to multiple doctors, and a doctor can be connected to multiple patients. The `PatientDoctor` junction table manages these relationships using a composite primary key consisting of `PatientId` and `DoctorId`.


## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server
- Visual Studio 2022 or later

### Setup

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Update the connection string in `appsettings.json` to point to your local SQL Server instance.
4. Apply the Entity Framework Core migrations.
5. Run the application.
6. Open Swagger to explore and test the API.

### Database

The project uses Entity Framework Core migrations to create and update the SQL Server database schema.

Run the following command in the Package Manager Console:

```powershell
Update-Database


