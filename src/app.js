const express = require('express');
const bodyParser = require('body-parser');
const { connectToOracle } = require('./database/oracleConnection');
const setAccessRoutes = require('./routes/accessRoutes');

const app = express();
const PORT = process.env.PORT || 3000;

// Middleware
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// Connect to Oracle Database
connectToOracle();

// Set up routes
setAccessRoutes(app);

// Start the server
app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});