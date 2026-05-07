# Access Management Web Application

This project is a web application designed to manage user access requests for applications. Users can submit requests to create new access, revoke existing access, or change permissions for their current access.

## Features

- **Create Access Request**: Users can request access to applications.
- **Revoke Access**: Users can revoke their existing access.
- **Change Access Permissions**: Users can modify their current access permissions.

## Project Structure

```
access-management-webapp
├── src
│   ├── app.js                  # Main entry point of the application
│   ├── controllers
│   │   └── accessController.js  # Logic for handling access requests
│   ├── models
│   │   └── userModel.js        # Interacts with the Oracle database
│   ├── routes
│   │   └── accessRoutes.js     # Defines application routes
│   ├── database
│   │   └── oracleConnection.js  # Connects to the Oracle database
│   └── views
│       └── index.html          # HTML structure for user interface
├── package.json                 # npm configuration file
├── server.js                    # Starts the Express server
└── README.md                    # Project documentation
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

3. Install the dependencies:
   ```
   npm install
   ```

## Usage

1. Start the server:
   ```
   node server.js
   ```

2. Open your web browser and navigate to `http://localhost:3000` to access the application.

## Database Connection

This application connects to an Oracle database. Ensure that your database is set up and the connection details are correctly configured in `src/database/oracleConnection.js`.

## Contributing

Feel free to submit issues or pull requests for any improvements or bug fixes.