# Academy Backend Developer Test (2026)

This repository contains the backend API for the Academy backend developer assessment. It provides a robust, RESTful API to manage user playlists and songs, built with clean architecture and SOLID principles in mind.

## 🛠 Tech Stack & Architecture
*   **Framework:** ASP.NET Core 10 Web API
*   **Database:** PostgreSQL
*   **ORM:** Entity Framework Core (Code-First)
*   **Testing:** xUnit, Moq, WebApplicationFactory (In-Memory DB for Integration Tests)

### Design Patterns Used
*   **Layered Architecture:** Strict separation of concerns between Controllers (routing), Services (business logic), and Repositories (data access).
*   **Repository Pattern:** Abstracts the Entity Framework database queries away from the business logic.
*   **Dependency Injection (DI):** Injected interfaces (`IPlaylistService`, `IPlaylistRepository`) to ensure loose coupling and testability.
*   **Data Transfer Objects (DTOs):** Used immutable `record` types to prevent exposing internal database entities to the client.

---

## 🗄️ Database Documentation & Justification
**Database Chosen:** PostgreSQL (Relational)

**Justification:** 
Playlists and Songs inherently share a **Many-to-Many relationship** (a playlist contains multiple songs, and a specific song can exist in multiple playlists). A relational database like PostgreSQL was explicitly chosen because it natively enforces data integrity through foreign key constraints, ensures ACID compliance for transactions, and allows for efficient table joining. 

**Schema Design:**
*   `Playlists`: Contains `Id` (UUID), `Name` (String), and `UserId` (String - to track playlist ownership).
*   `Songs`: Contains `Id` (UUID), `Title` (String), and `Artist` (String).
*   `PlaylistSongs` (Junction Table): Automatically managed by EF Core to map the many-to-many relationships between playlists and songs.

---

## 🚀 How to Run the Project Locally

### Prerequisites
1.  [.NET 10 SDK](https://dotnet.microsoft.com/download)
2.  [PostgreSQL](https://www.postgresql.org/download/) running locally (or a hosted PostgreSQL URI).

### Step 1: Clone the Repository
\`\`\`bash
git clone <your-repository-url>
cd AcademyBackendTest
\`\`\`

### Step 2: Database Configuration
Open `AcademyBackendTest.Api/appsettings.json` and update the `DefaultConnection` string with your local PostgreSQL credentials:
\`\`\`json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=AcademyTestDb;Username=postgres;Password=YOUR_PASSWORD"
}
\`\`\`

### Step 3: Apply Database Migrations
Navigate to the API directory and run the EF Core migrations to create the database schema:
\`\`\`bash
cd AcademyBackendTest.Api
dotnet ef database update
\`\`\`

### Step 4: Run the Application
Start the server:
\`\`\`bash
dotnet run
\`\`\`
The API will be available at `http://localhost:5000` or `https://localhost:5001`.

---

## 🧪 Running the Tests
This solution includes both Unit Tests (mocked database) and Integration Tests (In-Memory database). To run the test suite:
\`\`\`bash
# From the root directory
dotnet test
\`\`\`

---

## 🤖 AI Usage Transparency
As per the test requirements, AI (Google Gemini) was utilized as an expert pair-programming assistant during this assessment. It was primarily used for:
*   Discussing and confirming architectural choices (ASP.NET vs. Node.js).
*   Scaffolding repetitive boilerplate (e.g., test fixtures, DTO records).
*   Troubleshooting Linux environment configuration and C# namespace errors.

*A full transcript of the chat context is attached to the email submission as requested.*