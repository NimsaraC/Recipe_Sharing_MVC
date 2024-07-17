# Recipe Sharing MVC

Welcome to the Recipe Sharing Platform! This project is a web application built using ASP.NET Core MVC that allows users to share and explore recipes.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- User Registration and Authentication
- Recipe Creation, Editing, and Deletion
- Viewing and Searching Recipes
- User Profiles with Bio and Contact Information
- Review and Testimonial Section with Swiper Integration

## Technologies

- ASP.NET Core MVC
- C#
- Entity Framework Core
- Bootstrap
- Swiper.js
- HTML, CSS, JavaScript

## Getting Started

To get a local copy of the project up and running, follow these steps.

### Prerequisites

- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Visual Studio 2019 or later](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/NimsaraC/Recipe_Sharing_MVC.git
   ```

2. **Navigate to the project directory:**

   ```bash
   cd Recipe_Sharing_MVC
   ```

3. **Restore the NuGet packages:**

   ```bash
   dotnet restore
   ```

4. **Update the database:**

   Update the connection string in `appsettings.json` to point to your SQL Server instance, then run:

   ```bash
   dotnet ef database update
   ```

5. **Run the application:**

   ```bash
   dotnet run
   ```

6. **Open your browser and navigate to:**

   ```
   https://localhost:5001/
   ```

## Usage

### Register and Login

Create an account and log in to access all features of the application.

### Add a Recipe

Once logged in, navigate to the "Add Recipe" page to share your own recipes. Fill out the form with the recipe details and submit.

### View and Search Recipes

Browse through the recipes shared by other users. Use the search functionality to find specific recipes.

### User Profiles

Visit your profile to update your bio and contact information.

### Reviews and Testimonials

Leave reviews on recipes and view testimonials from other users in a Swiper-based slider.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request if you have any improvements or new features to suggest.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
