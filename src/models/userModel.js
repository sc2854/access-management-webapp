class UserModel {
    constructor(databaseConnection) {
        this.databaseConnection = databaseConnection;
    }

    async checkUserAccess(userId) {
        const query = 'SELECT access_level FROM user_access WHERE user_id = :userId';
        const binds = { userId: userId };
        const options = {
            outFormat: this.databaseConnection.OBJECT,
            autoCommit: true
        };

        try {
            const result = await this.databaseConnection.execute(query, binds, options);
            return result.rows.length > 0 ? result.rows[0].access_level : null;
        } catch (error) {
            throw new Error('Error checking user access: ' + error.message);
        }
    }

    async updateUserAccess(userId, newAccessLevel) {
        const query = 'UPDATE user_access SET access_level = :newAccessLevel WHERE user_id = :userId';
        const binds = { newAccessLevel: newAccessLevel, userId: userId };
        const options = {
            autoCommit: true
        };

        try {
            const result = await this.databaseConnection.execute(query, binds, options);
            return result.rowsAffected > 0;
        } catch (error) {
            throw new Error('Error updating user access: ' + error.message);
        }
    }
}

module.exports = UserModel;