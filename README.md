# Travel Experience API Documentation

## Overview

The Travel Experience API is a .NET 8 WebAPI application that allows users to manage travel trips and activities. The API follows Clean Architecture principles with CQRS (Command Query Responsibility Segregation) pattern, using MediatR for request handling and FluentValidation for input validation.

## Architecture

### Tech Stack
- **.NET 8** - Core framework
- **ASP.NET Core WebAPI** - API framework
- **Entity Framework Core** - ORM
- **Microsoft SQL Server** - Database
- **MediatR** - Mediator pattern implementation
- **FluentValidation** - Input validation
- **CQRS Pattern** - Command/Query separation

## Domain Models

### BaseEntity
All entities inherit from this base class providing common properties:
```csharp
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

### Trip
Represents a travel trip with associated activities:
```csharp
public class Trip : BaseEntity
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalCost { get; set; }
    public ICollection<Activity> Activities { get; set; }
}
```

### Activity
Represents an activity within a trip:
```csharp
public class Activity : BaseEntity
{
    public int TripId { get; set; }
    public int DestinationId { get; set; }
    public int Duration { get; set; } // in hours
    public decimal Cost { get; set; }
    public Trip Trip { get; set; }
    public virtual Destination Destination { get; set; }
}
```

### Destination
Represents a travel destination:
```csharp
public class Destination : BaseEntity
{
    public string Name { get; set; }
    public string Country { get; set; }
    public virtual ICollection<Activity> Activities { get; set; }
}
```

## API Endpoints

### Trips Controller

#### POST /api/trips
Creates a new trip for a user.

**Request Body:**
```json
{
    "userId": "user12345",
    "title": "Summer Vacation in Italy",
    "startDate": "2025-08-01T00:00:00",
    "endDate": "2025-08-10T00:00:00",
    "activities": [
        {
            "destinationId": 1,
            "duration": 4,
            "cost": 250.5
        },
        {
            "destinationId": 2,
            "duration": 6,
            "cost": 400.0
        }
    ]
}
```

**Response Codes:**
- `200 OK` - Trip created successfully
- `400 Bad Request` - Validation errors
- `500 Internal Server Error` - Server error

#### GET /api/trips/{tripId}
Retrieves a specific trip by ID.

**Parameters:**
- `tripId` (int) - The ID of the trip to retrieve

**Response Codes:**
- `200 OK` - Trip found and returned
- `404 Not Found` - Trip not found
- `400 Bad Request` - Invalid trip ID
- `500 Internal Server Error` - Server error

#### GET /api/trips/user/{userId}
Retrieves all trips for a specific user.

**Parameters:**
- `userId` (string) - The ID of the user

**Response Codes:**
- `200 OK` - Trips returned (empty array if no trips)
- `400 Bad Request` - Invalid user ID
- `500 Internal Server Error` - Server error

### Destinations Controller

#### GET /api/destinations
Retrieves all available destinations.

**Response Codes:**
- `200 OK` - Destinations returned
- `500 Internal Server Error` - Server error

## Validation Rules

### CreateTripCommand Validation

#### UserId
- **Required** - Cannot be empty
- **Length** - Must be between 8 and 32 characters
- **Format** - Can only contain letters and numbers (alphanumeric)

#### Title
- **Required** - Cannot be empty
- **Max Length** - Cannot exceed 200 characters

#### StartDate
- **Required** - Cannot be empty
- **Future Date** - Cannot be in the past
- **Max Future** - Cannot be more than 10 years in the future

#### EndDate
- **Required** - Cannot be empty
- **After Start** - Must be after start date
- **Max Future** - Cannot be more than 10 years in the future

#### Trip Duration
- **Max Duration** - Cannot exceed 365 days

#### Activities
- **Required** - At least one activity is required
- **Max Count** - Cannot have more than 50 activities per trip

### CreateActivityDto Validation

#### DestinationId
- **Positive** - Must be greater than 0
- **Valid Range** - Must be within valid integer range

#### Duration
- **Positive** - Must be greater than 0
- **Max Hours** - Cannot exceed 24 hours

#### Cost
- **Non-negative** - Cannot be negative
- **Max Amount** - Cannot exceed $50,000

### Query Validation

#### GetTripByIdQuery
- **TripId** - Must be greater than 0

#### GetTripsByUserIdQuery
- **UserId** - Same rules as CreateTripCommand UserId

## Error Handling

The API implements comprehensive error handling:

### Validation Errors (400 Bad Request)
```json
{
    "errors": [
        {
            "field": "UserId",
            "message": "User ID is required"
        },
        {
            "field": "Title",
            "message": "Title cannot exceed 200 characters"
        }
    ]
}
```

### Not Found Errors (404 Not Found)
```json
{
    "error": "Trip not found"
}
```

### Server Errors (500 Internal Server Error)
```json
{
    "error": "An internal server error occurred"
}
```

## Sample Usage

### Creating a Trip
```bash
curl -X POST "https://api.travelexperience.com/api/trips" \
     -H "Content-Type: application/json" \
     -d '{
       "userId": "user12345",
       "title": "European Adventure",
       "startDate": "2025-07-15T00:00:00",
       "endDate": "2025-07-25T00:00:00",
       "activities": [
         {
           "destinationId": 1,
           "duration": 8,
           "cost": 500.00
         }
       ]
     }'
```

### Getting a Trip
```bash
curl -X GET "https://api.travelexperience.com/api/trips/123"
```

### Getting User's Trips
```bash
curl -X GET "https://api.travelexperience.com/api/trips/user/user12345"
```

### Getting All Destinations
```bash
curl -X GET "https://api.travelexperience.com/api/destinations"
```

## Database Schema

### Tables
- **Trips** - Stores trip information
- **Activities** - Stores activity details linked to trips
- **Destinations** - Stores destination information

### Relationships
- Trip → Activities (One-to-Many)
- Activity → Destination (Many-to-One)
- Activity → Trip (Many-to-One)

## Business Rules

1. **Trip Duration**: Trips cannot exceed 365 days
2. **Activity Duration**: Each activity cannot exceed 24 hours
3. **Cost Limits**: Activity costs cannot exceed $50,000
4. **Activity Limits**: Maximum 50 activities per trip
5. **User ID Format**: Must be alphanumeric, 8-32 characters
6. **Date Validation**: Dates cannot be in the past or more than 10 years in the future

## Development Notes

### CQRS Implementation
CQRS Implementation and Clean Architecture Design Decisions
Why Clean Architecture?
Separation of Concerns: Clear boundaries between Domain, Application, Infrastructure, and Presentation layers ensure maintainable code where business logic is independent of data access and UI concerns.
Dependency Inversion: Inner layers don't depend on outer layers, making the system testable and flexible. Domain entities are independent of database implementation.
Testability: Each layer can be unit tested independently without external dependencies.
Why CQRS Pattern?
Read/Write Separation: Commands handle state changes with focus on business rules and validation, while Queries handle data retrieval optimized for performance and specific read models.
Single Responsibility: Each command/query has one clear purpose:

CreateTripCommand - Trip creation only
GetTripByIdQuery - Trip retrieval by ID only

Scalability: Read and write operations can be scaled independently and use different optimization strategies.
Repository Pattern Implementation
Separate Read and Write Repositories
ReadRepository<T>: Optimized for queries with AsNoTracking() for better performance, supports complex LINQ expressions, and provides flexible querying capabilities.
WriteRepository<T>: Optimized for write operations with full EF Core tracking, handles entity state management, and ensures data consistency through automatic SaveChanges().
Benefits

Performance: Read repositories disable change tracking; write repositories maintain full tracking
Interface Segregation: Read interfaces only expose query methods; write interfaces only expose modification methods
Future Flexibility: Easy to add caching layers, different data sources, or eventual consistency patterns

### Logging
The API implements structured logging using `ILogger<T>` for:
- Successful operations
- Validation failures
- Error conditions

### Response Patterns
- Consistent error response format
- Proper HTTP status codes
- Structured validation error messages
