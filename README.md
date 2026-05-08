# Access Management Web Application

This project is an ASP.NET Core 6 MVC web application designed to manage user access requests for applications. Users can submit requests to create new access, revoke existing access, change permissions, and reactivate previously revoked access.

## Features

- **Create Access Request**: Users can request access to applications.
- **Revoke Access**: Users can revoke their existing access.
- **Change Access Permissions**: Users can modify their current access permissions.
- **Reactivate Access**: Users can reactivate previously revoked access.

## Project Structure

```
access-management-webapp
├── Controllers
│   └── AccessController.cs              # MVC controller for access management actions
├── Models
│   ├── OracleSettings.cs                # Oracle database configuration model
│   └── AccessRequestModels.cs           # Request models (Create, Revoke, Change, Reactivate)
├── Repositories
│   ├── IUserAccessRepository.cs         # Interface for data access
│   └── UserAccessRepository.cs          # Oracle database operations implementation
├── Views
│   └── Access
│       ├── Index.cshtml                 # Home/navigation page
│       ├── CreateAccessForm.cshtml      # Create access form
│       ├── RevokeAccessForm.cshtml      # Revoke access form
│       ├── ChangePermissionForm.cshtml  # Change permission form
│       └── ReactivateAccessForm.cshtml  # Reactivate access form
│   └── Shared
│       └── Error.cshtml                 # Error view
├── Program.cs                           # ASP.NET Core app configuration
├── appsettings.json                     # Application settings (Oracle connection)
├── AccessManagementWebApp.csproj        # Project file
└── README.md                            # Project documentation
```

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/access-management-webapp.git
   ```

2. Navigate to the project directory:
   ```
   cd access-management-webapp
   ```

3. Restore .NET dependencies:
   ```
   dotnet restore
   ```

## Configuration

Update `appsettings.json` with your Oracle database connection details:

```json
{
  "Oracle": {
    "UserId": "your_username",
    "Password": "your_password",
    "DataSource": "your_connection_string"
  }
}
```

## Usage

1. Start the application:
   ```
   dotnet run --urls http://localhost:5000
   ```

2. Open your web browser and navigate to `http://localhost:5000` to access the application.

3. Access the forms using either:
   - **MVC Routes**: `/Access/CreateAccessForm`, `/Access/RevokeAccessForm`, `/Access/ChangePermissionForm`, `/Access/ReactivateAccessForm`
   - **Legacy .aspx Routes**: `/createAccessForm.aspx`, `/revokeAccessForm.aspx`, `/changePermissionForm.aspx`, `/reactivateAccessForm.aspx`

## Technology Stack

- **Framework**: ASP.NET Core 6
- **Pattern**: MVC with Razor Views
- **Database**: Oracle (via Oracle.ManagedDataAccess.Core)
- **Language**: C#

## Database

This application connects to an Oracle database. Ensure that:
- Your Oracle database is running and accessible
- The connection string in `appsettings.json` is correctly configured
- The required `user_access` table exists with columns: `username`, `application_id`, `access_level`

## API Endpoints

- `GET /Access/CreateAccessForm` - Display create access form
- `POST /Access/RequestAccess` - Submit create access request
- `GET /Access/RevokeAccessForm` - Display revoke access form
- `POST /Access/RevokeAccess` - Submit revoke access request
- `GET /Access/ChangePermissionForm` - Display change permission form
- `POST /Access/ChangePermission` - Submit change permission request
- `GET /Access/ReactivateAccessForm` - Display reactivate access form
- `POST /Access/ReactivateAccess` - Submit reactivate access request

## Contributing

Feel free to submit issues or pull requests for any improvements or bug fixes.