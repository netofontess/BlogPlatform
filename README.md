# BlogPlatform API

This project implements a simple blog API using **ASP.NET Core 9**, **FluentMigrator** for database migrations, and **SQLite** for data persistence. The application allows you to create and query blog posts, as well as add comments to posts.

## Features

- **GET /api/posts**: Returns a list of all blog posts, including their titles and the number of comments associated with each post.
- **POST /api/posts**: Creates a new blog post.
- **GET /api/posts/{id}**: Retrieves a specific blog post by its ID, including the title, content, and a list of associated comments.
- **POST /api/posts/{id}/comments**: Adds a comment to a specific blog post.

## Technologies

- **ASP.NET Core 9.0**
- **SQLite** for the database (persistent with `.db` file)
- **FluentMigrator** for migration management
- **Swagger** for API documentation

## Prerequisites

- **Docker** installed on your system.
- **.NET SDK 9.0** installed locally (for development or running locally without Docker).

## Running Locally

### 1. Clone the repository
Clone this repository to your local machine using Git.

```bash
git clone https://github.com/your-repository/blogplatform-api.git
cd blogplatform-api
```

### 2. Restore dependencies
Run the following command to restore the necessary NuGet packages:

```bash
dotnet restore
```

### 3. Run the application locally
To run the application locally, use the following command:

```bash
dotnet run
```

After running the command, the application will be accessible at `http://localhost:5000` or `https://localhost:5001` (if SSL is configured). You can access the API documentation at:

- **Swagger**: `http://localhost:5000/swagger`

### 4. Testing the endpoints
Once the application is running, you can test the following endpoints using tools like **Postman** or **cURL**:

- **GET /api/posts**: Returns the list of all posts with the number of comments.
- **POST /api/posts**: Creates a new post.
- **GET /api/posts/{id}**: Retrieves a specific post.
- **POST /api/posts/{id}/comments**: Adds a comment to a specific post.

## Running with Docker

### 1. Build the Docker image

To build the Docker image, run the following command in the root directory of the project:

```bash
docker build -t blogplatform-api .
```

This command will create a Docker image named `blogplatform-api` based on your Dockerfile.

### 2. Run the Docker container

After building the image, run the container with the following command:

```bash
docker run -d -p 5000:5000 -e ASPNETCORE_ENVIRONMENT=Production --name blogplatform-container blogplatform-api
```

This will run the container in the background (`-d`), map port 5000 from the host to port 5000 on the container, and set the environment variable `ASPNETCORE_ENVIRONMENT` to `Production`.

### 3. Access the application

After the container is started, you can access the API through:

- **Swagger (API Documentation)**: Access `http://localhost:5000/swagger`
- **API Endpoints**:
  - **GET /api/posts**: `http://localhost:5000/api/posts`
  - **POST /api/posts**: `http://localhost:5000/api/posts`
  - **GET /api/posts/{id}**: `http://localhost:5000/api/posts/{id}`
  - **POST /api/posts/{id}/comments**: `http://localhost:5000/api/posts/{id}/comments`

## Database

The application uses **SQLite** as the database. By default, it is configured to use **SQLite with a `.db` file** for persistent storage.

### 1. Using SQLite with a `.db` file
**Example configuration in `appsettings.json`** (via Docker):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=/app/blogplatform.db"
  }
}
