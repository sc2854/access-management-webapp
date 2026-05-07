const oracledb = require('oracledb');

async function connectToOracle() {
    let connection;

    try {
        connection = await oracledb.getConnection({
            user: 'your_username',
            password: 'your_password',
            connectionString: 'your_connection_string'
        });
        console.log('Successfully connected to Oracle database');
    } catch (err) {
        console.error('Error connecting to Oracle database:', err);
    } finally {
        if (connection) {
            try {
                await connection.close();
            } catch (err) {
                console.error('Error closing the connection:', err);
            }
        }
    }
}

module.exports = connectToOracle;