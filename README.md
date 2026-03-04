# EventXpert_MVC_Project

## 🏟️ EventXpert: Dynamic Sports Hub

**EventXpert** is a full-stack web application I developed to manage and analyze sports events. I transformed a static prototype into a robust **ASP.NET Core MVC** platform, featuring real-time data integration, database persistence, and a polished UI.

[**Live Demo on Azure**](#) *(Currently the page is in 'Stopped' state to avoid service cost.) *

---

## 🚀 Key Features
* **Full CRUD Management:** I built a complete interface to Create, Read, Update, and Delete sports events with instant data reflection across the dashboard.
* **Dynamic Dashboard:** Integrated **Open-Meteo** (Weather) and **TheSportsDB** (Live scores) APIs to provide real-time updates for users.
* **Data Visualization:** Utilized **Chart.js** to create interactive analytics for ticket prices and venue distribution.
* **Cloud Infrastructure:** Personally managed the deployment and environment configuration on **Microsoft Azure**.

## 🛠️ Tech Stack & Skills
* **Backend:** C# | ASP.NET Core MVC 
* **Database:** SQLite | Entity Framework (EF) Core
* **Frontend:** Razor Views | JavaScript (ES6+) | Chart.js | CSS3
* **DevOps:** Microsoft Azure App Service | Git

---

## 💡 Technical Highlights

### MVC Architecture & Persistence
I migrated the project to a formal **Model-View-Controller** pattern to ensure a clean separation of concerns. To handle data, I implemented **EF Core** with a SQLite provider, ensuring all event records remain consistent across user sessions.

### API & Asynchronous Logic
To keep the application responsive, I handled all external API calls **asynchronously**. I also implemented secure key handling via `IConfiguration` to ensure API credentials remain protected and are never hardcoded.

### Performance & Mapping
I used `JsonPropertyName` attributes to efficiently map complex external JSON data to internal C# models. I structured the **EF Core abstraction** so the app can be migrated to SQL Server or PostgreSQL with minimal configuration changes.

---

## 🛠️ Challenges & Solutions

| Challenge | My Solution |
| :--- | :--- |
| **API Mapping** | Used `JsonPropertyName` to map non-standard external fields to clean C# models. |
| **Cloud Persistence** | Configured Azure to ensure the SQLite database file persisted through app restarts. |
| **Concurrency** | Implemented EF Core logic to manage and prevent data conflicts during updates. |

---

## 🏁 Getting Started

Follow these steps to get a local copy up and running.

1. Clone the repository:
```bash
https://github.com/Kushal-555/EventXpert_MVC_Project.git
```
2. Database Setup
Run the following command to apply migrations and create the local SQLite database:
```bash
dotnet ef database update
```

3. Running the Application
Start the development server:
```bash
dotnet run
```
Once running, open your browser and navigate to http://localhost:5000.


